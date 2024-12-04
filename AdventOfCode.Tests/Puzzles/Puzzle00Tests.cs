namespace AdventOfCode.Tests.Puzzles;

public class Puzzle00Tests
{
    private readonly Puzzle00 _puzzle;

    public Puzzle00Tests()
    {
        _puzzle = new Puzzle00();
    }

    // [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }
}
