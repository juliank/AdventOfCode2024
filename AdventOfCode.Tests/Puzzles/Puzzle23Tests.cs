namespace AdventOfCode.Tests.Puzzles;

public class Puzzle23Tests
{
    private Puzzle23 _puzzle;

    public Puzzle23Tests()
    {
        _puzzle = new Puzzle23();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be("1227");
    }

    // [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be("0");
    }

    [Fact]
    public void TestPart1Example()
    {
        _puzzle = new Puzzle23(
            ("kh", "tc"),
            ("qp", "kh"),
            ("de", "cg"),
            ("ka", "co"),
            ("yn", "aq"),
            ("qp", "ub"),
            ("cg", "tb"),
            ("vc", "aq"),
            ("tb", "ka"),
            ("wh", "tc"),
            ("yn", "cg"),
            ("kh", "ub"),
            ("ta", "co"),
            ("de", "co"),
            ("tc", "td"),
            ("tb", "wq"),
            ("wh", "td"),
            ("ta", "ka"),
            ("td", "qp"),
            ("aq", "cg"),
            ("wq", "ub"),
            ("ub", "vc"),
            ("de", "ta"),
            ("wq", "aq"),
            ("wq", "vc"),
            ("wh", "yn"),
            ("ka", "de"),
            ("kh", "ta"),
            ("co", "tc"),
            ("wh", "qp"),
            ("tb", "vc"),
            ("td", "yn")
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be("7");
    }

    [Fact]
    public void TestPart2Example()
    {
        _puzzle = new Puzzle23(
            ("kh", "tc"),
            ("qp", "kh"),
            ("de", "cg"),
            ("ka", "co"),
            ("yn", "aq"),
            ("qp", "ub"),
            ("cg", "tb"),
            ("vc", "aq"),
            ("tb", "ka"),
            ("wh", "tc"),
            ("yn", "cg"),
            ("kh", "ub"),
            ("ta", "co"),
            ("de", "co"),
            ("tc", "td"),
            ("tb", "wq"),
            ("wh", "td"),
            ("ta", "ka"),
            ("td", "qp"),
            ("aq", "cg"),
            ("wq", "ub"),
            ("ub", "vc"),
            ("de", "ta"),
            ("wq", "aq"),
            ("wq", "vc"),
            ("wh", "yn"),
            ("ka", "de"),
            ("kh", "ta"),
            ("co", "tc"),
            ("wh", "qp"),
            ("tb", "vc"),
            ("td", "yn")
        );
        _puzzle.IsRunningFromTest = true;
        var result = _puzzle.SolvePart2();
        result.Should().Be("co,de,ka,ta");
    }
}
