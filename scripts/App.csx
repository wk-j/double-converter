#! "netcoreapp2.0"
#r "nuget:BenchmarkDotNet,0.10.12"

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Attributes.Jobs;
using System.Runtime.InteropServices;
using System;
using System.Globalization;

BenchmarkRunner.Run<Test>();

[MemoryDiagnoser]
[InProcess]
public class Test {

    /*
    Method |     Mean |    Error |    StdDev | Allocated |
------- |---------:|---------:|----------:|----------:|
   Try1 | 63.42 ns | 1.048 ns | 0.9803 ns |       0 B |
   Try2 | 60.98 ns | 1.144 ns | 1.9734 ns |       0 B |
     */

    [Benchmark]
    public void Try1() {
        var s1 = Int32.TryParse("123", out var x);
    }

    [Benchmark]
    public void Try2() {
        var s2 = Int32.TryParse("123", NumberStyles.Integer, CultureInfo.InvariantCulture, out var y);
    }
}

