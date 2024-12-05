namespace AdventOfCode.Solutions.Year2015.Day02;

/// <summary>
/// Day 2: I Was Told There Would Be No Math.
/// <see cref="https://adventofcode.com/2015/day/2"/>
/// </summary>
internal class Solution : SolutionBase
{
    List<(int length, int width, int height)> Dimensions = [];

    public Solution() : base(2015, 2)
    {
        List<string> lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (string line in lines)
        {
            string[] parts = line.Split('x');

            Dimensions.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
        }
    }

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
        List<int> sides = [dimension.length * dimension.width, dimension.width * dimension.height, dimension.height * dimension.length];

        return sides.Min();
    }

    private static int CalculateRibbonLength((int length, int width, int height) dimension)
    {
        List<int> sides = [dimension.length, dimension.width, dimension.height];

        sides.Sort();

        return 2 * (sides[0] + sides[1]) + sides[0] * sides[1] * sides[2];
    }
}