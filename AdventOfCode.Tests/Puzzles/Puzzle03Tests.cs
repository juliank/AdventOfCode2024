namespace AdventOfCode.Tests.Puzzles;

public class Puzzle03Tests
{
    private Puzzle03 _puzzle;

    public Puzzle03Tests()
    {
        _puzzle = new Puzzle03();
    }

    [Fact]
    public void SolvePart1()
    {
        var answer = _puzzle.SolvePart1();
        answer.Should().Be(178886550);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(87163705);
    }

    [Fact]
    public void Part1ExampleInput()
    {
        _puzzle = new Puzzle03("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))");
        var answer = _puzzle.SolvePart1();
        answer.Should().Be(161);
    }

    [Fact]
    public void Part2ExampleInput()
    {
        _puzzle = new Puzzle03("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))");
        var answer = _puzzle.SolvePart2();
        answer.Should().Be(48);
    }
    
}
