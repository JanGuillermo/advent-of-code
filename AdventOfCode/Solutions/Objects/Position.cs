namespace AdventOfCode.Solutions.Objects;

internal record Position(int Row, int Col)
{
    public Position Move(Direction direction) => Move(direction.Row, direction.Col);
    public Position Move(int row, int col) => new Position(Row + row, Col + col);
}
