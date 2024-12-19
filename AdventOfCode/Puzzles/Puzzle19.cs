
namespace AdventOfCode.Puzzles;

public class Puzzle19 : Puzzle<string, long>
{
    private const int PuzzleId = 19;

    public Puzzle19() : base(PuzzleId) { }

    public Puzzle19(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    private readonly HashSetTree<TowelColor> _towelPatterns = new(new TowelColor('\0'));
    
    private readonly List<string> _desiredDesigns = [];

    public override long SolvePart1()
    {
        ProcessInput();
        var possibleDesigns = FindPossibleDesigns();
        return possibleDesigns.Count;
    }

    private List<string> FindPossibleDesigns()
    {
        var possibleDesigns = new List<string>();
        foreach (var design in _desiredDesigns)
        {
            if (IsPossible(design, _towelPatterns))
            {
                possibleDesigns.Add(design);
            }
        }

        return possibleDesigns;
    }

    // private bool IsPossible(string design, List<HashSetTree<TowelColor>> towelPatterns)
    private bool IsPossible(string design, HashSetTree<TowelColor> towelPatterns)
    {
        var towelColor = new TowelColor(design[0]) { IsCompletePattern = design.Length == 1 };
        HashSetTree<TowelColor>? match = towelPatterns.FirstOrDefault(tp => tp.Value.Color == towelColor.Color);
        if (match == null)
        {
            return false;
        }

        if (design.Length == 1)
        {
            // We're at the final character (color) of the desired design.
            // If we have a match that *is* (the last color of) a complete pattern, it is possible to make
            return match.Value.IsCompletePattern;
        }

        var remainingDesign = design[1..];
        var possibleWithCurrentTowelPattern = IsPossible(remainingDesign, match);
        if (possibleWithCurrentTowelPattern)
        {
            return true;
        }
        var possibleWithOtherTowelPattern = IsPossible(remainingDesign, _towelPatterns);
        return possibleWithOtherTowelPattern;
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
    
    record TowelColor(char Color)
    {
        public bool IsCompletePattern { get; set; }
    }

    private void ProcessTowelPattern(string towelPattern)
    {
        var root = _towelPatterns.FirstOrDefault(tp => tp.Value.Color == towelPattern[0]) ?? // Existing root for this towel pattern
                   new HashSetTree<TowelColor>(new TowelColor(towelPattern[0])); // Towel pattern requires new root node
        if (towelPattern.Length == 1)
        {
            root.Value.IsCompletePattern = true;
        }

        var node = root;
        for (var i = 1; i < towelPattern.Length; i++)
        {
            var nextNode = new HashSetTree<TowelColor>(new TowelColor(towelPattern[i]));
            if (i == towelPattern.Length - 1)
            {
                nextNode.Value.IsCompletePattern = true;
            }
            node.Add(nextNode);
            node = nextNode;
        }
        
        _towelPatterns.Add(root);
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
