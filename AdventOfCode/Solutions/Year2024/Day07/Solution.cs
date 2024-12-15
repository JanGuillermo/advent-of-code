namespace AdventOfCode.Solutions.Year2024.Day07;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/7">
/// Day 7: Bridge Repair.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    List<(long answer, List<int> numbers)> equations = [];

    public Solution() : base(2024, 7)
    {
        foreach (string line in Input.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            string[] bits = line.Split(": ");
            long answer = long.Parse(bits[0]);
            List<int> numbers = bits[1].Split(" ").Select(int.Parse).ToList();

            equations.Add((answer, numbers));
        }
    }

    public override object SolvePartOne()
    {
        return equations
            .Where(equation => CanBeCalibrated(equation.answer, equation.numbers, ['*', '+']))
            .Sum(equation => equation.answer);
    }

    public override object SolvePartTwo()
    {
        List<(long answer, List<int> numbers)> calibratedEquations = equations.Where(equation => CanBeCalibrated(equation.answer, equation.numbers, ['*', '+'])).ToList();
        long result1 = calibratedEquations.Sum(equation => equation.answer);
        long result2 = equations
            .Except(calibratedEquations)
            .Where(equation => CanBeCalibrated(equation.answer, equation.numbers, ['*', '+', '|']))
            .Sum(equation => equation.answer);

        return result1 + result2;
    }

    private static bool CanBeCalibrated(long answer, List<int> numbers, List<char> operands)
    {
        return CanBeCalibratedRecursive(answer, numbers, operands, 0, numbers[0]);
    }

    private static bool CanBeCalibratedRecursive(long answer, List<int> numbers, List<char> operands, int index, long currentResult)
    {
        if (index == numbers.Count - 1)
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

            if (newResult > answer)
            {
                continue;
            }

            if (CanBeCalibratedRecursive(answer, numbers, operands, index + 1, newResult))
            {
                return true;
            }
        }

        return false;
    }
}