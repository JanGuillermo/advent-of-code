using System.Text;

namespace AdventOfCode.Solutions.Year2024.Day04;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/4">
/// Day 4: Ceres Search.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    public Dictionary<Position, char> Puzzle = new();
    public int PuzzleWidth = 0;
    public int PuzzleHeight = 0;

    public readonly static List<Direction> Directions = [
        Direction.North,
        Direction.South,
        Direction.East,
        Direction.West,
        Direction.Northwest,
        Direction.Northeast,
        Direction.Southwest,
        Direction.Southeast
    ];

    public Solution() : base(2024, 04)
    {
        string[] lines = Input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        for (int row = 0; row < lines.Length; row++)
        {
            for (int col = 0; col < lines[row].Length; col++)
            {
                Puzzle[new Position(row, col)] = lines[row][col];
            }
        }

        PuzzleWidth = lines[0].Length;
        PuzzleHeight = lines.Length;
    }

    public override object SolvePartOne()
    {
        return Puzzle
            .Where(cell => cell.Value == 'X')
            .Sum(cell => ScanPuzzleForKeyword(cell.Key));
    }

    public override object SolvePartTwo()
    {
        return Puzzle
            .Where(cell => cell.Key.Col < PuzzleWidth - 2
                && cell.Key.Row < PuzzleHeight - 2
                && IsMASPatternInXShape(cell.Key))
            .Count();
    }

    private int ScanPuzzleForKeyword(Position position)
    {
        int keywordAppearances = 0;
        string keyword = "XMAS";

        foreach (Direction direction in Directions)
        {
            Position currentPosition = position;
            StringBuilder scannedWord = new(keyword[0]);

            while ((currentPosition = currentPosition.Move(direction)).IsInBounds(PuzzleHeight, PuzzleWidth))
            {
                scannedWord.Append(Puzzle[currentPosition]);

                if (scannedWord.Length > keyword.Length || !keyword.StartsWith(scannedWord.ToString()))
                {
                    break;
                }

                if (scannedWord.ToString() == keyword)
                {
                    keywordAppearances++;
                    break;
                }
            }
        }

        return keywordAppearances;
    }

    private bool IsMASPatternInXShape(Position position)
    {
        if (Puzzle[position.Move(Direction.Southeast)] != 'A')
        {
            return false;
        }

        List<char> chars = new()
        {
            Puzzle[position],
            Puzzle[position.Move(Direction.East).Move(Direction.East)],
            Puzzle[position.Move(Direction.South).Move(Direction.South)],
            Puzzle[position.Move(Direction.Southeast).Move(Direction.Southeast)]
        };

        if (chars.Contains('X') || chars.Contains('A') || chars.Count(c => c == 'M') != 2 || chars.Count(c => c == 'S') != 2)
        {
            return false;
        }

        return (chars[0] == chars[1] && chars[2] == chars[3]) || (chars[0] == chars[2] && chars[1] == chars[3]);
    }
}

internal record Direction(int Row, int Col)
{
    public static Direction North => new(-1, 0);
    public static Direction South => new(1, 0);
    public static Direction East => new(0, 1);
    public static Direction West => new(0, -1);
    public static Direction Northwest => new(-1, -1);
    public static Direction Northeast => new(-1, 1);
    public static Direction Southwest => new(1, -1);
    public static Direction Southeast => new(1, 1);
}

internal record Position(int Row, int Col)
{
    public Position Move(Direction direction) => new(Row + direction.Row, Col + direction.Col);
    public bool IsInBounds(int rows, int cols) => Row >= 0 && Row < rows && Col >= 0 && Col < cols;
}