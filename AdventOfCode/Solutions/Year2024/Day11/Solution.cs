namespace AdventOfCode.Solutions.Year2024.Day11;

/// <summary>
/// Day 11: Plutonian Pebbles.
/// <see cref="https://adventofcode.com/2024/day/11"/>
/// </summary>
internal class Solution : SolutionBase
{
    List<long> stones = [];
    Dictionary<(long, int), long> memo = [];

    public Solution() : base(2024, 11)
    {
        ProcessInput(Input);
    }

    public override object SolvePartOne()
    {
        return stones.Sum(stone => Blink(stone, 25));
    }

    public override object SolvePartTwo()
    {
        return stones.Sum(stone => Blink(stone, 75));
    }

    private long Blink(long stone, int blinks)
    {
        if (blinks == 0) return 1;
        if (stone == 0) return Blink(1, blinks - 1);
        if (memo.TryGetValue((stone, blinks), out long result)) return result;

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

        memo[(stone, blinks)] = result;

        return result;
    }

    private void ProcessInput(string input)
    {
        stones = input.Trim().Split(" ").Select(long.Parse).ToList();
    }
}
