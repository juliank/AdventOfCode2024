namespace AdventOfCode.Tests.Puzzles;

public class Puzzle09Tests
{
    private Puzzle09 _puzzle;

    public Puzzle09Tests()
    {
        _puzzle = new Puzzle09();
    }

    [Fact]
    public void SolvePart1()
    {
        var result = _puzzle.SolvePart1();
        result.Should().Be(6201130364722);
    }

    [Fact]
    public void SolvePart2()
    {
        var result = _puzzle.SolvePart2();
        result.Should().Be(6221662795602);
        
    }

    [Fact]
    public void TestCreateDiskLayoutPart1()
    {
        var diskLayout = Puzzle09.CreateDiskLayout("2333133121414131402".Select(c => int.Parse(c.ToString())).ToArray());
        var compactedDiskLayout = Puzzle09.CompactDiskLayoutWithFragmentation(diskLayout);
        
        var expected = "0099811188827773336446555566".Select(c => int.Parse(c.ToString())).ToArray();
        compactedDiskLayout.Should().Equal(expected);
    }

    [Fact]
    public void TestCreateDiskLayoutPart2Example()
    {
        var diskMap = "2333133121414131402".Select(c => int.Parse(c.ToString())).ToArray();

        _puzzle = new Puzzle09(diskMap);
        var checksum = _puzzle.SolvePart2();
        checksum.Should().Be(2858);
    }
}
