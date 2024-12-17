namespace AdventOfCode.Puzzles;

public class Puzzle17 : Puzzle<string, string>
{
    private const int PuzzleId = 17;

    public Puzzle17() : base(PuzzleId) { }

    public Puzzle17(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    public override string SolvePart1()
    {
        // [pointer]            [next_pointer]
        // [operator] [operand] [operator] [operand]
        // OPERANDS:
        // - 0 => 0
        // - 1 => 1
        // - 2 => 2
        // - 3 => 3
        // - 4 => A
        // - 5 => B
        // - 6 => C
        // - 7 => invalid
        
        // INSTRUCTIONS
        // - adv (0): division => A / (2^[operand]) => [0] [5] => A / (2^B)
        //                     => truncate => A
        // - bxl (1): bitwise XOR => B XOR [operand]
        //                        => B
        // - bst (2): modulo => [operand] mod 8 (keeping lowest 3 bits)
        //                   => B
        // - jnz (3): conditional jump =>
        //            - if A == 0 => [nothing]
        //            - else => [pointer] => [operand] (do NOT move pointer the regular 2 steps after jnz)
        // - bxc (4) bitwise XOR => B XOR C (ignores [operand])
        //                       => B
        // - out (5) modulo => [operand] mod 8
        //                  => output result (comma-separated)
        // - bdv (6): division => A / (2^[operand]) => [0] [5] => A / (2^B) (same as adv, only result goes to B)
        //                     => truncate => B
        // - cdv (7): division => A / (2^[operand]) => [0] [5] => A / (2^B) (same as adv, only result goes to C)
        //                     => truncate => C
        throw new NotImplementedException();
    }

    public override string SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
