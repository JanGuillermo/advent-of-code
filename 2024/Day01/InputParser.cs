using System.Text.RegularExpressions;
using Common;
using static _2024.Day01.Constants;

namespace _2024.Day01;

internal class InputParser
{
    public static void Execute(string inputPath, out List<int> locations1, out List<int> locations2)
    {
        locations1 = [];
        locations2 = [];

        foreach (Match match in LocationsRegex().Matches(InputUtils.GetAllText(inputPath)))
        {
            locations1.Add(int.Parse(match.Groups[1].Value));
            locations2.Add(int.Parse(match.Groups[2].Value));
        }

        locations1.Sort();
        locations2.Sort();
    }
}
