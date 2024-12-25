namespace AdventOfCode.Tests.Puzzles;

public class Puzzle14Tests
{
    private Puzzle14 _puzzle;

    public Puzzle14Tests()
    {
        _puzzle = new Puzzle14();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().BeGreaterThan(217337280);
    }

    [Fact(Skip = "Requires manual debugging")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(7892);
    }

    [Fact]
    public void TestPart1Example()
    {
        _puzzle = new Puzzle14(10, 6);
        _puzzle.SetTestInput(
            "p=0,4 v=3,-3",
            "p=6,3 v=-1,-3",
            "p=10,3 v=-1,2",
            "p=2,0 v=2,-1",
            "p=0,0 v=1,3",
            "p=3,0 v=-2,-2",
            "p=7,6 v=-1,-3",
            "p=3,0 v=-1,-2",
            "p=9,3 v=2,3",
            "p=7,3 v=-1,2",
            "p=2,4 v=2,-3",
            "p=9,5 v=-3,-3"
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be(12);
    }
}
