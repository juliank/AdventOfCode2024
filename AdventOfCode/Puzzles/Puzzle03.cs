using System.Text.RegularExpressions; // Using statement seemingly not needed, but required for the GeneratedRegex attribute

namespace AdventOfCode.Puzzles;

public partial class Puzzle03 : Puzzle<string, long>
{
    private const int PuzzleId = 03;

    public Puzzle03() : base(PuzzleId) { }

    public Puzzle03(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MultiplyInstruction1 { get; }

    public override long SolvePart1()
    {
        var sum = 0L;

        foreach (var item in InputEntries)
        {
            var instructions = MultiplyInstruction1.Matches(item);
            foreach (Match match in instructions)
            {
                var d1 = long.Parse(match.Groups[1].Value);
                var d2 = long.Parse(match.Groups[2].Value);
                sum += d1 * d2;
            }
        }

        return sum;
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)")]
    private static partial Regex MultiplyInstruction2 { get; }

    public override long SolvePart2()
    {
        var sum = 0L;

        var @do = true;
        foreach (var item in InputEntries)
        {
            var instructions = MultiplyInstruction2.Matches(item);
            foreach (Match match in instructions)
            {
                if (match.Groups[0].Value == "do()")
                {
                    @do = true;
                    continue;
                }
                
                if (match.Groups[0].Value == "don't()")
                {
                    @do = false;
                    continue;
                }

                if (!@do)
                {
                    continue;
                }
                var d1 = long.Parse(match.Groups[1].Value);
                var d2 = long.Parse(match.Groups[2].Value);
                sum += d1 * d2;
            }
        }

        return sum;
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
