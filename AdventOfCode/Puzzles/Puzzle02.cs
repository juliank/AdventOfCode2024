namespace AdventOfCode.Puzzles;

public class Puzzle02 : Puzzle<int[], int>
{
    private const int PuzzleId = 02;

    public Puzzle02() : base(PuzzleId) { }

    public Puzzle02(params List<int[]> inputEntries) : base(PuzzleId, inputEntries) { }

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
                var numbersExceptI = RemoveAt(numbers, i).ToArray();
                if (ReportIsSafe(numbersExceptI))
                {
                    return true;
                }
            }
            return false;
        }).Count();
    }

    protected internal override IEnumerable<int[]> ParseInput(string inputItem)
    {
        yield return inputItem.Split(' ').Select(int.Parse).ToArray();
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
            bool incInner = numbers[i] > numbers[i - 1];
            if (incInner != increasing)
            {
                return false;
            }

            int value = numbers[i] - numbers[i - 1];
            var absValue = Math.Abs(value);
            if (absValue is < 1 or > 3)
            {
                return false;
            }
        }
        return true;
    }

    private static IEnumerable<T> RemoveAt<T>(IEnumerable<T> source, int index)
    {
        if (index < 0 || index >= source.Count())
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }

        return source
            .Select((item, idx) => new { item, idx })
            .Where(x => x.idx != index)
            .Select(x => x.item);
    }
}
