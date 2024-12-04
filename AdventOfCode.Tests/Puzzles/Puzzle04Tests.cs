namespace AdventOfCode.Tests.Puzzles;

public class Puzzle04Tests
{
    private Puzzle04 _puzzle;

    public Puzzle04Tests()
    {
        _puzzle = new Puzzle04();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(2685);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(2048);
    }

    [Fact]
    public void Part1ExampleInput()
    {
        _puzzle = new Puzzle04(
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"
        );

        var result = _puzzle.SolvePart1();

        result.Should().Be(18);
    }
}
