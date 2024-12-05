namespace _2024.Day02;

internal class InputParser
{
    public static void Execute(string inputPath, out List<List<int>> reports)
    {
        reports = File.ReadAllLines(inputPath).Select(line => line.Split(' ').Select(int.Parse).ToList()).ToList();
    }
}
