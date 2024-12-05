namespace AdventOfCode.Tests.Puzzles;

public class Puzzle05Tests
{
    private Puzzle05 _puzzle;

    public Puzzle05Tests()
    {
        _puzzle = new Puzzle05();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(5108);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(7380);
    }

    [Fact]
    public void Part2ExampleInput()
    {
        _puzzle = new Puzzle05(
            "47|53",
            "97|13",
            "97|61",
            "97|47",
            "75|29",
            "61|13",
            "75|53",
            "29|13",
            "97|29",
            "53|29",
            "61|53",
            "97|53",
            "61|29",
            "47|13",
            "75|47",
            "97|75",
            "47|61",
            "75|61",
            "47|29",
            "75|13",
            "53|13",
            "",
            "75,47,61,53,29",
            "97,61,53,29,13",
            "75,29,13",
            "75,97,47,61,53",
            "61,13,29",
            "97,13,75,29,47"
        );

        var answer = _puzzle.SolvePart2();

        answer.Should().Be(123);
    }
}
