using AdventOfCode.Solutions.Objects;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day10;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/10">
/// Day 10: Hoof It.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private const int MAX_HEIGHT = 9;

    private Dictionary<Position, int> Map = [];
    private int MapRows = 0;
    private int MapCols = 0;

    public Solution() : base(2024, 10) { }

    public override object SolvePartOne()
    {
        return Map
            .Where(cell => cell.Value == 0)
            .Sum(cell => CountTrailheads(cell.Key));
    }

    public override object SolvePartTwo()
    {
        return Map
            .Where(cell => cell.Value == 0)
            .Sum(cell => GetRating(cell.Key, 0, 0));
    }

    private int CountTrailheads(Position position)
    {
        HashSet<Position> trailheads = [];

        foreach (Direction direction in Direction.Orthogonal)
        {
            trailheads.UnionWith(GetTrailheads(position, direction, 0, []));
        }

        return trailheads.Count;
    }

    private HashSet<Position> GetTrailheads(Position position, Direction direction, int currentHeight, HashSet<Position> trailheads)
    {
        Position newPosition = position.Move(direction);

        if (!newPosition.IsInBounds(MapRows, MapCols)) return trailheads;

        int newHeight = Map[newPosition];

        if (newHeight == currentHeight + 1)
        {
            if (newHeight == MAX_HEIGHT)
            {
                trailheads.Add(newPosition);
            }
            else
            {
                foreach (Direction newDirection in Direction.Orthogonal)
                {
                    trailheads.UnionWith(GetTrailheads(newPosition, newDirection, newHeight, trailheads));
                }
            }
        }

        return trailheads;
    }

    private int GetRating(Position position, int currentHeight, int currentRating)
    {
        int rating = currentRating;

        foreach (Direction direction in Direction.Orthogonal)
        {
            Position newPosition = position.Move(direction);

            if (!newPosition.IsInBounds(MapRows, MapCols)) continue;

            int newHeight = Map[newPosition];

            if (newHeight == currentHeight + 1)
            {
                rating += (newHeight == MAX_HEIGHT) ? 1 : GetRating(newPosition, newHeight, currentRating);
            }
        }

        return rating;
    }

    protected override void ProcessInput()
    {
        string[] lines = InputUtils.SplitIntoLines(Input);

        MapRows = lines.Length;
        MapCols = lines[0].Length;

        for (int row = 0; row < MapRows; row++)
        {
            for (int col = 0; col < MapCols; col++)
            {
                Position position = new Position(row, col);
                Map[position] = lines[row][col] - '0';
            }
        }
    }
}
