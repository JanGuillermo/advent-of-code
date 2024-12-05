using Common;
using static _2024.Day04.Constants;

/// <summary>
/// Day 4: Ceres Search.
/// <see cref="https://adventofcode.com/2024/day/4"/>
/// </summary>
namespace _2024.Day04;

internal class Solution : BaseSolution
{
    public List<List<char>> Puzzle = [];

    public Solution() : base(2024, 4)
    {
        InputParser.Execute(InputPath, out Puzzle);
    }

    public override int SolvePartOne()
    {
        return Puzzle
            .SelectMany((row, rowIndex) => row.Select((cell, colIndex) => (cell, colIndex, rowIndex)))
            .Where(item => item.cell == X)
            .Sum(item => ScanPuzzleForKeyword((item.colIndex, item.rowIndex)));
    }

    public override int SolvePartTwo()
    {
        return Puzzle
            .SelectMany((row, rowIndex) => row.Select((cell, colIndex) => (cell, colIndex, rowIndex)))
            .Where(item => item.colIndex < Puzzle[0].Count - 2 && item.rowIndex < Puzzle.Count - 2)
            .Where(item => HasTwoMASInAnXShape((item.colIndex, item.rowIndex)))
            .Count();
    }

    private int ScanPuzzleForKeyword((int x, int y) coordinates)
    {
        int keywordAppearances = 0;

        foreach ((int x, int y) direction in DIRECTIONS)
        {
            (int x, int y) currentCoordinates = coordinates;
            string scannedWord = X.ToString();

            while (TryNavigate(currentCoordinates, direction, out (int x, int y) newCoordinates))
            {
                scannedWord += Puzzle[newCoordinates.y][newCoordinates.x];
                currentCoordinates = newCoordinates;

                if (scannedWord == XMAS)
                {
                    keywordAppearances++;
                    break;
                }

                if (!XMAS.StartsWith(scannedWord) || scannedWord.Length >= XMAS.Length)
                {
                    break;
                }
            }
        }

        return keywordAppearances;
    }

    private bool TryNavigate((int x, int y) currentCoordinates, (int x, int y) direction, out (int x, int y) newCoordinates)
    {
        int newX = currentCoordinates.x + direction.x;
        int newY = currentCoordinates.y + direction.y;

        newCoordinates = new(newX, newY);

        return newY >= 0 && newY < Puzzle.Count && newX >= 0 && newX < Puzzle[newY].Count;
    }

    private bool HasTwoMASInAnXShape((int x, int y) currentCoordinates)
    {
        if (Puzzle[currentCoordinates.y + 1][currentCoordinates.x + 1] != A)
        {
            return false;
        }

        List<char> chars = [
            Puzzle[currentCoordinates.y][currentCoordinates.x],
            Puzzle[currentCoordinates.y][currentCoordinates.x + 2],
            Puzzle[currentCoordinates.y + 2][currentCoordinates.x],
            Puzzle[currentCoordinates.y + 2][currentCoordinates.x + 2]
        ];

        if (chars.Contains(X) || chars.Contains(A) || chars.Count(c => c == M) != 2 || chars.Count(c => c == S) != 2)
        {
            return false;
        }

        return (chars[0] == chars[1] && chars[2] == chars[3]) || (chars[0] == chars[2] && chars[1] == chars[3]);
    }
}
