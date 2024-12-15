namespace AdventOfCode.Solutions.Year2024.Day02;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/2">
/// Day 2: Red-Nosed Reports.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    public List<List<int>> Reports = [];

    public Solution() : base(2024, 02)
    {
        Reports = Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList())
            .ToList();
    }

    public override object SolvePartOne()
    {
        return Reports.Count(ValidateSequence);
    }

    public override object SolvePartTwo()
    {
        return Reports.Count(levels =>
            ValidateSequence(levels)
            || levels
                .Where((_, i) =>
                {
                    List<int> modifiedReport = new(levels);
                    modifiedReport.RemoveAt(i);
                    return ValidateSequence(modifiedReport);
                })
                .Any()
        );
    }

    public bool ValidateSequence(List<int> levels)
    {
        bool? isAscending = null;

        for (int i = 0; i < levels.Count - 1; i++)
        {
            int difference = levels[i] - levels[i + 1];

            if (Math.Abs(difference) is < 1 or > 3)
            {
                return false;
            }

            bool currentIsAscending = difference < 0;

            if (isAscending == null)
            {
                isAscending = currentIsAscending;
            }
            else if (isAscending != currentIsAscending)
            {
                return false;
            }
        }

        return true;
    }
}
