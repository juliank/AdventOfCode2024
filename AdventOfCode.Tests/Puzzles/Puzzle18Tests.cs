namespace AdventOfCode.Tests.Puzzles;

public class Puzzle18Tests
{
    private Puzzle18 _puzzle;

    public Puzzle18Tests()
    {
        _puzzle = new Puzzle18();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be("296");
    }

    [Fact(Skip = "Too slow with real input")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be("28,44");
    }

    [Fact]
    public void TestPart1Example()
    {
        _puzzle = new Puzzle18(new Boundary(0, 0, 6, 6), 12,
            new Point(5, 4),
            new Point(4, 2),
            new Point(4, 5),
            new Point(3, 0),
            new Point(2, 1),
            new Point(6, 3),
            new Point(2, 4),
            new Point(1, 5),
            new Point(0, 6),
            new Point(3, 3),
            new Point(2, 6),
            new Point(5, 1),
            new Point(1, 2),
            new Point(5, 5),
            new Point(2, 5),
            new Point(6, 5),
            new Point(1, 4),
            new Point(0, 4),
            new Point(6, 4),
            new Point(1, 1),
            new Point(6, 1),
            new Point(1, 0),
            new Point(0, 5),
            new Point(1, 6),
            new Point(2, 0)
        );
        // _puzzle.DebugOutput = true;
        var result = _puzzle.SolvePart1();
        result.Should().Be("22");
    }

    [Fact]
    public void TestPart2Example()
    {
        _puzzle = new Puzzle18(new Boundary(0, 0, 6, 6), 12,
            new Point(5, 4),
            new Point(4, 2),
            new Point(4, 5),
            new Point(3, 0),
            new Point(2, 1),
            new Point(6, 3),
            new Point(2, 4),
            new Point(1, 5),
            new Point(0, 6),
            new Point(3, 3),
            new Point(2, 6),
            new Point(5, 1),
            new Point(1, 2),
            new Point(5, 5),
            new Point(2, 5),
            new Point(6, 5),
            new Point(1, 4),
            new Point(0, 4),
            new Point(6, 4),
            new Point(1, 1),
            new Point(6, 1),
            new Point(1, 0),
            new Point(0, 5),
            new Point(1, 6),
            new Point(2, 0)
        );
        _puzzle.IsRunningFromTest = true;
        // _puzzle.DebugOutput = true;
        var result = _puzzle.SolvePart2();
        result.Should().Be("6,1");
    }
}
