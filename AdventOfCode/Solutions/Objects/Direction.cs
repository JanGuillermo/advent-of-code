﻿namespace AdventOfCode.Solutions.Objects;

internal record Direction(int Row, int Col)
{
    public static Direction GetDirection(char instruction) => instruction switch
    {
        '^' => Direction.North,
        'v' => Direction.South,
        '<' => Direction.West,
        '>' => Direction.East,
        _ => throw new InvalidOperationException("Invalid instruction.")
    };

    public Direction TurnRight() => new(Col, -Row);

    public static Direction North => new(-1, 0);
    public static Direction South => new(1, 0);
    public static Direction West => new(0, -1);
    public static Direction East => new(0, 1);
    public static Direction Northwest => new(-1, -1);
    public static Direction Northeast => new(-1, 1);
    public static Direction Southwest => new(1, -1);
    public static Direction Southeast => new(1, 1);
    public static Direction[] Orthogonal = [North, South, West, East];
    public static Direction[] Diagonal = [Northwest, Northeast, Southwest, Southeast];
    public static Direction[] All = Orthogonal.Concat(Diagonal).ToArray();
}
