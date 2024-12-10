namespace AdventOfCode.Tests.Puzzles;

public class Puzzle10Tests
{
    private Puzzle10 _puzzle;

    public Puzzle10Tests()
    {
        _puzzle = new Puzzle10();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(688);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(1459);
    }

    [Fact]
    public void Part1Example()
    {
        _puzzle = new Puzzle10(
            "89010123",
            "78121874",
            "87430965",
            "96549874",
            "45678903",
            "32019012",
            "01329801",
            "10456732"
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be(36);
    }

    [Fact]
    public void Part2Example()
    {
        _puzzle = new Puzzle10(
            "89010123",
            "78121874",
            "87430965",
            "96549874",
            "45678903",
            "32019012",
            "01329801",
            "10456732"
        );
        var result = _puzzle.SolvePart2();
        result.Should().Be(81);
    }

    [Fact]
    public void Part1Test1()
    {
        _puzzle = new Puzzle10(
            "9990999",
            "9991999",
            "9992999",
            "6543456",
            "7111117",
            "8111118",
            "9111119"
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be(2);
    }

    [Fact]
    public void Part1Test2()
    {
        _puzzle = new Puzzle10(
            "1190779", // ..90..9 // every 9 is reachable via a hiking trail except the one immediately to the left of the 0
            "1171798", // ...1.98 // => score 4
            "1172717", // ...2..7
            "6543456", // 6543456
            "7651987", // 765.987
            "8761111", // 876....
            "9871111"  // 987....
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be(4);
    }
}
