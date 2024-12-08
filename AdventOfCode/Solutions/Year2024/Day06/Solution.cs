﻿namespace AdventOfCode.Solutions.Year2024.Day06;

/// <summary>
/// Day 6: Guard Gallivant.
/// <see cref="https://adventofcode.com/2024/day/6"/>
/// </summary>
internal class Solution : SolutionBase
{
    Map Map;
    public static char GUARD_INDICATOR = '^';
    public static char OBSTACLE = '#';

    public Solution() : base(2024, 06)
    {
        Map = new Map(Input
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.ToCharArray().ToList())
            .ToList());
    }

    public override object SolvePartOne()
    {
        Position guardPosition = GetInitialGuardPosition();
        Direction guardDirection = Direction.Up;
        HashSet<Position> visitedPositions = [];

        // Mark the guard's initial position as visited.
        int distinctPositions = 1;
        Position newPosition = guardPosition.Move(guardDirection);

        // Keeps going till the guard is out of bounds
        while (!newPosition.OutOfBounds(Map.Size))
        {
            if (Map.Grid[newPosition.Row][newPosition.Col] == OBSTACLE)
            {
                guardDirection = guardDirection.TurnRight();
            }
            else
            {
                guardPosition = newPosition;

                if (visitedPositions.Add(guardPosition))
                {
                    distinctPositions++;
                }
            }

            newPosition = guardPosition.Move(guardDirection);
        }

        return distinctPositions;
    }

    public override object SolvePartTwo()
    {
        Position guardPosition = GetInitialGuardPosition();
        Direction guardDirection = Direction.Up;
        HashSet<Position> visitedPositions = [];

        Position newPosition = guardPosition.Move(guardDirection);

        // Keeps going till the guard is out of bounds
        while (!newPosition.OutOfBounds(Map.Size))
        {
            if (Map.Grid[newPosition.Row][newPosition.Col] == OBSTACLE)
            {
                guardDirection = guardDirection.TurnRight();
            }
            else
            {
                guardPosition = newPosition;
                visitedPositions.Add(guardPosition);
            }

            newPosition = guardPosition.Move(guardDirection);
        }

        // Let's place an obstacle in every visited position and see if
        // we can get the guard stuck in a loop
        HashSet<(Position, Direction)> visitedPositionsAndDirections = [];
        int obstructions = 0;

        foreach (Position visitedPosition in visitedPositions)
        {
            guardPosition = GetInitialGuardPosition();
            guardDirection = Direction.Up;

            char currentValue = Map.Grid[visitedPosition.Row][visitedPosition.Col];
            Map.Grid[visitedPosition.Row][visitedPosition.Col] = OBSTACLE;

            visitedPositionsAndDirections.Clear();
            visitedPositionsAndDirections.Add((guardPosition, guardDirection));

            while (!guardPosition.OutOfBounds(Map.Size))
            {
                newPosition = guardPosition.Move(guardDirection);

                if (!newPosition.OutOfBounds(Map.Size) && Map.Grid[newPosition.Row][newPosition.Col] == OBSTACLE)
                {
                    guardDirection = guardDirection.TurnRight();
                }
                else
                {
                    guardPosition = newPosition;

                    if (!visitedPositionsAndDirections.Add((guardPosition, guardDirection)))
                    {
                        obstructions++;
                        break;
                    }
                }
            }

            Map.Grid[visitedPosition.Row][visitedPosition.Col] = currentValue;
        }

        return obstructions;
    }

    private Position GetInitialGuardPosition()
    {
        for (int row = 0; row < Map.Size; row++)
        {
            for (int col = 0; col < Map.Size; col++)
            {
                char guard = Map.Grid[row][col];

                if (Map.Grid[row][col] == GUARD_INDICATOR)
                {
                    return new Position(row, col);
                }
            }
        }

        return new Position(-1, -1);
    }
}

internal record Map(List<List<char>> Grid)
{
    public int Size => Grid.Count;
}

internal record Direction(int Row, int Col)
{
    public Direction TurnRight() => new(Col, -Row);

    public static readonly Direction Up = new(-1, 0);
    public static readonly Direction Right = new(0, 1);
    public static readonly Direction Down = new(1, 0);
    public static readonly Direction Left = new(0, -1);
}

internal record Position(int Row, int Col)
{
    public Position Move(Direction direction) => new(Row + direction.Row, Col + direction.Col);
    public bool OutOfBounds(int mapSize) => Row < 0 || Col < 0 || Row >= mapSize || Col >= mapSize;
}