using System.Diagnostics;
using ConsoleTableExt;

namespace AdventOfCode.Solutions;

/// <summary>
/// The base solution class for a day's puzzle.
/// </summary>
/// <param name="day">The day of the puzzle.</param>
internal abstract class SolutionBase(int year, int day, bool debug = false)
{
    protected int Year => year;
    protected int Day => day;
    protected string Input => LoadInput();
    private bool Debug => debug;

    /// <summary>
    /// Calculate the answers for the day's puzzle.
    /// </summary>
    public void Calculate()
    {
        long elapsedTimeForPartOne = GetElapsedTimeForSolution(SolvePartOne, out object answerOne);
        long elapsedTimeForPartTwo = GetElapsedTimeForSolution(SolvePartTwo, out object answerTwo);

        List<List<object>> tableData = [
            [$"{Year}/12/{Day:D2}", "Answer", "Elapsed Time (ms)"],
            ["Part One", answerOne, $"{elapsedTimeForPartOne} ms"],
            ["Part Two", answerTwo, $"{elapsedTimeForPartTwo} ms"]
        ];

        ConsoleTableBuilder.From(tableData).WithTitle("Advent of Code").WithFormat(ConsoleTableBuilderFormat.Default).ExportAndWriteLine();
    }

    /// <summary>
    /// Solve part one of the day's puzzle.
    /// </summary>
    /// <returns>The answer to part one.</returns>
    public abstract object SolvePartOne();

    /// <summary>
    /// Solve part two of the day's puzzle.
    /// </summary>
    /// <returns>The answer to part two.</returns>
    public abstract object SolvePartTwo();

    private string LoadInput()
    {
        if (Debug) return string.Empty;

        return InputProvider.Fetch(Year, Day).Result;
    }

    /// <summary>
    /// Get the elapsed time for a solution.
    /// </summary>
    /// <param name="solutionDelegate">A <see cref="Func{TResult}"/> delegate.</param>
    /// <param name="answer">The answer from the delegate.</param>
    /// <returns>The elapsed time.</returns>
    private static long GetElapsedTimeForSolution(Func<object> solutionDelegate, out object answer)
    {
        Stopwatch stopwatch = new();

        stopwatch.Start();

        answer = solutionDelegate();

        stopwatch.Stop();

        return stopwatch.ElapsedMilliseconds;
    }
}
