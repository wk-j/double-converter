using System;

class Program {
    static void Main(string[] args) {
        var ok = Double.TryParse(args[0], out var result);
        if (ok) {
            var str = DoubleConverter.ToExactString(result);
            Console.WriteLine(str);
        } else {
            Console.WriteLine("- invalid input");
        }
    }
}
