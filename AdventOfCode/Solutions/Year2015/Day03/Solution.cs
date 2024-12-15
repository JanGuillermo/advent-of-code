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
        Position position = new(0, 0);
        HashSet<Position> visited = [position];

        foreach (char instruction in Input)
        {
            position = position.Move(GetDirection(instruction));
            visited.Add(position);
        }

        return visited.Count;
    }

    public override object SolvePartTwo()
    {
        Position santaPosition = new(0, 0);
        Position roboSantaPosition = new(0, 0);
        HashSet<Position> visited = [santaPosition];

        for (int idx = 0; idx < Input.Length; idx++)
        {
            Direction direction = GetDirection(Input[idx]);

            if (idx % 2 == 0)
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

    private static Direction GetDirection(char instruction) => instruction switch
    {
        '^' => Direction.North,
        'v' => Direction.South,
        '>' => Direction.East,
        '<' => Direction.West,
        _ => throw new InvalidOperationException("Invalid instruction.")
    };
}
