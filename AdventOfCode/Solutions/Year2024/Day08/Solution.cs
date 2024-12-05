namespace AdventOfCode.Solutions.Year2024.Day08;

/// <summary>
/// Day 8: Resonant Collinearity.
/// <see cref="https://adventofcode.com/2024/day/8"/>
/// </summary>
internal class Solution : SolutionBase
{
    public Dictionary<Position, char> Map = new();
    public Dictionary<char, List<Position>> Frequencies = new();
    public int MapWidth = 0;
    public int MapHeight = 0;

    public const char Empty = '.';

    public Solution() : base(2024, 08)
    {
        string[] lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                char cellValue = lines[row][col];
                Position position = new Position(row, col);
                Map[position] = cellValue;

                if (cellValue != Empty)
                {
                    if (!Frequencies.TryGetValue(cellValue, out List<Position>? positions))
                    {
                        positions = new List<Position>();
                        Frequencies[cellValue] = positions;
                    }

                    positions.Add(position);
                }
            }
        }

        MapWidth = lines[0].Length;
        MapHeight = lines.Length;
    }

    public override object SolvePartOne()
    {
        HashSet<Position> antinodes = new();

        foreach ((char frequency, List<Position> positions) in Frequencies)
        {
            for (int i = 0; i < positions.Count - 1; i++)
            {
                Position position = positions[i];

                for (int j = i + 1; j < positions.Count; j++)
                {
                    Position otherPosition = positions[j];

                    int rowDiff = position.Row - otherPosition.Row;
                    int colDiff = position.Col - otherPosition.Col;

                    AddAntinodeIfValid(antinodes, position, rowDiff, colDiff, frequency);
                    AddAntinodeIfValid(antinodes, otherPosition, -rowDiff, -colDiff, frequency);
                }
            }
        }

        return antinodes.Count;
    }

    public override object SolvePartTwo()
    {
        HashSet<Position> antinodes = new();

        foreach ((char frequency, List<Position> positions) in Frequencies)
        {
            for (int i = 0; i < positions.Count - 1; i++)
            {
                Position position = positions[i];

                for (int j = i + 1; j < positions.Count; j++)
                {
                    Position otherPosition = positions[j];

                    int rowDiff = position.Row - otherPosition.Row;
                    int colDiff = position.Col - otherPosition.Col;

                    AddAntinodes(antinodes, position, rowDiff, colDiff);
                    AddAntinodes(antinodes, position, -rowDiff, -colDiff);
                    AddAntinodes(antinodes, otherPosition, -rowDiff, -colDiff);
                    AddAntinodes(antinodes, otherPosition, rowDiff, colDiff);
                }
            }
        }

        return antinodes.Count;
    }

    private void AddAntinodeIfValid(HashSet<Position> antinodes, Position position, int rowDiff, int colDiff, char frequency)
    {
        Position antiNode = position.Move(rowDiff, colDiff);
        if (antiNode.IsInBounds(MapHeight, MapWidth) && Map[antiNode] != frequency)
        {
            antinodes.Add(antiNode);
        }
    }

    private void AddAntinodes(HashSet<Position> antinodes, Position start, int rowDiff, int colDiff)
    {
        Position antiNode = start.Move(rowDiff, colDiff);

        while (antiNode.IsInBounds(MapHeight, MapWidth))
        {
            antinodes.Add(antiNode);
            antiNode = antiNode.Move(rowDiff, colDiff);
        }
    }
}

internal record Position(int Row, int Col)
{
    public Position Move(int row, int col) => new Position(Row + row, Col + col);
    public bool IsInBounds(int rows, int cols) => Row >= 0 && Row < rows && Col >= 0 && Col < cols;
}