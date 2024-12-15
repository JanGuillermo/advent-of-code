using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2015.Day02;

/// <summary>
/// <see href="https://adventofcode.com/2015/day/2">
/// Day 2: I Was Told There Would Be No Math.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private List<(int length, int width, int height)> Dimensions = [];

    public Solution() : base(2015, 2) { }

    public override object SolvePartOne()
    {
        return Dimensions.Sum(CalculateSurfaceAreaWithSlack);
    }

    public override object SolvePartTwo()
    {
        return Dimensions.Sum(CalculateRibbonLength);
    }

    private static int CalculateSurfaceAreaWithSlack((int length, int width, int height) dimension)
    {
        return CalculateSurfaceArea(dimension) + CalculateSlack(dimension);
    }

    private static int CalculateSurfaceArea((int length, int width, int height) dimension)
    {
        return 2 * dimension.length * dimension.width + 2 * dimension.width * dimension.height + 2 * dimension.height * dimension.length;
    }

    private static int CalculateSlack((int length, int width, int height) dimension)
    {
        int[] sides = [dimension.length * dimension.width, dimension.width * dimension.height, dimension.height * dimension.length];

        return sides.Min();
    }

    private static int CalculateRibbonLength((int length, int width, int height) dimension)
    {
        int[] sides = [dimension.length, dimension.width, dimension.height];

        Array.Sort(sides);

        return 2 * (sides[0] + sides[1]) + sides[0] * sides[1] * sides[2];
    }

    protected override void ProcessInput()
    {
        Regex regex = new(@"(?<length>\d+)x(?<width>\d+)x(?<height>\d+)");

        Dimensions = InputUtils.SplitIntoLines(Input)
            .Select(line => regex.Match(line))
            .Select(match => (
                int.Parse(match.Groups["length"].Value),
                int.Parse(match.Groups["width"].Value),
                int.Parse(match.Groups["height"].Value)
            ))
            .ToList();
    }
}