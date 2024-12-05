using Common;

namespace _2015.Day01;

internal class InputParser
{
    public static void Execute(string inputPath, out string buildingInstructions)
    {
        buildingInstructions = InputUtils.GetAllText(inputPath);
    }
}
