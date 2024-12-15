namespace AdventOfCode.Solutions.Year2024.Day10;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/10">
/// Day 10: Hoof It.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    Dictionary<Position, int> Map = [];
    int MapWidth = 0;
    int MapHeight = 0;

    Direction[] Directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];

    public Solution() : base(2024, 10)
    {
        string[] lines = Input.Split(["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                Position position = new Position(row, col);
                Map[position] = lines[row][col] - '0';
            }
        }

        MapWidth = lines[0].Length;
        MapHeight = lines.Length;
    }

    public override object SolvePartOne()
    {
        return Map
            .Where(cell => cell.Value == 0)
            .Select(cell => CountTrailheads(cell.Key))
            .Sum();
    }

    public override object SolvePartTwo()
    {
        return Map
            .Where(cell => cell.Value == 0)
            .Select(cell => GetRating(cell.Key))
            .Sum();
    }

    private int CountTrailheads(Position position)
    {
        HashSet<Position> trailheads = [];

        foreach (Direction direction in Directions)
        {
            trailheads.UnionWith(GetTrailheads(position, direction, 0, []));
        }

        return trailheads.Count;
    }

    private HashSet<Position> GetTrailheads(Position position, Direction direction, int currentHeight, HashSet<Position> trailheads)
    {
        Position newPosition = position.Move(direction);

        if (newPosition.OutOfBounds(MapHeight, MapWidth)) return trailheads;

        int newHeight = Map[newPosition];

        if (newHeight != currentHeight + 1) return trailheads;

        if (newHeight == 9)
        {
            trailheads.Add(newPosition);
            return trailheads;
        }

        foreach (Direction newDirection in Directions)
        {
            trailheads.UnionWith(GetTrailheads(newPosition, newDirection, newHeight, trailheads));
        }

        return trailheads;
    }

    private int GetRating(Position position)
    {
        return GetRating(position, 0, 0);
    }

    private int GetRating(Position position, int currentHeight, int currentRating)
    {
        int rating = currentRating;

        foreach (Direction direction in Directions)
        {
            Position newPosition = position.Move(direction);

            if (newPosition.OutOfBounds(MapHeight, MapWidth)) continue;

            int newHeight = Map[newPosition];

            if (newHeight == currentHeight + 1)
            {
                if (newHeight == 9)
                {
                    rating++;
                }
                else
                {
                    rating += GetRating(newPosition, newHeight, currentRating);
                }
            }
        }

        return rating;
    }
}

internal record Direction(int Row, int Col)
{
    public static readonly Direction Up = new(-1, 0);
    public static readonly Direction Right = new(0, 1);
    public static readonly Direction Down = new(1, 0);
    public static readonly Direction Left = new(0, -1);
}

internal record Position(int Row, int Col)
{
    public Position Move(Direction direction) => new(Row + direction.Row, Col + direction.Col);
    public bool OutOfBounds(int rows, int cols) => Row < 0 || Col < 0 || Row >= rows || Col >= cols;
}