namespace AdventOfCode.Tests.Puzzles;

public class Puzzle02Tests
{
    private readonly Puzzle02 _puzzle;

    public Puzzle02Tests()
    {
        _puzzle = new Puzzle02();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(660);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(689);
    }
}
