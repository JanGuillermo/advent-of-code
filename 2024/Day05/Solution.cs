using Common;

namespace _2024.Day05;

/// <summary>
/// Day 5: Print Queue.
/// <see cref="https://adventofcode.com/2024/day/5"/>
/// </summary>
internal class Solution : BaseSolution
{
    public List<(int firstNumber, int secondNumber)> Rules = [];
    public List<List<int>> UpdatesLists = [];

    public Solution() : base(2024, 5)
    {
        InputParser.Execute(InputPath, out Rules, out UpdatesLists);
    }

    public override int SolvePartOne()
    {
        return UpdatesLists.Where(AreUpdatesAcceptable).Sum(GetMiddleElement);
    }

    public override int SolvePartTwo()
    {
        List<List<int>> updatesLists = UpdatesLists
            .Where(updates => !AreUpdatesAcceptable(updates))
            .Select(GetFixedUpdates)
            .ToList();

        return updatesLists.Sum(GetMiddleElement);
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
        Dictionary<int, int> frequencies = updates
            .GroupBy(update => update)
            .ToDictionary(
                group => group.Key,
                group => Rules.Count(rule => rule.firstNumber == group.Key && updates.Contains(rule.secondNumber))
            );

        return frequencies
            .OrderByDescending(frequency => frequency.Value)
            .Select(frequency => frequency.Key)
            .ToList();
    }

    private int GetMiddleElement(List<int> updates)
    {
        return updates[updates.Count / 2];
    }
}
