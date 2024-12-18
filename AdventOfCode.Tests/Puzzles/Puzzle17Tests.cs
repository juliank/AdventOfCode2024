namespace AdventOfCode.Tests.Puzzles;

public class Puzzle17Tests
{
    private Puzzle17 _puzzle;

    public Puzzle17Tests()
    {
        _puzzle = new Puzzle17();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be("4,6,1,4,2,1,3,1,6");
    }

    [Fact(Skip = "Not yet implemented")]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be("");
    }

    [Fact]
    public void TestPart1Example1()
    {
        _puzzle = new Puzzle17(
            "Register A: 0",
            "Register B: 0",
            "Register C: 9",
            "",
            "Program: 2,6"
        );
        _ = _puzzle.SolvePart1();
        _puzzle.B.Should().Be(1);
    }

    [Fact]
    public void TestPart1Example2()
    {
        _puzzle = new Puzzle17(
            "Register A: 10",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 5,0,5,1,5,4"
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be("0,1,2");
    }

    [Fact]
    public void TestPart1Example3()
    {
        _puzzle = new Puzzle17(
            "Register A: 2024",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,1,5,4,3,0"
        );
        var result = _puzzle.SolvePart1();
        result.Should().Be("4,2,5,6,7,7,7,7,3,1,0");
        _puzzle.A.Should().Be(0);
    }

    [Fact]
    public void TestPart1Example4()
    {
        _puzzle = new Puzzle17(
            "Register A: 0",
            "Register B: 29",
            "Register C: 0",
            "",
            "Program: 1,7"
        );
        _ = _puzzle.SolvePart1();
        _puzzle.B.Should().Be(26);
    }

    [Fact]
    public void TestPart1Example5()
    {
        _puzzle = new Puzzle17(
            "Register A: 0",
            "Register B: 2024",
            "Register C: 43690",
            "",
            "Program: 4,0"
        );
        _ = _puzzle.SolvePart1();
        _puzzle.B.Should().Be(44354);
    }

    [Fact]
    public void TestPart2Example1()
    {
        _puzzle = new Puzzle17(
            "Register A: 2024",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,3,5,4,3,0"
        );
        _puzzle.RunningFromTests = true;
        var result = _puzzle.SolvePart2();
        result.Should().Be("117440");
    }
}
