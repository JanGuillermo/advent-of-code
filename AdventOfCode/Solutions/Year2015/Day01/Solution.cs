namespace AdventOfCode.Solutions.Year2015.Day01;

/// <summary>
/// Day 1: Not Quite Lisp.
/// <see cref="https://adventofcode.com/2015/day/1"/>
/// </summary>
internal class Solution : SolutionBase
{
    public const char Up = '(';
    public const char Down = ')';

    public Solution() : base(2015, 01) { }

    public override object SolvePartOne()
    {
        return Input.Count(ch => ch == Up) - Input.Count(ch => ch == Down);
    }

    public override object SolvePartTwo()
    {
        int position = 0;
        int currentFloor = 0;

        while (currentFloor != -1)
        {
            currentFloor += Input[position] == Up ? 1 : -1;
            position++;
        }

        return position;
    }
}