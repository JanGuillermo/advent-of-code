using Common;

namespace _2024.Day03;

internal class InputParser
{
    public static void Execute(string inputPath, out string memory)
    {
        memory = InputUtils.GetAllText(inputPath);
    }
}
