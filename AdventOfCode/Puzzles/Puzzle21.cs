namespace AdventOfCode.Puzzles;

public class Puzzle21 : Puzzle<string, long>
{
    private const int PuzzleId = 21;

    public Puzzle21() : base(PuzzleId) { }

    public Puzzle21(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        _doPrint = false;
        var moves = GetMoves();
        var complexities = moves.Select(m => CalculateComplexity(m.Code, m.Moves)).ToList();
        var complexitySum = complexities.Sum();
        
        // 103026 is too low
        return complexitySum;
    }

    internal List<(string Code, List<Direction?> Moves)> GetMoves()
    {
        InitializeKeyPads();
        var moves = InputEntries.Select(s => (Code: s, Moves: PushDigits(s))).ToList();
        return moves;
    }

    internal static int CalculateComplexity(string code, List<Direction?> moves)
    {
        var number = int.Parse(code[..3]);
        var sequenceLength = moves.Count;
        var product = number * sequenceLength;
        return product;
    }

    private KeyPad<int> _numPad = null!;
    private KeyPad<Direction> _dPad1 = null!;
    private KeyPad<Direction> _dPad2 = null!;

    private void InitializeKeyPads()
    {
        HashSet<(Point Position, int? Value)> buttons = [
            (new (0, 0), 7), (new (1, 0), 8), (new (2, 0), 9),
            (new (0, 1), 4), (new (1, 1), 5), (new (2, 1), 6),
            (new (0, 2), 1), (new (1, 2), 2), (new (2, 2), 3),
                             (new (1, 3), 0), (new (2, 3), null)
        ];
        HashSet<Point> obstacles = [ new(0, 3) ];
        Boundary boundary = new(0, 0, 3, 4);
        _numPad = new KeyPad<int>(buttons, obstacles, boundary);
        
        HashSet<(Point Position, Direction? Value)> dButtons = [
                                       (new (1, 0), Direction.N), (new (2, 0), null),
            (new (0, 1), Direction.W), (new (1, 1), Direction.S), (new (2, 1), Direction.E)
        ];
        obstacles = [ new(0, 0) ];
        boundary = new(0, 0, 3, 1);
        _dPad1 = new KeyPad<Direction>(dButtons, obstacles, boundary);
        _dPad2 = new KeyPad<Direction>(dButtons, obstacles, boundary);
    }

    private bool _doPrint;
    
    private void Print(string str)
    {
        if (_doPrint)
        {
            Console.WriteLine(str);
        }
    }

    private List<Direction?> PushDigits(string code)
    {
        Print("====");
        Print(code);
        Print("====");
        
        var numbers = code.Select(c => c == 'A' ? (int?)null : int.Parse(c.ToString())).ToList();
        var numPadMoveSequences = PushKeyPad(_numPad, numbers);
        foreach ((int index, char value) in code.Index())
        {
            Print($"{value}: {SequenceToString(numPadMoveSequences[index])}");
        }
        Print("*");
        
        List<List<Direction?>> dPad1MoveSequences = [];
        Print(SequenceToString(numPadMoveSequences.SelectMany(s => s).ToList()));
        Print("---");
        foreach (var moveSequence in numPadMoveSequences)
        {
            var dPad1Moves = PushKeyPad(_dPad1, moveSequence);
            dPad1MoveSequences.AddRange(dPad1Moves);
            foreach ((int index, List<Direction?>  value) in dPad1Moves.Index())
            {
                Print($"{SequenceToString(value)}: {SequenceToString(dPad1Moves[index])}");
            }
            Print("*");
        }
        
        List<List<Direction?>> dPad2MoveSequences = [];
        foreach (var moveSequence in dPad1MoveSequences)
        {
            var dPad2Moves = PushKeyPad(_dPad2, moveSequence);
            dPad2MoveSequences.AddRange(dPad2Moves);
            
        }
        return dPad2MoveSequences.SelectMany(s => s).ToList();
    }

    private List<List<Direction?>> PushKeyPad<T>(KeyPad<T> keyPad, List<T?> values) where T : struct
    {
        List<List<Direction?>> movesSequences = [];
        for (var i = 0; i < values.Count; i++)
        {
            // Move to button with given value
            T? value = values[i];
            var movesSequence = keyPad.MoveTo(value);
            var orderedMovesSequence = movesSequence.OrderBy(d => d switch
            {
                // Ordering of sequences was initially added to ease debugging,
                // but it actually helped to make 3 out of 5 example codes correct...
                // With this ordering, 029A, 980A and 379A get the expected result.
                Direction.W => 0,
                Direction.E => 1,
                Direction.S => 2,
                Direction.N => 3,
                _ => throw new ArgumentOutOfRangeException()
            });
            
            // Every move sequence should end with a push of the Action button (button with the "null" direction)
            List<Direction?> sequences = [..orderedMovesSequence, null];
            var str = SequenceToString(sequences);
            Print(str);
            // Console.WriteLine($"{value?.ToString() ?? "A"}: {str}");
            movesSequences.Add(sequences);
        }

        return movesSequences;
    }

    private static string SequenceToString(List<Direction?> sequence)
    {
        var str = sequence.Select(d =>
        {
            return d switch
            {
                Direction.N => '^',
                Direction.S => 'v',
                Direction.E => '>',
                Direction.W => '<',
                _ => 'A'
            };
        }).CreateString();
        return str;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
    
    [DebuggerDisplay("({_currentPosition.X}, {_currentPosition.Y}): {CurrentValue}")]
    private class KeyPad<T> where T : struct
    {
        // A null button indicates the 'A' (activte) button
        private readonly HashSet<(Point Position, T? Value)> _buttons;
        private readonly HashSet<Point> _obstacles;
        private readonly Boundary _boundary;
        private readonly Point _initialPosition;
        private Point _currentPosition;
        private T? CurrentValue => _buttons.SingleOrDefault(b => b.Position == _currentPosition).Value;

        public KeyPad(HashSet<(Point Position, T? Value)> buttons, HashSet<Point> obstacles, Boundary boundary)
        {
            _buttons = buttons;
            _obstacles = obstacles;
            _boundary = boundary;
            
            // The initial current position is the A button (indicated by null) 
            _initialPosition = buttons.Single(b => b.Value == null).Position;
            _currentPosition = _initialPosition;
        }

        /// <summary>
        /// Get a list of moves needed to move from <see cref="_currentPosition"/>
        /// to the given next position.
        /// </summary>
        /// <param name="nextValue">The position to move to. Will also be the next current position.</param>
        public List<Direction> MoveTo(T? nextValue)
        {
            var nextPosition = _buttons.Single(b => EqualityComparer<T?>.Default.Equals(b.Value, nextValue)).Position;
            var path = AStarPathfinding.FindShortestPath(_currentPosition, nextPosition, _boundary, _obstacles);
            _currentPosition = nextPosition;
            if (path.Count <= 1)
            {
                return [];
            }
            var moves = new List<Direction>();
            for (var i = 0; i < path.Count - 1; i++)
            {
                moves.Add(path[i].GetDirectionTo(path[i + 1]));
            }

            return moves;
        }
    }
}
