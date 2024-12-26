namespace AdventOfCode.Puzzles;

public class Puzzle17 : Puzzle<string, string>
{
    private const int PuzzleId = 17;

    public Puzzle17() : base(PuzzleId) { }

    public Puzzle17(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }
    
    // Internal properties to allow test assertions on the register values
    internal long A { get; private set; }
    internal long B { get; private set; }
    internal long C { get; private set; }
    private long[] _program = null!;

    public override string SolvePart1()
    {
        (A, B, C, _program) = ProcessInput();
        var output = RunProgram();
        return output;
    }

    private readonly HashSet<(long, long, long)> _invalidInput = [];
    private long? _lastKnownInvalidA = null;

    internal bool RunningFromTests { get; set; }

    public override string SolvePart2()
    {
        ThrowHardCodedResult("?", "Hit an eternal loop when attempting part 2 with real input", notFromTest: true);
        // if (!RunningFromTests)
        // {
        //     // Runs "eternally" with the real input.
        //     // Have attempted to add even more caching (at jnz),
        //     // but with this even the test case ends up in a loop...
        //     throw new NotImplementedException("Solution isn't working - attempt to add caching created a loop in jnz");
        // }
        (A, B, C, _program) = ProcessInput();
        var expectedOutput = string.Join(',', _program);
        var a = 0L;
        while (true)
        {
            var output = "INVALID";
            var currentInput = (a, B, C);
            if (!_invalidInput.Contains(currentInput)) // Only invalid values found inside RunProgram is added to this cache
            {
                output = RunProgram(a);
                if (output == expectedOutput)
                {
                    return a.ToString();
                }
                _lastKnownInvalidA = a;
                // _invalidInput.Add(currentInput);
            }
            a++;
        }
        
        // 13185239446 is too low
    }

    private string RunProgram(long? alternativeA = null)
    {
        if (alternativeA.HasValue)
        {
            A = alternativeA.Value;
        }
        var initialA = A;
        var initialB = B;
        var initialC = C;

        var output = new List<long>();

        long GetValue(long op)
        {
            return op switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => A,
                5 => B,
                6 => C,
                7 => 7, // Reserved, will not appear in a valid program
                _ => throw new ArgumentOutOfRangeException(nameof(op), op, $"{op} is an invalid operand.")
            };
        }

        for (var p = 0L; p < _program.Length; p += 2)
        {
            var @operator = _program[p];
            var literalOperand = _program[p + 1];
            var comboOperand = GetValue(literalOperand);

            switch (@operator)
            {
                case 0: // adv
                    // var adv = A / (long)Math.Pow(2, comboOperand);
                    A /= (1L << (int)comboOperand);
                    break;
                case 1: // bxl
                    B ^= literalOperand;
                    break;
                case 2: // bst
                    // var bst = comboOperand % 8;
                    B = comboOperand & 7;
                    break;
                case 3: // jnz
                    if (A != 0 && p != literalOperand) // Only jump if A is not 0 *AND* the instruction will actually move the pointer
                    {
                        p = literalOperand;
                        // Attempt to add more caching, but we seem to hit a loop...
                        if (p == 0)
                        {
                            if (A <= _lastKnownInvalidA && B == 0 && C == 0)
                            {
                                // No need to add this to the cache
                                return "INVALID";
                            }
                            if (_invalidInput.Contains((A, B, C)))
                            {
                                _invalidInput.Add((initialA, initialB, initialC));
                                return "INVALID";
                            }
                        }
                        p -= 2; // Since the for loop will increment the pointer by 2
                    }
                    break;
                case 4: // bxc
                    B ^= C;
                    break;
                case 5: // out
                    // var outValue = comboOperand % 8;
                    output.Add(comboOperand & 7);
                    break;
                case 6: // bdv
                    // var bdv = A / (long)Math.Pow(2, comboOperand);
                    B = A / (1L << (int)comboOperand);
                    break;
                case 7: // cdv
                    // var cdv = A / (long)Math.Pow(2, comboOperand);
                    C = A / (1L << (int)comboOperand);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@operator), @operator, $"{@operator} is an invalid operator.");
            }
        }

        return string.Join(',', output);
    }

    private (long A, long B, long C, long[] Program) ProcessInput()
    {
        var a = long.Parse(InputEntries[0].Split(':').Last().Trim());
        var b = long.Parse(InputEntries[1].Split(':').Last().Trim());
        var c = long.Parse(InputEntries[2].Split(':').Last().Trim());
        var p = InputEntries[4].Split(':').Last().Trim().Split(',').Select(long.Parse).ToArray();
        return (a, b, c, p);
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
