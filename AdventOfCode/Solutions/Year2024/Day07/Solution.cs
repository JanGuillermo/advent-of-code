using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day07;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/7">
/// Day 7: Bridge Repair.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private List<(long answer, int[] numbers)> Equations = [];

    public Solution() : base(2024, 7) { }

    public override object SolvePartOne()
    {
        return Equations
            .Where(equation => CanBeCalibrated(equation.answer, equation.numbers, ['*', '+'], 0, equation.numbers[0]))
            .Sum(equation => equation.answer);
    }

    public override object SolvePartTwo()
    {
        List<(long answer, int[] numbers)> calibratedEquations = Equations.Where(equation => CanBeCalibrated(equation.answer, equation.numbers, ['*', '+'], 0, equation.numbers[0])).ToList();
        long result1 = calibratedEquations.Sum(equation => equation.answer);
        long result2 = Equations
            .Except(calibratedEquations)
            .Where(equation => CanBeCalibrated(equation.answer, equation.numbers, ['*', '+', '|'], 0, equation.numbers[0]))
            .Sum(equation => equation.answer);

        return result1 + result2;
    }

    private static bool CanBeCalibrated(long answer, int[] numbers, char[] operands, int index, long currentResult)
    {
        if (index == numbers.Length - 1)
        {
            return currentResult == answer;
        }

        foreach (char operand in operands)
        {
            long newResult = operand switch
            {
                '+' => currentResult + numbers[index + 1],
                '*' => currentResult * numbers[index + 1],
                '|' => long.Parse($"{currentResult}{numbers[index + 1]}"),
                _ => currentResult
            };

            if (newResult <= answer && CanBeCalibrated(answer, numbers, operands, index + 1, newResult))
            {
                return true;
            }
        }

        return false;
    }

    protected override void ProcessInput()
    {
        foreach (string line in InputUtils.SplitIntoLines(Input))
        {
            string[] bits = line.Split(": ");
            long answer = long.Parse(bits[0]);
            int[] numbers = bits[1].Split(" ").Select(int.Parse).ToArray();

            Equations.Add((answer, numbers));
        }
    }
}