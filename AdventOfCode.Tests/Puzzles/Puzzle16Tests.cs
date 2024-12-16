namespace AdventOfCode.Tests.Puzzles;

public class Puzzle16Tests
{
    private Puzzle16 _puzzle;

    public Puzzle16Tests()
    {
        _puzzle = new Puzzle16();
    }

    // [Fact(Skip = "Not yet implemented")]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(0);
    }

    // [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Fact]
    public void TestPart1Example1()
    {
        _puzzle = new Puzzle16(
            "###############",
            "#.......#....E#",
            "#.#.###.#.###.#",
            "#.....#.#...#.#",
            "#.###.#####.#.#",
            "#.#.#.......#.#",
            "#.#.#####.###.#",
            "#...........#.#",
            "###.#.#####.#.#",
            "#...#.....#.#.#",
            "#.#.#.###.#.#.#",
            "#.....#...#.#.#",
            "#.###.#.#.#.#.#",
            "#S..#.....#...#",
            "###############"
        );
        // _puzzle.PrintMap = true;
        var result = _puzzle.SolvePart1();
        result.Should().Be(7036);
    }
}
