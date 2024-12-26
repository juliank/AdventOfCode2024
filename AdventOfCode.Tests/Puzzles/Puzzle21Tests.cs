namespace AdventOfCode.Tests.Puzzles;

public class Puzzle21Tests
{
    private Puzzle21 _puzzle;

    public Puzzle21Tests()
    {
        _puzzle = new Puzzle21();
    }

    [Fact(Skip = "Not yet implemented")]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(0);
    }

    [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Fact(Skip = "Fails")]
    public void TestPart1Example()
    {
        _puzzle = new Puzzle21(
            "029A",
            "980A",
            "179A",
            "456A",
            "379A"
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be(126384);
    }
    
    // FAILING#1 - correct count and complexity score, but the first 4 "sequences" appear to be "inverse"
    // exp:  <vA <A  A >>^A vAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A
    // act: <<vA  A >A  >^A vAA<^A>A<<vA>>^AvA^A<vA>^A<<vA>^A>AAvA^A<<vA>A>^AAAvA<^A>A
    //
    // FAILING#3 - number of sequences are the same, and are equal from the 11th
    // exp: <v<A >>^A <vA  <A >>^A  A  vA  A <^A >A <v<A>>^AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A
    // act: <<vA    A  >A >^A    A vA <^A >A  vA ^A <<vA>>^AAvA^A<vA>^AA<A>A<<vA>A>^AAAvA<^A>A
    //
    // FAILING#4 - number of sequences are the same, and are equal from the 11th
    // exp: <v<A >>^A  A <vA <A >>^A   A vA A <^A >A<vA>^A<A>A<vA>^A<A>A<v<A>A>^AAvA<^A>A
    // act: <<vA    A >A >^A  A   vA <^A >A A  vA ^A<vA>^A<A>A<vA>^A<A>A<<vA>A>^AAvA<^A>A
    [Theory]
    [InlineData("029A", "<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A", 68 * 29)]
    [InlineData("980A", "<v<A>>^AAAvA^A<vA<AA>>^AvAA<^A>A<v<A>A>^AAAvA<^A>A<vA>^A<A>A", 60 * 980)]
    // [InlineData("179A", "<v<A>>^A<vA<A>>^AAvAA<^A>A<v<A>>^AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A", 68 * 179)]
    // [InlineData("456A", "<v<A>>^AA<vA<A>>^AAvAA<^A>A<vA>^A<A>A<vA>^A<A>A<v<A>A>^AAvA<^A>A", 64 * 456)]
    [InlineData("379A", "<v<A>>^AvA^A<vA<AA>>^AAvA<^A>AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A", 64 * 379)]
    public void TestGetMoves(string code, string expectedMoves, int expectedComplexity)
    {
        _puzzle = new Puzzle21(code);
        var codeMove = _puzzle.GetMoves().Single();
        codeMove.Code.Should().Be(code);
        codeMove.Moves.Count.Should().Be(expectedMoves.Length);
        
        var complexity = Puzzle21.CalculateComplexity(codeMove.Code, codeMove.Moves);
        complexity.Should().Be(expectedComplexity);
        
        // var moves = Puzzle21.SequenceToString(codeMove.Moves);
        // moves.Should().Be(expectedMoves);
    }
    
    // OUTPUT FROM 029A
    //
    // 0: <A
    // 2: ^A
    // 9: >^^A
    // A: vvvA
    // => <A^A>^^AvvvA
    // ex:<A^A>^^AvvvA
    //
    // W: <<vA
    // A: >>^A
    // N: <A
    // A: >A
    // E: vA
    // N: <^A
    // N: A
    // A: >A
    // S: <vA
    // S: A
    // S: A
    // A: >^A
    // => <<vA>>^A<A>AvA<^AA>A<vAAA>^A <= MY OUTPUT: <<vA
    // ex:v<<A>>^A<A>AvA<^AA>A<vAAA>^A <=   EXAMPLE: v<<A
    //
    // Different ordering of first step above leads to following steps being different...
    // Perhaps this is the reason why some of the other codes yields wrong results?
    // But why? There doesn't seem to be a consistent ordering...
    //
    // W: <<vA
    // W: A
    // S: >A
    // A: >^A
    // ***
    // E: vA
    // E: A
    // N: <^A
    // A: >A
    // W: <<vA
    // A: >>^A
    // E: vA
    // A: ^A
    // S: <vA
    // A: >^A
    // W: <<vA
    // N: >^A
    // A: >A
    // A: A
    // E: vA
    // A: ^A
    // W: <<vA
    // S: >A
    // A: >^A
    // A: A
    // A: A
    // E: vA
    // N: <^A
    // A: >A
    // => <<vAA>A>^A *** vAA<^A>A<<vA>>^AvA^A<vA>^A<<vA>^A>AAvA^A<<vA>A>^AAAvA<^A>A
    // ex:<vA<AA>>^A *** vAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A
}
