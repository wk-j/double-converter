#addin "wk.StartProcess"
#addin "wk.ProjectParser"

using PS = StartProcess.Processor;
using ProjectParser;

var name = "DoubleConverter";
var project = $"src/{name}/{name}.csproj";

var currentDir = new System.IO.DirectoryInfo(".").FullName;
var info = Parser.Parse(project);

Task("Pack").Does(() => {
    CleanDirectory("publish");
    DotNetCorePack($"src/{name}", new DotNetCorePackSettings {
        OutputDirectory = "publish"
    });
});

Task("Publish-Nuget")
    .IsDependentOn("Pack")
    .Does(() => {
        var npi = EnvironmentVariable("npi");
        var nupkg = new DirectoryInfo("publish").GetFiles("*.nupkg").LastOrDefault();
        var package = nupkg.FullName;
        NuGetPush(package, new NuGetPushSettings {
            //Source = "http://192.168.0.109:7777/nuget",
            ApiKey = npi
        });
});

Task("Install")
    .IsDependentOn("Pack")
    .Does(() => {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        PS.StartProcess("dotnet tool uninstall -g wk.DoubleConverter");
        PS.StartProcess($"dotnet tool install  -g wk.DoubleConverter --add-source {currentDir}/publish --version {info.Version}");
    });

var target = Argument("target", "Pack");
RunTarget(target);