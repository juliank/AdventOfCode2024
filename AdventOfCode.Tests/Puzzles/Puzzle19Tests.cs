
namespace AdventOfCode.Tests.Puzzles;

public class Puzzle19Tests
{
    private Puzzle19 _puzzle;

    public Puzzle19Tests()
    {
        _puzzle = new Puzzle19();
    }

    [Fact(Skip = "Wrong answer")]
    public void SolvePart1()
    {
        // Uncomment when debugging in Rider
        _puzzle = new Puzzle19(File.ReadLines("C:\\src\\personal\\AdventOfCode2024Input\\19.txt"));
        var result = _puzzle.SolvePart1();
        result.Should().BeGreaterThan(0);
    }

    [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Fact(Skip = "Wrong answer")]
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
