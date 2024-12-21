using AdventOfCode.Solutions.Objects;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day20;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/20">
/// Day 20: Race Condition.
/// </see>
/// </summary>
internal class Solution() : SolutionBase(2024, 20)
{
    private readonly List<Position> Obstacles = [];
    private Position Start = Position.Default;
    private Position End = Position.Default;

    public override object SolvePartOne()
    {
        return Solve(Obstacles, Start, End, nanoseconds: 2);
    }

    public override object SolvePartTwo()
    {
        return Solve(Obstacles, Start, End, nanoseconds: 20);
    }

    private static int Solve(List<Position> obstacles, Position start, Position end, int nanoseconds)
    {
        List<(Position Position, int StepCount)> optimalPath = FindOptimalPath(obstacles, start, end);
        List<int> cheats = [];

        for (int i = 0; i < optimalPath.Count - 3; i++)
        {
            for (int j = i + 3; j < optimalPath.Count; j++)
            {
                int difference = Math.Abs(optimalPath[j].Position.Row - optimalPath[i].Position.Row)
                                 + Math.Abs(optimalPath[j].Position.Col - optimalPath[i].Position.Col);

                if (difference <= nanoseconds)
                {
                    cheats.Add(optimalPath[j].StepCount - optimalPath[i].StepCount - difference);
                }
            }
        }

        return cheats.Count(reducedTime => reducedTime >= 100);
    }

    private static List<(Position, int)> FindOptimalPath(List<Position> obstacles, Position start, Position end)
    {
        Queue<(Position, int)> queue = new([(start, 0)]);
        HashSet<Position> visited = [start];
        Dictionary<Position, Position> parentMap = [];

        while (queue.Count > 0)
        {
            (Position current, int steps) = queue.Dequeue();

            if (current == end)
            {
                List<(Position, int)> path = [];

                for (Position backtrack = end; !backtrack.Equals(start); backtrack = parentMap[backtrack])
                {
                    path.Add((backtrack, steps--));
                }

                path.Add((start, 0));
                path.Reverse();

                return path;
            }

            foreach (Position neighbor in GetNeighbors(current).Where(neighbor => !obstacles.Contains(neighbor) && visited.Add(neighbor)))
            {
                queue.Enqueue((neighbor, steps + 1));
                parentMap[neighbor] = current;
            }
        }

        return [];
    }

    private static List<Position> GetNeighbors(Position position)
    {
        return Direction.Orthogonal.Select(position.Move).ToList();
    }

    protected override void ProcessInput()
    {
        string[] lines = InputUtils.SplitIntoLines(Input);
        int rows = lines.Length;
        int cols = lines[0].Length;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Position pos = new Position(row, col);

                if (lines[row][col] == '#')
                {
                    Obstacles.Add(pos);
                }
                else if (lines[row][col] == 'S')
                {
                    Start = pos;
                }
                else if (lines[row][col] == 'E')
                {
                    End = pos;
                }
            }
        }
    }
}
