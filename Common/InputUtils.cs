namespace Common;

public class InputUtils
{
    public static string GetAllText(string inputPath) => File.ReadAllText(inputPath);

    public static List<string> GetListOfLines(string inputPath) => [.. File.ReadAllLines(inputPath)];

    public static List<List<char>> GetListOfCharsEveryLine(string inputPath) => GetListOfLines(inputPath).Select(line => line.ToList()).ToList();
}
