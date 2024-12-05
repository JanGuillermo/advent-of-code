using AdventOfCode;
using AdventOfCode.Solutions;

Settings settings = Settings.GetInstance();
Type? solutionType = Type.GetType($"AdventOfCode.Solutions.Year{settings.Year}.Day{settings.Day:D2}.Solution");

if (solutionType != null)
{
    SolutionBase solution = (SolutionBase)Activator.CreateInstance(solutionType)!;

    solution.Calculate();
}
else
{
    Console.WriteLine($"Solution for {settings.Year}/{settings.Day:D2} not found.");
}
