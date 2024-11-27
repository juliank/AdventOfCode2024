namespace AdventOfCode.Puzzles;

public class Puzzle01 : Puzzle<int, int>
{
    private const int PuzzleId = 01;

    public Puzzle01() : base(PuzzleId) { }

    public Puzzle01(params List<int> inputEntries) : base(PuzzleId, inputEntries) { }

    public override int SolvePart1()
    {
        return InputEntries.Sum();
    }

    public override int SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override IEnumerable<int> ParseInput(string inputItem)
    {
        yield return int.Parse(inputItem);
    }
}
