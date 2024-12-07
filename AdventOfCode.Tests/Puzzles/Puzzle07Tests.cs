namespace AdventOfCode.Tests.Puzzles;

public class Puzzle07Tests
{
    private Puzzle07 _puzzle;

    public Puzzle07Tests()
    {
        _puzzle = new Puzzle07();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(465126289353);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(70597497486371);
    }

    [Fact]
    public void Part2ExampleInput()
    {
        _puzzle = new Puzzle07(
            "190: 10 19",
            "3267: 81 40 27",
            "83: 17 5",
            "156: 15 6",
            "7290: 6 8 6 15",
            "161011: 16 10 13",
            "192: 17 8 14",
            "21037: 9 7 18 13",
            "292: 11 6 16 20"
        );

        var answer = _puzzle.SolvePart2();
        answer.Should().Be(11387);
    }
}
