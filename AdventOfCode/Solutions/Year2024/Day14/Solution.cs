using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Utils;

namespace AdventOfCode.Solutions.Year2024.Day14;

/// <summary>
/// Day 14: Restroom Redoubt
/// <see cref="https://adventofcode.com/2024/day/14"/>
/// </summary>
internal class Solution : SolutionBase
{
    readonly List<Robot> Robots = [];
    readonly int MapWidth = 101;
    readonly int MapHeight = 103;
    readonly int SecondsElapsed = 100;

    public Solution() : base(2024, 14) { }

    public override object SolvePartOne()
    {
        return Robots
            .GroupBy(robot => GetQuadrant(robot, MapWidth, MapHeight, SecondsElapsed))
            .Where(group => group.Key != 0)
            .Select(group => group.Count())
            .Aggregate(1, (product, count) => product * count);
    }

    public override object SolvePartTwo()
    {
        return CalculateElapsedTimeForEasterEgg(Robots, MapWidth, MapHeight);
    }

    private static int GetQuadrant(Robot robot, int mapWidth, int mapHeight, int secondsElapsed)
    {
        Position finalPosition = GetFinalPosition(robot, mapWidth, mapHeight, secondsElapsed);
        int middleX = (mapWidth - 1) / 2;
        int middleY = (mapHeight - 1) / 2;

        if (finalPosition.X == middleX || finalPosition.Y == middleY)
        {
            return 0;
        }

        return (finalPosition.X < middleX, finalPosition.Y < middleY) switch
        {
            (true, true) => 1,
            (false, true) => 2,
            (true, false) => 3,
            (false, false) => 4,
        };
    }

    private static int CalculateElapsedTimeForEasterEgg(List<Robot> robots, int mapWidth, int mapHeight)
    {
        int bestX = Enumerable
            .Range(0, mapWidth)
            .MinBy(secondsElapsed => MathUtils.Variance(robots.Select(robot => GetFinalPosition(robot, mapWidth, mapHeight, secondsElapsed).X)));
        int bestY = Enumerable
            .Range(0, mapHeight)
            .MinBy(secondsElapsed => MathUtils.Variance(robots.Select(robot => GetFinalPosition(robot, mapWidth, mapHeight, secondsElapsed).Y)));

        return bestX + MathUtils.PositiveModulo(MathUtils.ModInverse(mapWidth, mapHeight) * (bestY - bestX), mapHeight) * mapWidth;
    }

    private static Position GetFinalPosition(Robot robot, int mapWidth, int mapHeight, int secondsElapsed)
    {
        int x = MathUtils.PositiveModulo(robot.Position.X + (robot.MoveXTiles * secondsElapsed), mapWidth);
        int y = MathUtils.PositiveModulo(robot.Position.Y + (robot.MoveYTiles * secondsElapsed), mapHeight);

        return new Position(x, y);
    }

    protected override void ProcessInput()
    {
        string[] lines = Input.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries);
        Regex regex = new(@"p=(?<px>-?\d+),(?<py>-?\d+) v=(?<vx>-?\d+),(?<vy>-?\d+)");

        foreach (string line in lines)
        {
            Match match = regex.Match(line);
            Position position = new Position(int.Parse(match.Groups["px"].Value), int.Parse(match.Groups["py"].Value));
            Robots.Add(new Robot(position, int.Parse(match.Groups["vx"].Value), int.Parse(match.Groups["vy"].Value)));
        }
    }
}

internal record Robot(Position Position, int MoveXTiles, int MoveYTiles)
{
    public Robot Move(Position newPosition) => new(newPosition, MoveXTiles, MoveYTiles);
}

internal record Position(int X, int Y);