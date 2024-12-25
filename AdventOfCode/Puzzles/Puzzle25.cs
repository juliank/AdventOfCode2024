namespace AdventOfCode.Puzzles;

public class Puzzle25 : Puzzle<string, long>
{
    private const int PuzzleId = 25;

    public Puzzle25() : base(PuzzleId) { }

    public Puzzle25(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    private readonly List<int[]> _locks = [];
    private readonly List<int[]> _keys = [];

    public override long SolvePart1()
    {
        ProcessInput();
        var candidates = GetCandidates();
        return candidates.Count;
    }

    private List<(int[] Lock, int[] Key)> GetCandidates()
    {
        List<(int[] Lock, int[] Key)> candidates = [];
        
        foreach (var @lock in _locks)
        {
            foreach (var key in _keys)
            {
                var isCandidate = true;
                for (int i = 0; i < @lock.Length; i++)
                {
                    if (@lock[i] + key[i] > 5)
                    {
                        isCandidate = false;
                        break;
                    }
                }
                if (isCandidate)
                {
                    candidates.Add((@lock, key));
                }
            }
        }

        return candidates;
    }

    private void ProcessInput()
    {
        var inputs = InputEntries.AsEnumerable();
        for (int i = 0; i < InputEntries.Count; i += 8)
        {
            var nextInput = InputEntries.Skip(i).Take(7).ToList();
            if (nextInput[0].StartsWith('#'))
            {
                ProcessInput(_locks, nextInput.Skip(1).ToList());
            }
            else
            {
                ProcessInput(_keys, nextInput.Take(6).ToList());
            }
        }
    }

    private static void ProcessInput(List<int[]> schematics, List<string> input)
    {
        var schematic = new int[5];
        for (int i = 0; i < schematic.Length; i++)
        {
            schematic[i] = input.Count(inp => inp[i] == '#');
        }
        schematics.Add(schematic);
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
