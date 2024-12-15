using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2024.Day03;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/3">
/// Day 3: Mull It Over.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    public Solution() : base(2024, 03) { }

    public override object SolvePartOne()
    {
        return new Regex(@"mul\((\d+),(\d+)\)").Matches(Input).Select(HandleMultiplication).Sum();
    }

    public override object SolvePartTwo()
    {
        int sum = 0;
        bool shouldCalculate = true;

        foreach (Match match in new Regex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)").Matches(Input))
        {
            if (match.Value == "do()")
            {
                shouldCalculate = true;
            }
            else if (match.Value == "don't()")
            {
                shouldCalculate = false;
            }
            else if (shouldCalculate)
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
        }

        return sum;
    }

    private int HandleMultiplication(Match match) => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
}
