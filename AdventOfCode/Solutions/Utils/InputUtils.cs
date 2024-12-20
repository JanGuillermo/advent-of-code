namespace AdventOfCode.Solutions.Utils;

internal static class InputUtils
{
    public static string[] SplitIntoSections(string input)
    {
        return input.Split(["\r\n\r\n", "\n\n", "\r\r"], StringSplitOptions.RemoveEmptyEntries);
    }

    public static string[] SplitIntoLines(string input)
    {
        return input.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);
    }

    public static string RemoveLineBreaks(string input)
    {
        return input.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
    }
}
