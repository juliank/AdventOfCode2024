namespace AdventOfCode.Puzzles;

public class Puzzle13 : Puzzle<string, long>
{
    private const int PuzzleId = 13;

    public Puzzle13() : base(PuzzleId) { }

    public Puzzle13(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    private readonly List<(Point A, Point B, Point P)> _clawMachines = [];
    
    public override long SolvePart1()
    {
        ProcessInput();

        var totalTokens = 0L;
        foreach (var clawMachine in _clawMachines)
        {
            (long x, long y) = SolveEquationsNative(clawMachine);
            if (x is <0 or >100 || y is <0 or >100)
            {
                // You estimate that each button would need to be pressed no more than 100 times to win a prize.
                continue;
            }

            var tokens = x * 3 + y;
            totalTokens += tokens;
        }

        return totalTokens;
    }

    public override long SolvePart2()
    {
        ProcessInput();

        var totalTokens = 0L;
        foreach (var clawMachine in _clawMachines)
        {
            (long x, long y) = SolveEquationsNative(clawMachine, 10000000000000);
            if (x < 0  || y < 0 )
            {
                continue;
            }

            var tokens = x * 3 + y;
            totalTokens += tokens;
        }

        return totalTokens;
    }

    /// <summary>
    /// This is basically solving two equations with two unknowns. Jetbrains' AI Assistant assisted
    /// with the main steps of the algorithm. Manual adjustments for floating point division, rounding
    /// and additional validation at the end were necessary.
    /// </summary>
    private static (long x, long y) SolveEquationsNative((Point A, Point B, Point P) clawMachine, long offset = 0)
    {
        // Coefficients from Points A, B, and P
        long ax = clawMachine.A.X;
        long bx = clawMachine.B.X;
        long px = offset + clawMachine.P.X;

        long ay = clawMachine.A.Y;
        long by = clawMachine.B.Y;
        long py = offset + clawMachine.P.Y;

        // Calculate the determinant of the system
        var determinant = ax * by - ay * bx;

        // If the determinant is 0, the equations are either inconsistent or dependent
        if (determinant == 0)
        {
            return (-1, -1); // Return an invalid point
        }

        // Use Cramer's Rule to calculate X and Y using floating-point division
        var x = (double)(px * by - py * bx) / determinant;
        var y = (double)(ax * py - ay * px) / determinant;

        // Round to the nearest integer since we only care about "perfect" values
        var roundedX = (long)Math.Round(x);
        var roundedY = (long)Math.Round(y);

        // Verify the equations with the rounded values
        var isValid = (ax * roundedX + bx * roundedY == px) &&
                      (ay * roundedX + by * roundedY == py);

        if (!isValid)
        {
            return (-1, -1); // Return an invalid point if the equations aren't satisfied
        }

        return (roundedX, roundedY);
    }

    private void ProcessInput()
    {
        for (var i = 0; i < InputEntries.Count; i += 4)
        {
            var a = ParseInputLine(InputEntries[i]);
            var b = ParseInputLine(InputEntries[i + 1]);
            var p = ParseInputLine(InputEntries[i + 2]);
            _clawMachines.Add((a, b, p));
        }
    }

    private static Point ParseInputLine(string inputEntry)
    {
        var second = inputEntry.Split(':')[1].Trim();
        var xy = second.Split(',').Select(s => s.Trim()).ToArray();
        var x = xy[0].Split(['+', '=']);
        var y = xy[1].Split(['+', '=']);
        return new Point(int.Parse(x[1]), int.Parse(y[1]));
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
