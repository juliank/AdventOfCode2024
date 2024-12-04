namespace AdventOfCode.Tests.Puzzles;

public class Puzzle01Tests
{
    private readonly Puzzle01 _puzzle;

    public Puzzle01Tests()
    {
        _puzzle = new Puzzle01();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(1889772);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(23228917);
    }
}
