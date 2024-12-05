using Common;

namespace _2024.Day04;

internal class InputParser
{
    public static void Execute(string inputPath, out List<List<char>> puzzle)
    {
        puzzle = InputUtils.GetListOfCharsEveryLine(inputPath);
    }
}
