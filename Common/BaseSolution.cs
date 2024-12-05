using System.Diagnostics;
using ConsoleTableExt;

namespace Common;

/// <summary>
/// The base solution class for a day's puzzle.
/// </summary>
/// <param name="day">The day of the puzzle.</param>
public abstract class BaseSolution(int year, int day)
{
    /// <summary>
    /// The year of the puzzle.
    /// </summary>
    protected int Year => year;

    /// <summary>
    /// The day of the puzzle.
    /// </summary>
    protected int Day => day;

    /// <summary>
    /// The path to the input file.
    /// </summary>
    protected string InputPath => $"input.txt";

    /// <summary>
    /// Calculate the answers for the day's puzzle.
    /// </summary>
    public void Calculate()
    {
        long elapsedTimeForPartOne = GetElapsedTimeForSolution(SolvePartOne, out int answerOne);
        long elapsedTimeForPartTwo = GetElapsedTimeForSolution(SolvePartTwo, out int answerTwo);

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
    public abstract int SolvePartOne();

    /// <summary>
    /// Solve part two of the day's puzzle.
    /// </summary>
    /// <returns>The answer to part two.</returns>
    public abstract int SolvePartTwo();

    /// <summary>
    /// Get the elapsed time for a solution.
    /// </summary>
    /// <param name="solutionDelegate">A <see cref="Func{TResult}"/> delegate.</param>
    /// <param name="answer">The answer from the delegate.</param>
    /// <returns>The elapsed time.</returns>
    private static long GetElapsedTimeForSolution(Func<int> solutionDelegate, out int answer)
    {
        Stopwatch stopwatch = new();

        stopwatch.Start();

        answer = solutionDelegate();

        stopwatch.Stop();

        return stopwatch.ElapsedMilliseconds;
    }
}
