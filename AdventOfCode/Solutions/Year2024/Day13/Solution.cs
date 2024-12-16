using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day13;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/13">
/// Day 13: Claw Contraption
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private List<(decimal aX, decimal aY, decimal bX, decimal bY, decimal pX, decimal pY)> ClawMachines = [];

    public Solution() : base(2024, 13) { }

    public override object SolvePartOne()
    {
        return ClawMachines.Sum(machine => CalculateMinimumTokensToWin(machine.aX, machine.aY, machine.bX, machine.bY, machine.pX, machine.pY));
    }

    public override object SolvePartTwo()
    {
        return ClawMachines.Sum(machine => CalculateMinimumTokensToWin(machine.aX, machine.aY, machine.bX, machine.bY, machine.pX, machine.pY, offset: 10000000000000));
    }

    private static long CalculateMinimumTokensToWin(decimal aX, decimal aY, decimal bX, decimal bY, decimal pX, decimal pY, decimal offset = 0)
    {
        (decimal adjustedPX, decimal adjustedPY) = (pX + offset, pY + offset);
        decimal buttonBPresses = ((adjustedPY * aX) - (aY * adjustedPX)) / ((bY * aX) - (bX * aY));
        decimal buttonAPresses = (adjustedPX - (buttonBPresses * bX)) / aX;

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
        Regex regexButtonA = new Regex(@"Button A: X\+(?<x>\d+), Y\+(?<y>\d+)");
        Regex regexButtonB = new Regex(@"Button B: X\+(?<x>\d+), Y\+(?<y>\d+)");
        Regex regexPrize = new Regex(@"Prize: X=(?<x>\d+), Y=(?<y>\d+)");

        foreach (string section in InputUtils.SplitIntoSections(Input))
        {
            Match matchButtonA = regexButtonA.Match(section);
            Match matchButtonB = regexButtonB.Match(section);
            Match matchPrize = regexPrize.Match(section);

            ClawMachines.Add((
                aX: decimal.Parse(matchButtonA.Groups["x"].Value),
                aY: decimal.Parse(matchButtonA.Groups["y"].Value),
                bX: decimal.Parse(matchButtonB.Groups["x"].Value),
                bY: decimal.Parse(matchButtonB.Groups["y"].Value),
                pX: decimal.Parse(matchPrize.Groups["x"].Value),
                pY: decimal.Parse(matchPrize.Groups["y"].Value)));
        }
    }
}
