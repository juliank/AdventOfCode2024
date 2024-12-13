namespace AdventOfCode.Tests.Puzzles;

public class Puzzle13Tests
{
    private Puzzle13 _puzzle;

    public Puzzle13Tests()
    {
        _puzzle = new Puzzle13();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(37680);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(87550094242995);
    }

    [Fact]
    public void TestPart1Example1()
    {
        _puzzle = new Puzzle13(
            "Button A: X+94, Y+34",
            "Button B: X+22, Y+67",
            "Prize: X=8400, Y=5400",
            "",
            "Button A: X+26, Y+66",
            "Button B: X+67, Y+21",
            "Prize: X=12748, Y=12176",
            "",
            "Button A: X+17, Y+86",
            "Button B: X+84, Y+37",
            "Prize: X=7870, Y=6450",
            "",
            "Button A: X+69, Y+23",
            "Button B: X+27, Y+71",
            "Prize: X=18641, Y=10279"
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be(480);
    }

    [Fact]
    public void TestPart2Example1()
    {
        _puzzle = new Puzzle13(
            "Button A: X+94, Y+34",
            "Button B: X+22, Y+67",
            "Prize: X=8400, Y=5400",
            "",
            "Button A: X+26, Y+66",
            "Button B: X+67, Y+21",
            "Prize: X=12748, Y=12176",
            "",
            "Button A: X+17, Y+86",
            "Button B: X+84, Y+37",
            "Prize: X=7870, Y=6450",
            "",
            "Button A: X+69, Y+23",
            "Button B: X+27, Y+71",
            "Prize: X=18641, Y=10279"
        );
        var result = _puzzle.SolvePart2();
        result.Should().BeGreaterThan(480);
    }
}
