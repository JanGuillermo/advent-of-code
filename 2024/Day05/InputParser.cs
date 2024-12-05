using System.Text.RegularExpressions;
using Common;
using static _2024.Day05.Constants;

namespace _2024.Day05;

internal class InputParser
{
    public static void Execute(
        string inputPath,
        out List<(int firstNumber, int secondNumber)> rules,
        out List<List<int>> updatesLists)
    {
        rules = [];
        updatesLists = [];
        List<string> inputs = InputUtils.GetListOfLines(inputPath);
        int lineReadIdx = 0;

        while (lineReadIdx < inputs.Count && !string.IsNullOrEmpty(inputs[lineReadIdx]))
        {
            Match match = RuleRegex().Match(inputs[lineReadIdx]);

            rules.Add((int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
            lineReadIdx++;
        }

        lineReadIdx++;

        while (lineReadIdx < inputs.Count)
        {
            updatesLists.Add(inputs[lineReadIdx].Split(",").Select(int.Parse).ToList());
            lineReadIdx++;
        }
    }
}
