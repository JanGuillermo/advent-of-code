using System.Text.RegularExpressions;

namespace _2024.Day03;

internal partial class Constants
{
    public const string DoInstruction = "do()";

    public const string DontInstruction = "don't()";

    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    public static partial Regex MultiplicationRegex();

    [GeneratedRegex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")]
    public static partial Regex MultiplicationWithToleranceRegex();
}
