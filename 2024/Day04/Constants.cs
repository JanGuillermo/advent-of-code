namespace _2024.Day04;

internal class Constants
{
    private readonly static (int x, int y) NORTHWEST = new(-1, -1);
    private readonly static (int x, int y) NORTH = new(0, -1);
    private readonly static (int x, int y) NORTHEAST = new(1, -1);
    private readonly static (int x, int y) WEST = new(-1, 0);
    private readonly static (int x, int y) EAST = new(1, 0);
    private readonly static (int x, int y) SOUTHWEST = new(-1, 1);
    private readonly static (int x, int y) SOUTH = new(0, 1);
    private readonly static (int x, int y) SOUTHEAST = new(1, 1);
    public readonly static List<(int x, int y)> DIRECTIONS = [NORTHWEST, NORTH, NORTHEAST, WEST, EAST, SOUTHWEST, SOUTH, SOUTHEAST];

    public readonly static char X = 'X';
    public readonly static char M = 'M';
    public readonly static char A = 'A';
    public readonly static char S = 'S';

    public readonly static string XMAS = "XMAS";
}
