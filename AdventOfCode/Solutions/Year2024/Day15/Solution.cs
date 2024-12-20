using AdventOfCode.Solutions.Objects;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day15;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/15">
/// Day 15: Warehouse Woes.
/// </see>
/// </summary>
internal class Solution() : SolutionBase(2024, 15)
{
    private char[] Instructions = [];
    private string[] GridLines = [];

    public override object SolvePartOne()
    {
        (List<Position> obstacles, List<Position> boxes, Position start) = Initialize();
        PerformInstructions(Instructions, start, obstacles, boxes);

        return boxes.Sum(box => 100 * box.Row + box.Col);
    }

    public override object SolvePartTwo()
    {
        (List<Position> obstacles, List<Position> boxes, Position start) = Initialize();
        Upscale(start, obstacles, boxes, out start, out List<(Position Left, Position Right)> bigBoxes);
        PerformInstructions(Instructions, start, obstacles, bigBoxes);

        return bigBoxes.Select(box => box.Left).Sum(box => 100 * box.Row + box.Col);
    }

    private (List<Position> obstacles, List<Position> boxes, Position start) Initialize()
    {
        List<Position> obstacles = [];
        List<Position> boxes = [];

        ProcessMap(GridLines, obstacles, boxes, out Position start);

        return (obstacles, boxes, start);
    }

    private static void PerformInstructions(char[] instructions, Position current, List<Position> obstacles, List<Position> boxes)
    {
        foreach (char instruction in instructions)
        {
            Direction direction = Direction.GetDirection(instruction);
            Position nextPosition = current.Move(direction);

            if (obstacles.Contains(nextPosition) || !TryMoveBoxes(nextPosition, direction, obstacles, boxes))
            {
                continue;
            }

            current = nextPosition;
        }
    }

    private static bool TryMoveBoxes(Position current, Direction direction, List<Position> obstacles, List<Position> boxes)
    {
        List<Position> moveableBoxes = [];

        while (boxes.Contains(current))
        {
            moveableBoxes.Add(current);
            current = current.Move(direction);

            if (obstacles.Contains(current))
            {
                return false;
            }
        }

        foreach (Position box in moveableBoxes)
        {
            boxes.Remove(box);
            boxes.Add(box.Move(direction));
        }

        return true;
    }

    private static void PerformInstructions(char[] instructions, Position current, List<Position> obstacles, List<(Position, Position)> bigBoxes)
    {
        foreach (char instruction in instructions)
        {
            Direction direction = Direction.GetDirection(instruction);
            Position nextPosition = current.Move(direction);

            if (obstacles.Contains(nextPosition) || !TryMoveBoxes(nextPosition, direction, obstacles, bigBoxes))
            {
                continue;
            }

            current = nextPosition;
        }
    }

    private static bool TryMoveBoxes(Position current, Direction direction, List<Position> obstacles, List<(Position Left, Position Right)> bigBoxes)
    {
        HashSet<(Position Left, Position Right)> moveableBigBoxes = [];

        if (direction == Direction.North || direction == Direction.South)
        {
            Queue<Position> boxChecks = new([current]);

            while (boxChecks.Count > 0)
            {
                Position boxCheck = boxChecks.Dequeue();

                if (obstacles.Contains(boxCheck))
                {
                    return false;
                }

                (Position Left, Position Right) bigBox = bigBoxes.FirstOrDefault(box => box.Left == boxCheck || box.Right == boxCheck);

                if (bigBox == default || !moveableBigBoxes.Add(bigBox))
                {
                    continue;
                }

                boxChecks.Enqueue(bigBox.Left.Move(direction));
                boxChecks.Enqueue(bigBox.Right.Move(direction));
            }
        }
        else
        {
            while (bigBoxes.Any(box => box.Left == current || box.Right == current))
            {
                (Position Left, Position Right) bigBox = bigBoxes.First(box => box.Left == current || box.Right == current);
                moveableBigBoxes.Add(bigBox);
                current = current.Move(direction).Move(direction);

                if (obstacles.Contains(current))
                {
                    return false;
                }
            }
        }

        bigBoxes.RemoveAll(box => moveableBigBoxes.Contains(box));
        bigBoxes.AddRange(moveableBigBoxes.Select(box => (box.Left.Move(direction), box.Right.Move(direction))));

        return true;
    }

    private static void Upscale(Position current, List<Position> obstacles, List<Position> boxes, out Position start, out List<(Position, Position)> bigBoxes)
    {
        start = current.Move(0, current.Col);
        bigBoxes = [];

        List<Position> newObstacles = [];

        for (int obstacleIndex = 0; obstacleIndex < obstacles.Count; obstacleIndex++)
        {
            Position obstacle = obstacles[obstacleIndex];
            obstacles[obstacleIndex] = obstacle.Move(0, obstacle.Col);
            newObstacles.Add(obstacle.Move(0, obstacle.Col + 1));
        }

        obstacles.AddRange(newObstacles);
        bigBoxes.AddRange(boxes.Select(box => (box.Move(0, box.Col), box.Move(0, box.Col + 1))));
    }

    private static void ProcessMap(string[] gridLines, List<Position> obstacles, List<Position> boxes, out Position start)
    {
        start = Position.Default;

        for (int row = 0; row < gridLines.Length; row++)
        {
            for (int col = 0; col < gridLines[row].Length; col++)
            {
                Position position = new(row, col);

                switch (gridLines[row][col])
                {
                    case '#':
                        obstacles.Add(position);
                        break;
                    case 'O':
                        boxes.Add(position);
                        break;
                    case '@':
                        start = position;
                        break;
                }
            }
        }
    }

    protected override void ProcessInput()
    {
        string[] sections = InputUtils.SplitIntoSections(Input);
        GridLines = InputUtils.SplitIntoLines(sections[0]);
        Instructions = InputUtils.RemoveLineBreaks(sections[1]).ToCharArray();
    }
}
