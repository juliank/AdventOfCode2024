namespace AdventOfCode.Tests.Puzzles;

public class Puzzle01Tests
{
    private readonly Puzzle01 puzzle;

    public Puzzle01Tests()
    {
        puzzle = new Puzzle01();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(1889772);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(23228917);
    }
}
