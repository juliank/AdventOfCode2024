using System.Collections.Concurrent;

namespace AdventOfCode.Puzzles;

public class Puzzle19 : Puzzle<string, long>
{
    private const int PuzzleId = 19;

    public Puzzle19() : base(PuzzleId) { }

    public Puzzle19(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    // Is this perhaps a good time to implement DirectoryTree?
    // Then we could have a tree where the *key* only consists of the color (char => easier lookup),
    // and the *value* is the bool indicating if the pattern is complete or not... 
    private readonly DictionaryTree<char, bool> _towelPatterns = new(false);
    
    private readonly List<string> _desiredDesigns = [];
    
    // Must use ReadOnlyMemory (instead of ReadOnlyStruct) to allow usage as generic type argument
    private readonly SortedDictionary<int, List<ReadOnlyMemory<char>>> _towelPatternsNew = new();
    private readonly List<ReadOnlyMemory<char>> _desiredDesignsNew = [];

    public override long SolvePart1()
    {
        ProcessInputNew();
        var possibleDesigns = FindPossibleDesignsNew();
        return possibleDesigns.Count;
    }

    private List<string> FindPossibleDesignsNew()
    {
        var possibleDesigns = new List<string>();
        foreach (var desiredDesign in _desiredDesignsNew)
        {
            if (IsPossible(desiredDesign))
            {
                possibleDesigns.Add(desiredDesign.ToString());
            }
        }
        return possibleDesigns;
    }

    private bool IsPossible(ReadOnlyMemory<char> desiredDesign)
    {
        var isPossible = false;
        
        for (int length = desiredDesign.Length; length > 0; length--)
        {
            if (!_towelPatternsNew.TryGetValue(length, out var towelPatterns))
            {
                continue;
            }

            if (desiredDesign.Span.StartsWith(towelPatterns[0].Span))
            {
                
            }
        }

        return isPossible;
    }

    private void ProcessInputNew()
    {
        foreach (var towelPattern in InputEntries[0].Split(',', StringSplitOptions.TrimEntries))
        {
            if (!_towelPatternsNew.TryGetValue(towelPattern.Length, out var towelPatterns))
            {
                towelPatterns = new();
                _towelPatternsNew.Add(towelPattern.Length, towelPatterns);
            }
            towelPatterns.Add(towelPattern.AsMemory());
        }
        
        _desiredDesignsNew.AddRange(InputEntries.Skip(2).Select(design => design.AsMemory()));
    }

    public long SolvePart1Old()
    {
        ProcessInput();
        var possibleDesigns = FindPossibleDesigns();
        
        // Processing is *way* to slow. The puzzle ran for about 7 hours during a work day,
        // so something must definitely be done... There is no memory leak though - steady at 10-20 MB :D
        return possibleDesigns.Count;
    }

    private List<string> FindPossibleDesigns()
    {
        var possibleDesigns = new ConcurrentBag<string>();
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount / 2 // Can't use all my CPU - got to work as well :-)
        };
        Parallel.ForEach(_desiredDesigns, parallelOptions, design =>
        {
            if (IsPossible(design, _towelPatterns))
            {
                possibleDesigns.Add(design);
            }
        });

        return possibleDesigns.ToList();
    }

    private bool IsPossible(string design, DictionaryTree<char, bool> towelPatterns)
    {
        var towelColor = design[0];
        if (!towelPatterns.TryGetValue(towelColor, out var match))
        {
            return false;
        }

        if (design.Length == 1)
        {
            // We're at the final character (color) of the desired design.
            // If we have a match that *is* (the last color of) a complete pattern, it is possible to make
            return match.Value;
        }

        var remainingDesign = design[1..];
        var possibleWithCurrentTowelPattern = IsPossible(remainingDesign, match);
        if (possibleWithCurrentTowelPattern)
        {
            return true;
        }

        if (match.Value)
        {
            // If we're at the current match has a completely matching towel pattern,
            // we can continue checking the remaining design for all the available patterns.
            var possibleWithOtherTowelPattern = IsPossible(remainingDesign, _towelPatterns);
            return possibleWithOtherTowelPattern;
        }

        return false;
    }

    private void ProcessInput()
    {
        // Process towel patterns
        foreach (var towelPattern in InputEntries[0].Split(',', StringSplitOptions.TrimEntries))
        {
            ProcessTowelPattern(towelPattern);
        }
        
        // Process desired designs
        _desiredDesigns.AddRange(InputEntries.Skip(2));
    }

    private void ProcessTowelPattern(string towelPattern)
    {
        var towelColor = towelPattern[0];
        if (!_towelPatterns.TryGetValue(towelColor, out var root))
        {
            root = new DictionaryTree<char, bool>(towelPattern.Length == 1);
            _towelPatterns.Add(towelColor, root);
        }
        if (!root.Value && towelPattern.Length == 1)
        {
            root.Value = true;
        }

        var node = root;
        for (var i = 1; i < towelPattern.Length; i++)
        {
            towelColor = towelPattern[i];
            if (!node.TryGetValue(towelColor, out var nextNode))
            {
                nextNode = new DictionaryTree<char, bool>(i == towelPattern.Length - 1);
                node.Add(towelColor, nextNode);
            }

            if (!nextNode.Value && i == towelPattern.Length - 1)
            {
                nextNode.Value = true;
            }
            node = nextNode;
        }
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
