namespace AdventOfCode.Solutions.Year2015.Day03;

/// <summary>
/// Day 3: Perfectly Spherical Houses in a Vacuum.
/// <see cref="https://adventofcode.com/2015/day/3"/>
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
        '^' => Direction.Up,
        'v' => Direction.Down,
        '>' => Direction.Right,
        '<' => Direction.Left,
        _ => throw new InvalidOperationException("Invalid instruction.")
    };
}

internal record Direction(int Row, int Col)
{
    public static Direction Up => new(1, 0);
    public static Direction Down => new(-1, 0);
    public static Direction Right => new(0, 1);
    public static Direction Left => new(0, -1);
}

internal record Position(int Row, int Col)
{
    public Position Move(Direction direction) => new Position(Row + direction.Row, Col + direction.Col);
}