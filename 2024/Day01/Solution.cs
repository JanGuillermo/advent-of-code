using Common;

namespace _2024.Day01;

/// <summary>
/// Day 1: Historian Hysteria.
/// <see cref="https://adventofcode.com/2024/day/1"/>
/// </summary>
internal class Solution : BaseSolution
{
    public List<int> Locations1 = [];
    public List<int> Locations2 = [];

    public Solution() : base(2024, 1)
    {
        InputParser.Execute(InputPath, out Locations1, out Locations2);
    }

    public override int SolvePartOne()
    {
        return Locations1.Zip(Locations2, (location1, location2) => Math.Abs(location1 - location2)).Sum();
    }

    public override int SolvePartTwo()
    {
        Dictionary<int, int> frequencies = Locations2.GroupBy(value => value).ToDictionary(group => group.Key, group => group.Count());

        return Locations1.Select(value => frequencies.GetValueOrDefault(value) * value).Sum();
    }
}