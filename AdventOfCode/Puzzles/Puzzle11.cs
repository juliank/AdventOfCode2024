namespace AdventOfCode.Puzzles;

public class Puzzle11 : Puzzle<long[], long>
{
    private const int PuzzleId = 11;

    public Puzzle11() : base(PuzzleId) { }

    public Puzzle11(params IEnumerable<long[]> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        return Blink(25);
    }

    public override long SolvePart2()
    {
        return Blink(75);
    }

    private readonly Dictionary<(long Number, int Iteration), long> _cache = [];

    internal long Blink(int iterations)
    {
        var stones = InputEntries[0];
        var totalNewStones = 0L;
        foreach (var stone in stones)
        {
            var newStones = Blink(stone, iterations);
            totalNewStones += newStones;
        }
        return totalNewStones;
    }

    private long Blink(long stone, int i)
    {
        if (_cache.TryGetValue((stone, i), out var cachedCount))
        {
            return cachedCount;
        }

        var newStones = new List<long>();
        if (stone == 0)
        {
            newStones.Add(1);
        }
        else
        {
            var numberOfDigits = stone == 0 ? 1 : (int)Math.Floor(Math.Log10(stone) + 1);
            if (numberOfDigits % 2 == 0)
            {
                var halfDigits = numberOfDigits / 2;
                var divisor = (long)Math.Pow(10, halfDigits);
                var first = stone / divisor;
                var second = stone % divisor;

                newStones.AddRange([first, second]);
            }
            else
            {
                newStones.Add(stone * 2024);
            }
        }

        if (i == 1)
        {
            _cache[(stone, i)] = newStones.Count;
            return newStones.Count;
        }
        
        var totalNewStoneCount = 0L;
        foreach (var s in newStones)
        {
            var newStoneCount = Blink(s, i - 1);
            totalNewStoneCount += newStoneCount;
        }

        _cache[(stone, i)] = totalNewStoneCount;
        return totalNewStoneCount;
    }

    protected internal override long[] ParseInput(string inputItem)
    {
        return inputItem.Split(' ').Select(long.Parse).ToArray();
    }
}
