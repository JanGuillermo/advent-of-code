using AdventOfCode.Solutions.Objects;

namespace AdventOfCode.Solutions.Year2015.Day03;

/// <summary>
/// <see href="https://adventofcode.com/2015/day/3">
/// Day 3: Perfectly Spherical Houses in a Vacuum.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    public Solution() : base(2015, 03) { }

    public override object SolvePartOne()
    {
        return CalculateVisitedHouses(Input);
    }

    public override object SolvePartTwo()
    {
        return CalculateVisitedHouses(Input, includeRoboSanta: true);
    }

    private static int CalculateVisitedHouses(string instructions, bool includeRoboSanta = false)
    {
        Position santaPosition = new(0, 0);
        Position roboSantaPosition = new(0, 0);
        HashSet<Position> visited = [santaPosition];

        for (int idx = 0; idx < instructions.Length; idx++)
        {
            Direction direction = Direction.GetDirection(instructions[idx]);

            if (idx % 2 == 0 || !includeRoboSanta)
            {
                santaPosition = santaPosition.Move(direction);
                visited.Add(santaPosition);
            }
            else
            {
                roboSantaPosition = roboSantaPosition.Move(direction);
                visited.Add(roboSantaPosition);
            }
        }

        return visited.Count;
    }
}
