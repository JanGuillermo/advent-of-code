using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2024.Day01;

/// <summary>
/// <see href="https://adventofcode.com/2024/day/1">
/// Day 1: Historian Hysteria.
/// </see>
/// </summary>
internal class Solution : SolutionBase
{
    private List<int> Locations1 = [];
    private List<int> Locations2 = [];

    public Solution() : base(2024, 01) { }

    public override object SolvePartOne()
    {
        return Locations1
            .Zip(Locations2, (location1, location2) => Math.Abs(location1 - location2))
            .Sum();
    }

    public override object SolvePartTwo()
    {
        Dictionary<int, int> frequencies = Locations2
            .GroupBy(value => value)
            .ToDictionary(group => group.Key, group => group.Count());

        return Locations1.Sum(value => frequencies.GetValueOrDefault(value) * value);
    }

    protected override void ProcessInput()
    {
        foreach (Match match in new Regex(@"(\d{5})   (\d{5})").Matches(Input))
        {
            Locations1.Add(int.Parse(match.Groups[1].Value));
            Locations2.Add(int.Parse(match.Groups[2].Value));
        }

        Locations1.Sort();
        Locations2.Sort();
    }
}