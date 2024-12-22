namespace AdventOfCode.Tests.Puzzles;

public class Puzzle22Tests
{
    private Puzzle22 _puzzle;

    public Puzzle22Tests()
    {
        _puzzle = new Puzzle22();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(13185239446);
    }

    [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(0);
    }

    [Fact]
    public void TestPart1Example()
    {
        _puzzle = new Puzzle22(1, 10, 100, 2024);
        var result = _puzzle.SolvePart1();
        result.Should().Be(37327623);
    }
    
    [Theory]
    [InlineData(123, 3)]
    [InlineData(15887950, 0)]
    [InlineData(11100544, 4)]
    public void TestGet(long number, long expectedOneDigit)
    {
        var oneDigit = Puzzle22.GetOneDigit(number);
        oneDigit.Should().Be(expectedOneDigit);
    }

    [Theory]
    [InlineData(123, 15887950)]
    [InlineData(15887950, 16495136)]
    [InlineData(16495136, 527345)]
    [InlineData(527345, 704524)]
    [InlineData(704524, 1553684)]
    [InlineData(1553684, 12683156)]
    [InlineData(12683156, 11100544)]
    [InlineData(11100544, 12249484)]
    [InlineData(12249484, 7753432)]
    [InlineData(7753432, 5908254)]
    public void TestGetNextSecret(long secret, long expectedNextSecret)
    {
        var nextSecret = Puzzle22.NextSecret(secret);
        nextSecret.Should().Be(expectedNextSecret);
    }

    [Theory]
    [InlineData(123, 10, 5908254)]
    [InlineData(1, 2000, 8685429)]
    [InlineData(10, 2000, 4700978)]
    [InlineData(100, 2000, 15273692)]
    [InlineData(2024, 2000, 8667524)]
    public void TestGetNextSecrets(long initialSecret, int iterations, long expectedSecret)
    {
        var nextSecret = Puzzle22.NextSecrets(initialSecret, iterations).Last();
        nextSecret.Should().Be(expectedSecret);
    }
}
