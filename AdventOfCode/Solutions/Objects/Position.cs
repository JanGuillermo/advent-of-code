namespace AdventOfCode.Solutions.Objects;

internal record Position(int Row, int Col)
{
    public Position Move(Direction direction) => Move(direction.RowOffset, direction.ColOffset);
    public Position Move(int row, int col) => new Position(Row + row, Col + col);
    public bool IsInBounds(int rows, int cols) => Row >= 0 && Row < rows && Col >= 0 && Col < cols;

    public static Position Default => new(0, 0);
}
