namespace AdventOfCode.Tests.Puzzles;

public class Puzzle12Tests
{
    private Puzzle12 _puzzle;

    public Puzzle12Tests()
    {
        _puzzle = new Puzzle12();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(1533024);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(910066);
    }

    [Fact]
    public void TestPart1Example1()
    {
        _puzzle = new Puzzle12(
            "AAAA",
            "BBCD",
            "BBCC",
            "EEEC"
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be(140);
    }

    [Fact]
    public void TestPart2Example1()
    {
        _puzzle = new Puzzle12(
            "AAAA",
            "BBCD",
            "BBCC",
            "EEEC"
        );
        var result = _puzzle.SolvePart2();
        result.Should().Be(80);
    }

    [Fact]
    public void TestPart2Example2()
    {
        _puzzle = new Puzzle12(
            "EEEEE",
            "EXXXX",
            "EEEEE",
            "EXXXX",
            "EEEEE"
        );
        var result = _puzzle.SolvePart2();
        result.Should().Be(236);
    }

    [Fact]
    public void TestPart2Example3()
    {
        _puzzle = new Puzzle12(
            "AAAAAA",
            "AAABBA",
            "AAABBA",
            "ABBAAA",
            "ABBAAA",
            "AAAAAA"
        );
        var result = _puzzle.SolvePart2();
        result.Should().Be(368);
    }

    [Fact]
    public void TestPart2Example4()
    {
        _puzzle = new Puzzle12(
            "OOOOO",
            "OXOXO",
            "OOOOO",
            "OXOXO",
            "OOOOO"
        );
        var result = _puzzle.SolvePart2();
        result.Should().Be(436);
    }

    [Fact]
    public void TestPart2Example5()
    {
        _puzzle = new Puzzle12(
            "RRRRIICCFF",
            "RRRRIICCCF",
            "VVRRRCCFFF",
            "VVRCCCJFFF",
            "VVVVCJJCFE",
            "VVIVCCJJEE",
            "VVIIICJJEE",
            "MIIIIIJJEE",
            "MIIISIJEEE",
            "MMMISSJEEE"
        );
        var result = _puzzle.SolvePart2();
        result.Should().Be(1206);
    }
}
