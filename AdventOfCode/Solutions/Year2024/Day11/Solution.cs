namespace AdventOfCode.Solutions.Year2024.Day11;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/11">
/// Day 11: Plutonian Pebbles.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private List<long> Stones = [];
    private Dictionary<(long, int), long> Memo = [];

    public Solution() : base(2024, 11) { }

    public override object SolvePartOne()
    {
        return Stones.Sum(stone => Blink(stone, 25));
    }

    public override object SolvePartTwo()
    {
        return Stones.Sum(stone => Blink(stone, 75));
    }

    private long Blink(long stone, int blinks)
    {
        if (Memo.TryGetValue((stone, blinks), out long result)) return result;
        if (blinks == 0) return 1;
        if (stone == 0) return Blink(1, blinks - 1);

        string stoneStr = stone.ToString();

        if (stoneStr.Length % 2 == 0)
        {
            int mid = stoneStr.Length / 2;
            long leftHalf = long.Parse(stoneStr.Substring(0, mid));
            long rightHalf = long.Parse(stoneStr.Substring(mid));

            result = Blink(leftHalf, blinks - 1) + Blink(rightHalf, blinks - 1);
        }
        else
        {
            result = Blink(stone * 2024, blinks - 1);
        }

        Memo[(stone, blinks)] = result;

        return result;
    }

    protected override void ProcessInput()
    {
        Stones = Input.Split(" ").Select(long.Parse).ToList();
    }
}
