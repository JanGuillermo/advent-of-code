namespace AdventOfCode.Solutions.Utils;

internal class MathUtils
{
    public static double Variance(IEnumerable<int> data)
    {
        double mean = data.Average();

        return data.Select(x => Math.Pow(x - mean, 2)).Average() * (double)data.Count() / (data.Count() - 1);
    }

    public static int ModInverse(int value, int modulo)
    {
        value = value % modulo;

        for (int x = 1; x < modulo; x++)
        {
            if ((value * x) % modulo == 1)
            {
                return x;
            }
        }

        throw new InvalidOperationException("Modular inverse does not exist.");
    }

    public static int PositiveModulo(int value, int modulo)
    {
        return (value % modulo + modulo) % modulo;
    }
}
