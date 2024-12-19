
namespace AdventOfCode.Tests.Puzzles;

public class Puzzle19Tests
{
    private Puzzle19 _puzzle;

    public Puzzle19Tests()
    {
        _puzzle = new Puzzle19();
    }

    // [Fact(Skip = "Not yet implemented")]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Fact]
    public void TestPart1Example()
    {
        _puzzle = new Puzzle19(
            "r, wr, b, g, bwu, rb, gb, br",
            "",
            "brwrr",
            "bggr",
            "gbbr",
            "rrbgbr",
            "ubwu",
            "bwurrg",
            "brgr",
            "bbrgwb" // bbrgwb is falsely marked as possible...
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be(6);
    }
}
