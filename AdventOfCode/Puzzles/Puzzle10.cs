namespace AdventOfCode.Puzzles;

public class Puzzle10 : Puzzle<string, long>
{
    private const int PuzzleId = 10;

    public Puzzle10() : base(PuzzleId) { }

    public Puzzle10(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    private readonly Dictionary<BoundedPoint, int> _map = [];
    private readonly Dictionary<int, List<BoundedPoint>> _heights = new()
    {
        {0, new List<BoundedPoint>()},
        {1, new List<BoundedPoint>()},
        {2, new List<BoundedPoint>()},
        {3, new List<BoundedPoint>()},
        {4, new List<BoundedPoint>()},
        {5, new List<BoundedPoint>()},
        {6, new List<BoundedPoint>()},
        {7, new List<BoundedPoint>()},
        {8, new List<BoundedPoint>()},
        {9, new List<BoundedPoint>()},
    };

    public override long SolvePart1()
    {
        ProcessInput();
        ScorePositions();
    }

    private void ScorePositions()
    {
        foreach (var position in _heights[9])
        {
            ScorePosition(position);
        }
    }

    private void ScorePosition(BoundedPoint position)
    {
        throw new NotImplementedException();
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    private void ProcessInput()
    {
        /// InputEntries[y][x] (rows,columns)
        var rows = InputEntries.Count;
        var columns = InputEntries[0].Length;
        int maxX = columns - 1;
        int maxY = rows - 1;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                var position = new BoundedPoint(new Point(x, y))
                    {
                        MinX = 0, MaxX = maxX,
                        MinY = 0, MaxY = maxY
                };
                var height = int.Parse(InputEntries[y][x].ToString());
                _map.Add(position, height);
                _heights[height].Add(position);
            }
        }
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
