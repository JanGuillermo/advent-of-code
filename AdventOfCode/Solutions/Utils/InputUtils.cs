namespace AdventOfCode.Solutions.Utils;

internal class InputUtils
{
    public static string[] SplitIntoSections(string input)
    {
        return input.Split(["\r\n\r\n", "\n\n", "\r\r"], StringSplitOptions.RemoveEmptyEntries);
    }

    public static string[] SplitIntoLines(string input)
    {
        return input.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);
    }
}
