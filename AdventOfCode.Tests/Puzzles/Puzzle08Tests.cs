namespace AdventOfCode.Tests.Puzzles;

public class Puzzle08Tests
{
    private Puzzle08 _puzzle;

    public Puzzle08Tests()
    {
        _puzzle = new Puzzle08();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(308);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(1147);
    }

    [Fact]
    public void Part1ExampleInput()
    {
        _puzzle = new Puzzle08(
            "............",
            "........0...",
            ".....0......",
            ".......0....",
            "....0.......",
            "......A.....",
            "............",
            "............",
            "........A...",
            ".........A..",
            "............",
            "............"
        );

        var answer = _puzzle.SolvePart1();
        answer.Should().Be(14);
    }

    [Fact]
    public void Part1SimpleInput1()
    {
        _puzzle = new Puzzle08(
            "........#.", // (8,0)
            "..........",
            "......a...", // (6,2)
            "..........",
            "....a.....", // (4,4)
            "..........",
            ".#........", // (2,6)
            "..........",
            "..........",
            ".........."
        );

        var answer = _puzzle.SolvePart1();
        answer.Should().Be(2);
    }

    [Fact]
    public void Part1SimpleInput2()
    {
        _puzzle = new Puzzle08(
            "......#...", // (6,0)
            "..........",
            "......a...", // (6,2)
            "..........",
            "......a...", // (6,4)
            "..........",
            "......#...", // (6,6)
            "..........",
            "..........",
            ".........."
        );

        var answer = _puzzle.SolvePart1();
        answer.Should().Be(2);
    }

    [Fact]
    public void Part1SimpleInput3()
    {
        _puzzle = new Puzzle08(
            "..........",
            "..........",
            "..#.a.a.#.", // (2,2) (4,2) (6,2) (8,2)
            "..........",
            "..........",
            "..........",
            "..........",
            "..........",
            "..........",
            ".........."
        );

        var answer = _puzzle.SolvePart1();
        answer.Should().Be(2);
    }

    [Fact]
    public void Part1SimpleInput4()
    {
        _puzzle = new Puzzle08(
            "..........",
            "...#......",
            "#.........",
            "....a.....",
            "........a.",
            ".....a....",
            "..#.......",
            "......A...", // # at position of A
            "..........",
            ".........."
        );

        var answer = _puzzle.SolvePart1();
        answer.Should().Be(4);
    }
}
