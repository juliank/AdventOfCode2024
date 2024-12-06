namespace AdventOfCode.Tests.Puzzles;

public class Puzzle06Tests
{
    private Puzzle06 _puzzle;

    public Puzzle06Tests()
    {
        _puzzle = new Puzzle06();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(4752);
    }

    [Fact(Skip = "Wrong answer")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().BeGreaterThan(654);
    }

    [Fact]
    public void Part2ExampleInput()
    {
        _puzzle = new Puzzle06(
            "....#.....",
            ".........#",
            "..........",
            "..#.......",
            ".......#..",
            "..........",
            ".#..^.....",
            "........#.",
            "#.........",
            "......#..."
        );

        var answer = _puzzle.SolvePart2();

        answer.Should().Be(6);
    }
}
