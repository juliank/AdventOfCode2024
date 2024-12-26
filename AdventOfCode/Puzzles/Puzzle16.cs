namespace AdventOfCode.Puzzles;

public class Puzzle16 : Puzzle<string, long>
{
    private const int PuzzleId = 16;

    public Puzzle16() : base(PuzzleId) { }

    public Puzzle16(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }
    
    private HashSet<Point> _walls = [];
    private Point _endPoint;
    private Point _startPoint;

    private Boundary _boundary = null!; // For easier debugging
    internal bool PrintMap;

    public override long SolvePart1()
    {
        ThrowHardCodedResult(105516, "The result is wrong, but tests work on all examples", notFromTest: true);
        _walls = InputMap.Where(im => im.Value == '#').Select(im => im.Key).ToHashSet();
        _endPoint = InputMap.Single(im => im.Value == 'E').Key;
        _startPoint = InputMap.Single(im => im.Value == 'S').Key;
        var maxX = _walls.Max(w => w.X);
        var maxY = _walls.Max(w => w.Y);
        _boundary = new Boundary(0, 0, maxX, maxY);

        var path = AStarPathfinding.FindShortestPath(_startPoint, _endPoint, _boundary, _walls, CostFunction);
        
        PrintMap = false;
        PrintMapToConsole(path.Last(), path);
        
        int score = ScorePath(path);
        // 105500 not the right answer, it is too high :-|
        // (It is not an off-by-one error - 105449 is also too high...)
        // 105516 is too high (it is also slightly higher than the answer from the previous semi-successful attempt: 105500...)
        return score;
    }

    private int ScorePath(List<Point> path)
    {
        var score = CostFunction(null, path[0], path[1]);
        for (int i = 1; i < path.Count - 1; i++)
        {
            score += CostFunction(path[i - 1], path[i], path[i + 1]);
        }

        return score;
    }

    private int CostFunction(Point? previous, Point current, Point next)
    {
        // When current is the starting point, we fake that the previous one
        // is the point to the west, since the initial traversal direction
        // from the starting point should be westwards.
        previous ??= _startPoint.GetE();

        // If the previous and the next point are "in line", we're heading
        // one step forward from current to next => cost is 1.
        // If not, it means we'll have to first take a turn at current (cost 1000),
        // and then move one step forward to next (cost 1) => cost is 1001
        return previous.Value.X == next.X || previous.Value.Y == next.Y ? 1 : 1001;
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    private void PrintMapToConsole(Point position, IEnumerable<Point> path)
    {
        var walls = _walls.Select(w => (w, '#'));
        var e = (_endPoint, 'E');
        var s = (_startPoint, 'S');
        var pos = (position, '*');
        var pat = path.Select(vp => (vp, '.'));
        
        if (PrintMap)
        {
            Console.WriteLine();
            Helper.PrintMap(_boundary, [s, pos, e, ..pat, ..walls]);
        }
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
