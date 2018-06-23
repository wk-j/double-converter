using System.Globalization;

var s1 = Int32.TryParse("123", out var x);
var s2 = Int32.TryParse("123", NumberStyles.Integer, CultureInfo.InvariantCulture, out var y);