namespace AdventOfCode.Puzzles;

public class Puzzle02 : Puzzle<int[], int>
{
    private const int PuzzleId = 02;

    public Puzzle02() : base(PuzzleId) { }

    public Puzzle02(params IEnumerable<int[]> inputEntries) : base(PuzzleId, inputEntries) { }

    public override int SolvePart1()
    {
        return InputEntries.Where(ReportIsSafe).Count();
    }

    public override int SolvePart2()
    {
        return InputEntries.Where(numbers =>
        {
            if (ReportIsSafe(numbers))
            {
                return true;
            }

            // The Problem Dampener is a reactor-mounted module that lets the
            // reactor safety systems tolerate a single bad level in what would
            // otherwise be a safe report.
            for (int i = 0; i < numbers.Length; i++)
            {
                var numbersExceptI = numbers[..i].Concat(numbers[(i + 1)..]).ToArray();
                if (ReportIsSafe(numbersExceptI))
                {
                    return true;
                }
            }
            return false;
        }).Count();
    }

    protected internal override int[] ParseInput(string inputItem)
    {
        return inputItem.Split(' ').Select(int.Parse).ToArray();
    }

    /// <summary>
    /// A report only counts as safe if both of the following are true:
    ///   - The levels are either all increasing or all decreasing.
    ///   - Any two adjacent levels differ by at least one and at most three.
    /// </summary>
    private static bool ReportIsSafe(int[] numbers)
    {
        // A report only counts as safe if both of the following are true:
        // - The levels are either all increasing or all decreasing.
        // - Any two adjacent levels differ by at least one and at most three.
        var increasing = numbers[1] > numbers[0];
        for (var i = 1; i < numbers.Length; i++)
        {
            var incInner = numbers[i] > numbers[i - 1];
            if (incInner != increasing)
            {
                return false;
            }

            var value = numbers[i] - numbers[i - 1];
            var absValue = Math.Abs(value);
            if (absValue is < 1 or > 3)
            {
                return false;
            }
        }
        return true;
    }
}
