using System.Text.RegularExpressions;
using Common;
using static _2024.Day03.Constants;

/// <summary>
/// Day 3: Mull It Over.
/// <see cref="https://adventofcode.com/2024/day/3"/>
/// </summary>
namespace _2024.Day03;

internal class Solution : BaseSolution
{
    public string Memory;

    public Solution() : base(2024, 3)
    {
        InputParser.Execute(InputPath, out Memory);
    }

    public override int SolvePartOne()
    {
        return MultiplicationRegex().Matches(Memory).Select(HandleMultiplication).Sum();
    }

    public override int SolvePartTwo()
    {
        int sum = 0;
        bool shouldCalculate = true;

        foreach (Match match in MultiplicationWithToleranceRegex().Matches(Memory))
        {
            if (match.Value == DoInstruction)
            {
                shouldCalculate = true;
            }
            else if (match.Value == DontInstruction)
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
