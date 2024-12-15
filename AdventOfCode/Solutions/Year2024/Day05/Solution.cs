using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day05;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/5">
/// Day 5: Print Queue.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private List<(int firstNumber, int secondNumber)> Rules = [];
    private List<List<int>> UpdatesLists = [];

    public Solution() : base(2024, 05) { }

    public override object SolvePartOne()
    {
        return UpdatesLists.Where(AreUpdatesAcceptable).Sum(GetMiddleElement);
    }

    public override object SolvePartTwo()
    {
        return UpdatesLists
            .Where(updates => !AreUpdatesAcceptable(updates))
            .Select(GetFixedUpdates)
            .Sum(GetMiddleElement);
    }

    private bool AreUpdatesAcceptable(List<int> updates)
    {
        for (int i = 0; i < updates.Count - 1; i++)
        {
            if (!Rules.Any(rule => rule.firstNumber == updates[i] && rule.secondNumber == updates[i + 1]))
            {
                return false;
            }
        }

        return true;
    }

    private List<int> GetFixedUpdates(List<int> updates)
    {
        return updates
            .GroupBy(update => update)
            .ToDictionary(
                group => group.Key,
                group => Rules.Count(rule => rule.firstNumber == group.Key && updates.Contains(rule.secondNumber))
            )
            .OrderByDescending(frequency => frequency.Value)
            .Select(frequency => frequency.Key)
            .ToList();
    }

    private int GetMiddleElement(List<int> elements) => elements[elements.Count / 2];

    protected override void ProcessInput()
    {
        string[] sections = InputUtils.SplitIntoSections(Input);

        Rules = new Regex(@"(\d+)\|(\d+)")
            .Matches(sections[0])
            .Select(match => (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)))
            .ToList();

        UpdatesLists = InputUtils
            .SplitIntoLines(sections[1])
            .Select(line => line.Split(",").Select(int.Parse).ToList())
            .ToList();
    }
}
