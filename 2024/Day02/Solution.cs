using Common;

namespace _2024.Day02;

/// <summary>
/// Day 2: Red-Nosed Reports.
/// <see cref="https://adventofcode.com/2024/day/2"/>
/// </summary>
internal class Solution : BaseSolution
{
    public List<List<int>> Reports = [];

    public Solution() : base(2024, 2)
    {
        InputParser.Execute(InputPath, out Reports);
    }

    public override int SolvePartOne()
    {
        return Reports.Count(ValidateSequence);
    }

    public override int SolvePartTwo()
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
