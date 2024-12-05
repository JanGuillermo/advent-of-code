using Common;

namespace _2015.Day02;

internal class InputParser
{
    public static void Execute(string inputPath, out List<(int length, int width, int height)> dimensions)
    {
        dimensions = [];
        List<string> lines = InputUtils.GetListOfLines(inputPath);

        foreach (string line in lines)
        {
            string[] parts = line.Split('x');

            dimensions.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
        }
    }
}