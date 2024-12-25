namespace AdventOfCode.Puzzles;

public class Puzzle24 : Puzzle<string, long>
{
    private const int PuzzleId = 24;

    public Puzzle24() : base(PuzzleId) { }

    public Puzzle24(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        ProcessInput();
        ProcessGates();
        var result = GetResult();
        return result;
    }

    private long GetResult()
    {
        var zValues = _input
            .Where(i => i.Key.StartsWith('z'))
            .OrderBy(i => i.Key)
            .ToList();
        var zValuesWithIndex = zValues
            .Select(i => i.Value ? 1 : 0)
            .Index()
            .ToList();
        var results = zValuesWithIndex
            // .Select(i => i.Item * (long)Math.Pow(2, i.Index))
            .Select(i => i.Item * (1L << i.Index))
            .ToList();
        var sum = results.Sum();
        return sum;
    }

    private void ProcessGates()
    {
        while (_gatesToEvaluate.TryDequeue(out var gate))
        {
            var value = gate.Evaluate(_input);
            _input[gate.Output] = value;
            if (_gatesByInput1.TryGetValue(gate.Output, out var i1Gates))
            {
                // We've just added a value for the [Output] item
                foreach (var i1Gate in i1Gates)
                {
                    // For every gate that as [Output] as its Input1...
                    if (_input.ContainsKey(i1Gate.Input2))
                    {
                        // ... if there also is a value for Input2, the gate can be evaluated
                        _gatesToEvaluate.Enqueue(i1Gate);
                    }
                }
            }
            if (_gatesByInput2.TryGetValue(gate.Output, out var i2Gates))
            {
                // We've just added a value for the [Output] item
                foreach (var i2Gate in i2Gates)
                {
                    // For every gate that as [Output] as its Input2...
                    if (_input.ContainsKey(i2Gate.Input1))
                    {
                        // ... if there also is a value for Input1, the gate can be evaluated
                        _gatesToEvaluate.Enqueue(i2Gate);
                    }
                }
            }
        }
    }

    private readonly Dictionary<string, bool> _input = [];
    private readonly Dictionary<string, List<Gate>> _gatesByInput1 = [];
    private readonly Dictionary<string, List<Gate>> _gatesByInput2 = [];
    private readonly Queue<Gate> _gatesToEvaluate = [];
    
    private void ProcessInput()
    {
        var doneProcessingInput = false;
        foreach (var inputEntry in InputEntries)
        {
            if (inputEntry == string.Empty)
            {
                doneProcessingInput = true;
                continue;
            }
            
            if (!doneProcessingInput)
            {
                var inputParts = inputEntry.Split(':', StringSplitOptions.TrimEntries);
                _input[inputParts[0]] = inputParts[1] == "1";
            }
            else
            {
                var gateParts = inputEntry.Split(' ');
                // x00 AND y00 -> z00
                
                var i1 = gateParts[0];
                var i2 = gateParts[2];
                var o = gateParts[4];
                var op = gateParts[1] switch
                {
                    "AND" => '&',
                    "OR" => '|',
                    "XOR" => '^',
                    _ => throw new NotImplementedException()
                };
                
                var gate = new Gate(i1, i2, op, o);
                if (!_gatesByInput1.TryGetValue(i1, out var i1Gates))
                {
                    i1Gates = [];
                    _gatesByInput1.Add(i1, i1Gates);
                }
                i1Gates.Add(gate);
                
                if (!_gatesByInput2.TryGetValue(i2, out var i2Gates))
                {
                    i2Gates = [];
                    _gatesByInput2.Add(i2, i2Gates);
                }
                i2Gates.Add(gate);

                if (_input.ContainsKey(i1) && _input.ContainsKey(i2))
                {
                    _gatesToEvaluate.Enqueue(gate);
                }
            }
        }
    }

    private record Gate(string Input1, string Input2, char Operation, string Output)
    {
        public bool Evaluate(Dictionary<string, bool> input)
        {
            var i1 = input[Input1];
            var i2 = input[Input2];
            return Operation switch
            {
                '&' => i1 && i2,
                '|' => i1 || i2,
                '^' => i1 ^ i2,
                _ => throw new NotImplementedException()
            };
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
