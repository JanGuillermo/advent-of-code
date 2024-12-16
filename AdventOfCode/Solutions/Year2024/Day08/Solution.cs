using AdventOfCode.Solutions.Objects;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day08;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/8">
/// Day 8: Resonant Collinearity.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private const char EMPTY = '.';

    private Dictionary<Position, char> Map = [];
    private Dictionary<char, List<Position>> Frequencies = [];
    private int MapCols = 0;
    private int MapRows = 0;

    public Solution() : base(2024, 08, true) { }

    public override object SolvePartOne()
    {
        return Frequencies
            .SelectMany(frequency => FindAntinodes(Map, frequency.Key, frequency.Value, MapRows, MapCols))
            .Distinct()
            .Count();
    }

    public override object SolvePartTwo()
    {
        return Frequencies
            .SelectMany(frequency => FindAntinodes(Map, frequency.Key, frequency.Value, MapRows, MapCols, isUpdatedModel: true))
            .Distinct()
            .Count();
    }

    private static HashSet<Position> FindAntinodes(Dictionary<Position, char> map, char frequency, List<Position> positions, int mapRows, int mapCols, bool isUpdatedModel = false)
    {
        HashSet<Position> antinodes = [];

        for (int i = 0; i < positions.Count - 1; i++)
        {
            Position position = positions[i];

            for (int j = i + 1; j < positions.Count; j++)
            {
                Position otherPosition = positions[j];

                int rowDiff = position.Row - otherPosition.Row;
                int colDiff = position.Col - otherPosition.Col;

                if (isUpdatedModel)
                {
                    AddAntinodes(mapRows, mapCols, antinodes, position, rowDiff, colDiff);
                    AddAntinodes(mapRows, mapCols, antinodes, position, -rowDiff, -colDiff);
                    AddAntinodes(mapRows, mapCols, antinodes, otherPosition, -rowDiff, -colDiff);
                    AddAntinodes(mapRows, mapCols, antinodes, otherPosition, rowDiff, colDiff);
                }
                else
                {
                    AddAntinodeIfValid(map, mapRows, mapCols, antinodes, position, rowDiff, colDiff, frequency);
                    AddAntinodeIfValid(map, mapRows, mapCols, antinodes, otherPosition, -rowDiff, -colDiff, frequency);
                }
            }
        }

        return antinodes;
    }

    private static void AddAntinodeIfValid(Dictionary<Position, char> map, int mapRows, int mapCols, HashSet<Position> antinodes, Position start, int rowDiff, int colDiff, char frequency)
    {
        Position antiNode = start.Move(rowDiff, colDiff);

        if (antiNode.IsInBounds(mapRows, mapCols) && map[antiNode] != frequency)
        {
            antinodes.Add(antiNode);
        }
    }

    private static void AddAntinodes(int mapRows, int mapCols, HashSet<Position> antinodes, Position start, int rowDiff, int colDiff)
    {
        Position antiNode = start.Move(rowDiff, colDiff);

        while (antiNode.IsInBounds(mapRows, mapCols))
        {
            antinodes.Add(antiNode);
            antiNode = antiNode.Move(rowDiff, colDiff);
        }
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
                char cellValue = lines[row][col];
                Position position = new Position(row, col);
                Map[position] = cellValue;

                if (cellValue != EMPTY)
                {
                    if (!Frequencies.TryGetValue(cellValue, out List<Position>? positions))
                    {
                        positions = [];
                        Frequencies[cellValue] = positions;
                    }

                    positions.Add(position);
                }
            }
        }
    }
}