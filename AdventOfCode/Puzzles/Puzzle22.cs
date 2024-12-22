namespace AdventOfCode.Puzzles;

public class Puzzle22 : Puzzle<long, long>
{
    private const int PuzzleId = 22;

    public Puzzle22() : base(PuzzleId) { }

    public Puzzle22(params IEnumerable<long> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        const int iterations = 2000;
        
        var nextSecrets = InputEntries.Select(secret => NextSecrets(secret, iterations).Last());
        var sum = nextSecrets.Sum();
        
        return sum;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    internal static long GetOneDigit(long number)
    {
        return number % 10;
    }

    internal static IEnumerable<long> NextSecrets(long secret, int count)
    {
        for (var i = 0; i < count; i++)
        {
            secret = NextSecret(secret);
            yield return secret;
        }
    }

    internal static long NextSecret(long secret)
    {
        // const long pruneFactor = 16_777_216;
        const long pruneFactor = 0xFFFFFF;
        var nextSecret = secret;

        // Step 1
        // var multiplied = secret * 64;
        var multiplied = secret << 6;
        var mix = multiplied ^ nextSecret;
        // var pruned = mix % pruneFactor;
        var pruned = mix & pruneFactor;
        nextSecret = pruned;
        
        // Step 2
        // var divided = nextSecret / 32;
        var divided = nextSecret >> 5;
        mix = divided ^ nextSecret;
        // pruned = mix % pruneFactor;
        pruned = mix & pruneFactor;
        nextSecret = pruned;
        
        // Step 3
        // multiplied = nextSecret * 2048;
        multiplied = nextSecret << 11;
        mix = multiplied ^ nextSecret;
        // pruned = mix % pruneFactor;
        pruned = mix & pruneFactor;
        nextSecret = pruned;
        
        return nextSecret;
    }

    protected internal override long ParseInput(string inputItem)
    {
        return long.Parse(inputItem);
    }
}
