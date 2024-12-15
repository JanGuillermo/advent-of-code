namespace AdventOfCode.Solutions.Utils;

internal class InputUtils
{
    public static string[] SplitIntoLines(string input)
    {
        return input.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);
    }
}
