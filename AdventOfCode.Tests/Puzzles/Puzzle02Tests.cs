namespace AdventOfCode.Tests.Puzzles;

public class Puzzle02Tests
{
    private readonly Puzzle02 puzzle;

    public Puzzle02Tests()
    {
        puzzle = new Puzzle02();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(660);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(689);
    }
}
