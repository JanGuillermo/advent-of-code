using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day19;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/19">
/// Day 19: Linen Layout
/// </see>
/// </summary>
internal class Solution() : SolutionBase(2024, 19)
{
    private static readonly Dictionary<(string, int), long> Part1Cache = [];
    private static readonly Dictionary<(string, int), long> Part2Cache = [];
    private string[] AvailablePatterns = [];
    private string[] DesiredPatterns = [];

    public override object SolvePartOne()
    {
        return DesiredPatterns.Count(pattern => CountArrangements(pattern, AvailablePatterns, Part1Cache) > 0);
    }

    public override object SolvePartTwo()
    {
        return DesiredPatterns.Sum(pattern => CountArrangements(pattern, AvailablePatterns, Part2Cache));
    }

    private static long CountArrangements(string desiredPattern, string[] availablePatterns, Dictionary<(string, int), long> cache, int index = 0)
    {
        if (index == desiredPattern.Length)
        {
            return 1;
        }

        if (cache.TryGetValue((desiredPattern, index), out long cachedResult))
        {
            return cachedResult;
        }

        long count = 0;

        foreach (string availablePattern in availablePatterns)
        {
            if (!desiredPattern[index..].StartsWith(availablePattern))
            {
                continue;
            }

            count += CountArrangements(desiredPattern, availablePatterns, cache, index + availablePattern.Length);
        }

        cache[(desiredPattern, index)] = count;

        return count;
    }

    protected override void ProcessInput()
    {
        string[] sections = InputUtils.SplitIntoSections(Input);
        AvailablePatterns = sections[0].Split(",").Select(x => x.Trim()).ToArray();
        DesiredPatterns = InputUtils.SplitIntoLines(sections[1]);
    }
}
