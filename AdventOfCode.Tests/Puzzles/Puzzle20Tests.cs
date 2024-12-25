namespace AdventOfCode.Tests.Puzzles;

public class Puzzle20Tests
{
    private Puzzle20 _puzzle;

    public Puzzle20Tests()
    {
        _puzzle = new Puzzle20();
    }

    [Fact(Skip = "Too slow with real input")]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(1485);
    }

    [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Theory]
    [InlineData( 2, 44)]
    [InlineData( 4, 30)]
    [InlineData( 6, 16)]
    [InlineData( 8, 14)]
    [InlineData(10, 10)]
    [InlineData(12,  8)]
    [InlineData(20,  5)]
    [InlineData(36,  4)]
    [InlineData(38,  3)]
    [InlineData(40,  2)]
    [InlineData(64,  1)]
    public void TestPart1Example(int cheatTime, int expectedCheats)
    {
        _puzzle = new Puzzle20(cheatTime,
            "###############",
            "#...#...#.....#",
            "#.#.#.#.#.###.#",
            "#S#...#.#.#...#",
            "#######.#.#.###",
            "#######.#.#...#",
            "#######.#.###.#",
            "###..E#...#...#",
            "###.#######.###",
            "#...###...#...#",
            "#.#####.#.###.#",
            "#.#...#.#.#...#",
            "#.#.#.#.#.#.###",
            "#...#...#...###",
            "###############"
        );
        _puzzle.IsRunningFromTest = true;
        var result = _puzzle.SolvePart1();
        result.Should().Be(expectedCheats);
    }
}
