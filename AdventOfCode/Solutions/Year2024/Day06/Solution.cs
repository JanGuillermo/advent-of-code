using AdventOfCode.Solutions.Objects;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day06;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/6">
/// Day 6: Guard Gallivant.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private const char GUARD_INDICATOR = '^';
    private const char OBSTACLE = '#';

    private Dictionary<Position, char> Map = [];
    private int MapRows = 0;
    private int MapCols = 0;

    public Solution() : base(2024, 06) { }

    public override object SolvePartOne()
    {
        return GetVisitedPositions(Map, MapRows, MapCols).Count;
    }

    public override object SolvePartTwo()
    {
        return GetVisitedPositions(Map, MapRows, MapCols)
            .Skip(1)
            .Where(position => WillResultInALoop(Map, MapRows, MapCols, position))
            .Count();
    }

    private static HashSet<Position> GetVisitedPositions(Dictionary<Position, char> map, int mapRows, int mapCols)
    {
        Position guardPosition = map.First(cell => cell.Value == GUARD_INDICATOR).Key;
        Position nextPosition = guardPosition;
        Direction guardDirection = Direction.North;
        HashSet<Position> visitedPositions = [guardPosition];

        while ((nextPosition = guardPosition.Move(guardDirection)).IsInBounds(mapRows, mapCols))
        {
            if (map[nextPosition] == OBSTACLE)
            {
                guardDirection = guardDirection.TurnRight();
            }
            else
            {
                guardPosition = nextPosition;
                visitedPositions.Add(guardPosition);
            }
        }

        return visitedPositions;
    }

    private static bool WillResultInALoop(Dictionary<Position, char> map, int mapRows, int mapCols, Position newObstacle)
    {
        Dictionary<Position, char> updatedMap = new(map) { [newObstacle] = OBSTACLE };
        Position guardPosition = updatedMap.First(cell => cell.Value == GUARD_INDICATOR).Key;
        Position nextPosition = guardPosition;
        Direction guardDirection = Direction.North;
        HashSet<(Position, Direction)> visitedPositionsAndDirections = [(guardPosition, guardDirection)];

        while ((nextPosition = guardPosition.Move(guardDirection)).IsInBounds(mapRows, mapCols))
        {
            if (updatedMap[nextPosition] == OBSTACLE)
            {
                guardDirection = guardDirection.TurnRight();
            }
            else
            {
                guardPosition = nextPosition;

                if (!visitedPositionsAndDirections.Add((guardPosition, guardDirection)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected override void ProcessInput()
    {
        InputUtils.SplitIntoLines(Input)
            .Select((line, row) => line.Select((cell, col) => (new Position(row, col), cell)))
            .SelectMany(x => x)
            .ToList()
            .ForEach(x => Map[x.Item1] = x.Item2);

        MapRows = Map.Max(cell => cell.Key.Row) + 1;
        MapCols = Map.Max(cell => cell.Key.Col) + 1;
    }
}
