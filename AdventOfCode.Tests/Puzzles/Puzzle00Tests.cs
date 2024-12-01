namespace AdventOfCode.Tests.Puzzles;

public class Puzzle00Tests
{
    private readonly Puzzle00 puzzle;

    public Puzzle00Tests()
    {
        puzzle = new Puzzle00();
    }

    // [Fact]
    public void SolvePart1()
    {
        var result = puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact]
    public void SolvePart2()
    {
        var result = puzzle.SolvePart2();
        result.Should().Be(0);
    }
}
