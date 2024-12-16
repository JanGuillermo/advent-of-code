using AdventOfCode.Solutions.Objects;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day12;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/12">
/// Day 12: Garden Groups.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private Dictionary<Position, char> Map = [];
    private int MapRows = 0;
    private int MapCols = 0;

    public Solution() : base(2024, 12) { }

    public override object SolvePartOne()
    {
        return GetRegions(Map, MapRows, MapCols).Sum(CalculatePriceBasedOnPerimeter);
    }

    public override object SolvePartTwo()
    {
        return GetRegions(Map, MapRows, MapCols).Sum(CalculatePriceBasedOnSides);
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

    private static List<List<Position>> GetRegions(Dictionary<Position, char> map, int mapRows, int mapCols)
    {
        List<List<Position>> regions = [];
        HashSet<Position> visited = [];

        foreach (Position position in map.Keys)
        {
            if (visited.Add(position))
            {
                HashSet<Position> region = PlotRegion(map, mapRows, mapCols, map[position], position);

                visited.UnionWith(region);
                regions.Add(region.ToList());
            }
        }

        return regions;
    }

    private static HashSet<Position> PlotRegion(Dictionary<Position, char> map, int mapRows, int mapCols, char plot, Position position)
    {
        HashSet<Position> plotPositions = [position];
        Stack<Position> stack = new([position]);

        while (stack.Count > 0)
        {
            Position currentPosition = stack.Pop();

            foreach (Direction direction in Direction.Orthogonal)
            {
                Position newPosition = currentPosition.Move(direction);

                if (newPosition.IsInBounds(mapRows, mapCols)
                    && map[newPosition] == plot
                    && plotPositions.Add(newPosition))
                {
                    stack.Push(newPosition);
                }
            }
        }

        return plotPositions;
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
                Map[new Position(row, col)] = lines[row][col];
            }
        }
    }
}
