namespace AdventOfCode.Solutions.Year2024.Day07;

/// <summary>
/// Day 7: Bridge Repair.
/// <see cref="https://adventofcode.com/2024/day/7"/>
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
        int combinations = (int)Math.Pow(operands.Count, numbers.Count - 1);

        for (int combination = 0; combination < combinations; combination++)
        {
            long result = numbers[0];
            int operandDeterminer = combination;

            for (int index = 1; index < numbers.Count; index++)
            {
                result = operands[operandDeterminer % operands.Count] switch
                {
                    '+' => result + numbers[index],
                    '*' => result * numbers[index],
                    '|' => long.Parse($"{result}{numbers[index]}"),
                    _ => result
                };
                operandDeterminer /= operands.Count;

                if (result > answer)
                {
                    break;
                }
            }

            if (result == answer)
            {
                return true;
            }
        }

        return false;
    }
}