using AdventOfCode.Solutions.Objects;

namespace AdventOfCode.Solutions.Year2024.Day12;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/12">
/// Day 12: Garden Groups.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    readonly Dictionary<Position, char> Map = [];
    int MapWidth = 0;
    int MapHeight = 0;

    public Solution() : base(2024, 12) { }

    public override object SolvePartOne()
    {
        return GetRegions().Sum(CalculatePriceBasedOnPerimeter);
    }

    public override object SolvePartTwo()
    {
        return GetRegions().Sum(CalculatePriceBasedOnSides);
    }

    private long CalculatePriceBasedOnPerimeter(List<Position> region)
    {
        int area = region.Count;
        int perimeter = region.Sum(position => Direction.Orthogonal.Count(direction => !region.Contains(position.Move(direction))));

        return area * perimeter;
    }

    private long CalculatePriceBasedOnSides(List<Position> region)
    {
        int area = region.Count;
        int sides = 0;

        foreach (Position position in region)
        {
            List<bool> cornerChecks = [
                // Outer Corners
                (!region.Contains(position.Move(Direction.North)) && !region.Contains(position.Move(Direction.East))),
                (!region.Contains(position.Move(Direction.South)) && !region.Contains(position.Move(Direction.East))),
                (!region.Contains(position.Move(Direction.South)) && !region.Contains(position.Move(Direction.West))),
                (!region.Contains(position.Move(Direction.North)) && !region.Contains(position.Move(Direction.West))),

                // Inner Corners
                (region.Contains(position.Move(Direction.North)) && region.Contains(position.Move(Direction.East)) && !region.Contains(position.Move(Direction.North).Move(Direction.East))),
                (region.Contains(position.Move(Direction.South)) && region.Contains(position.Move(Direction.East)) && !region.Contains(position.Move(Direction.South).Move(Direction.East))),
                (region.Contains(position.Move(Direction.South)) && region.Contains(position.Move(Direction.West)) && !region.Contains(position.Move(Direction.South).Move(Direction.West))),
                (region.Contains(position.Move(Direction.North)) && region.Contains(position.Move(Direction.West)) && !region.Contains(position.Move(Direction.North).Move(Direction.West)))

            ];

            sides += cornerChecks.Count(check => check);
        }

        return area * sides;
    }

    private List<List<Position>> GetRegions()
    {
        List<List<Position>> regions = [];
        HashSet<Position> visited = [];

        foreach (Position position in Map.Keys)
        {
            if (visited.Add(position))
            {
                HashSet<Position> region = PlotRegion(Map[position], position);

                visited.UnionWith(region);
                regions.Add(region.ToList());
            }
        }

        return regions;
    }

    private HashSet<Position> PlotRegion(char plot, Position position)
    {
        HashSet<Position> plotPositions = [position];

        foreach (Direction direction in Direction.Orthogonal)
        {
            plotPositions.UnionWith(PlotRegion(plot, position, direction, plotPositions));
        }

        return plotPositions;
    }

    private HashSet<Position> PlotRegion(char plot, Position position, Direction direction, HashSet<Position> plotPositions)
    {
        Position newPosition = position.Move(direction);

        if (newPosition.OutOfBounds(MapHeight, MapWidth)
            || Map[newPosition] != plot
            || plotPositions.Contains(newPosition))
        {
            return plotPositions;
        }

        plotPositions.Add(newPosition);

        foreach (Direction newDirection in Direction.Orthogonal)
        {
            plotPositions.UnionWith(PlotRegion(plot, newPosition, newDirection, plotPositions));
        }

        return plotPositions;
    }

    protected override void ProcessInput()
    {
        string[] lines = Input.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries);

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                Position position = new Position(row, col);
                Map[position] = lines[row][col];
            }
        }

        MapWidth = lines[0].Length;
        MapHeight = lines.Length;
    }
}
