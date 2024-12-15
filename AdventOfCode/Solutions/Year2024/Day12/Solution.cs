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

    Direction[] Directions = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];

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
        int perimeter = region.Sum(position => Directions.Count(direction => !region.Contains(position.Move(direction))));

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
                (!region.Contains(position.Move(Direction.Up)) && !region.Contains(position.Move(Direction.Right))),
                (!region.Contains(position.Move(Direction.Down)) && !region.Contains(position.Move(Direction.Right))),
                (!region.Contains(position.Move(Direction.Down)) && !region.Contains(position.Move(Direction.Left))),
                (!region.Contains(position.Move(Direction.Up)) && !region.Contains(position.Move(Direction.Left))),

                // Inner Corners
                (region.Contains(position.Move(Direction.Up)) && region.Contains(position.Move(Direction.Right)) && !region.Contains(position.Move(Direction.Up).Move(Direction.Right))),
                (region.Contains(position.Move(Direction.Down)) && region.Contains(position.Move(Direction.Right)) && !region.Contains(position.Move(Direction.Down).Move(Direction.Right))),
                (region.Contains(position.Move(Direction.Down)) && region.Contains(position.Move(Direction.Left)) && !region.Contains(position.Move(Direction.Down).Move(Direction.Left))),
                (region.Contains(position.Move(Direction.Up)) && region.Contains(position.Move(Direction.Left)) && !region.Contains(position.Move(Direction.Up).Move(Direction.Left)))

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

        foreach (Direction direction in Directions)
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

        foreach (Direction newDirection in Directions)
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