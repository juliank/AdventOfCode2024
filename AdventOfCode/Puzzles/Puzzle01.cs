namespace AdventOfCode.Puzzles;

public class Puzzle01 : Puzzle<(int, int), int>
{
    private const int PuzzleId = 01;

    public Puzzle01() : base(PuzzleId) { }

    public Puzzle01(params List<(int, int)> inputEntries) : base(PuzzleId, inputEntries) { }

    public override int SolvePart1()
    {
        var firstList = InputEntries.Select(x => x.Item1).Order();
        var secondList = InputEntries.Select(x => x.Item2).Order().ToArray();

        var diffSum = 0;
        foreach (var (index, item) in firstList.Index())
        {
            var diff = Math.Abs(secondList[index] - item);
            diffSum += diff;
        }
        return diffSum;
    }

    public override int SolvePart2()
    {
        var firstList = InputEntries.Select(x => x.Item1).Order();
        // ToArray is not necessary here, but the code is much faster this way
        var secondList = InputEntries.Select(x => x.Item2).Order().ToArray();

        var totalSimilarityScore = 0;
        foreach (var item in firstList)
        {
            var similarityScore = secondList.Count(i => i == item);
            totalSimilarityScore += similarityScore * item;
        }
        return totalSimilarityScore;
    }

    protected internal override IEnumerable<(int, int)> ParseInput(string inputItem)
    {
        var pair = inputItem.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        yield return (int.Parse(pair[0]), int.Parse(pair[1]));
    }
}
