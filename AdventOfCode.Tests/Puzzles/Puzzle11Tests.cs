namespace AdventOfCode.Tests.Puzzles;

public class Puzzle11Tests
{
    private Puzzle11 _puzzle;

    public Puzzle11Tests()
    {
        _puzzle = new Puzzle11();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(187738);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(223767210249237);
    }

    [Fact]
    public void TestPart1Example()
    {
        long[] numbers = [125,17];
        _puzzle = new Puzzle11(numbers);
        var result = _puzzle.SolvePart1();
        result.Should().Be(55312);
    }

    [Theory]
    [InlineData(new long[]{125,17}, 1, 3)] // 253000 1 7
    [InlineData(new long[]{125,17}, 2, 4)] // 253 0 2024 14168
    [InlineData(new long[]{125,17}, 3, 5)] // 512072 1 20 24 28676032
    [InlineData(new long[]{125,17}, 4, 9)] // 512 72 2024 2 0 2 4 2867 6032
    [InlineData(new long[]{512,72,2024,2,0,2,4,2867,6032}, 1, 13)] // 512 72 2024 2 0 2 4 2867 6032
    [InlineData(new long[]{125,17}, 5, 13)] // 1036288 7 2 20 24 4048 1 4048 8096 28 67 60 32
    [InlineData(new long[]{125,17}, 6, 22)] // 2097446912 14168 4048 2 0 2 4 40 48 2024 40 48 80 96 2 8 6 7 6 0 3 2
    public void TestPart1Example1(long[] numbers, int iterations, int expectedCount)
    {
        _puzzle = new Puzzle11(numbers);
        var result = _puzzle.Blink(iterations);
        result.Should().Be(expectedCount);
    }
}
