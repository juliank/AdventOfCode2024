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

    [Fact(Skip = "Too slow with real input")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(1719);
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
        _puzzle.IsRunningFromTest = true;
        var answer = _puzzle.SolvePart2WorkingWithExample();
        answer.Should().Be(6);
    }
}
