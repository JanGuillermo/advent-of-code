using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2024.Day13;

/// <summary>
/// Day 13: Claw Contraption
/// <see cref="https://adventofcode.com/2024/day/13"/>
/// </summary>
internal class Solution : SolutionBase
{
    static List<ClawMachine> ClawMachines = [];

    public Solution() : base(2024, 13) { }

    public override object SolvePartOne()
    {
        return ClawMachines.Sum(clawMachine => CalculateMinimumTokensToWin(clawMachine));
    }

    public override object SolvePartTwo()
    {
        return ClawMachines.Sum(clawMachine => CalculateMinimumTokensToWin(clawMachine, offset: 10000000000000));
    }

    private static long CalculateMinimumTokensToWin(ClawMachine clawMachine, long offset = 0)
    {
        Button buttonA = clawMachine.ButtonA;
        Button buttonB = clawMachine.ButtonB;
        Position prize = clawMachine.Prize.Move(offset);
        decimal buttonBPresses = ((prize.Y * buttonA.X) - (buttonA.Y * prize.X)) / ((buttonB.Y * buttonA.X) - (buttonB.X * buttonA.Y));
        decimal buttonAPresses = (prize.X - (buttonBPresses * buttonB.X)) / buttonA.X;

        if (buttonAPresses % 1 != 0
            || buttonBPresses % 1 != 0
            || (offset == 0 && (buttonAPresses > 100 || buttonBPresses > 100)))
        {
            return 0;
        }

        return (long)buttonAPresses * 3 + (long)buttonBPresses;
    }

    protected override void ProcessInput()
    {
        string[] sections = Input.Split(["\r\n\r\n", "\n\n"], StringSplitOptions.RemoveEmptyEntries);

        foreach (string section in sections)
        {
            Match regexButtonA = new Regex(@"Button A: X\+(?<x>\d+), Y\+(?<y>\d+)").Match(section);
            Match regexButtonB = new Regex(@"Button B: X\+(?<x>\d+), Y\+(?<y>\d+)").Match(section);
            Match regexPrize = new Regex(@"Prize: X=(?<x>\d+), Y=(?<y>\d+)").Match(section);

            Button buttonA = new(decimal.Parse(regexButtonA.Groups["x"].Value), decimal.Parse(regexButtonA.Groups["y"].Value));
            Button buttonB = new(decimal.Parse(regexButtonB.Groups["x"].Value), decimal.Parse(regexButtonB.Groups["y"].Value));
            Position prize = new(decimal.Parse(regexPrize.Groups["x"].Value), decimal.Parse(regexPrize.Groups["y"].Value));

            ClawMachines.Add(new ClawMachine(buttonA, buttonB, prize));
        }
    }
}

internal record ClawMachine(Button ButtonA, Button ButtonB, Position Prize);

internal record Button(decimal X, decimal Y);

internal record Position(decimal X, decimal Y)
{
    public Position Move(long offset) => new(X + offset, Y + offset);
}