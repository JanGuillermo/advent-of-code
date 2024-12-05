using Common;
using static _2015.Day01.Constants;

namespace _2015.Day01;

/// <summary>
/// Day 1: Not Quite Lisp.
/// <see cref="https://adventofcode.com/2015/day/1"/>
/// </summary>
internal class Solution : BaseSolution
{
    public string buildingInstructions;

    public Solution() : base(2015, 1)
    {
        InputParser.Execute(InputPath, out buildingInstructions);
    }

    public override int SolvePartOne()
    {
        return buildingInstructions.Count(ch => ch == Up) - buildingInstructions.Count(ch => ch == Down);
    }

    public override int SolvePartTwo()
    {
        int position = 0;
        int currentFloor = 0;

        while (currentFloor != -1)
        {
            currentFloor += buildingInstructions[position] == Up ? 1 : -1;
            position++;
        }

        return position;
    }
}