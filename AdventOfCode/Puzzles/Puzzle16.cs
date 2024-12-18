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
    internal bool PrintMap = false;

    public override long SolvePart1()
    {
        _walls = InputMap.Where(im => im.Value == '#').Select(im => im.Key).ToHashSet();
        _endPoint = InputMap.Single(im => im.Value == 'E').Key;
        _startPoint = InputMap.Single(im => im.Value == 'S').Key;
        var maxX = _walls.Max(w => w.X);
        var maxY = _walls.Max(w => w.Y);
        _boundary = new Boundary(0, 0, maxX, maxY);
        
        // Solution using HashSetTree
        // var visitedPoints = new Dictionary<Point, long>();
        // ScorePaths(_endPoint, visitedPoints);
        // var score = visitedPoints[_startPoint];
        // return score;
        // The last implementation (using HashSetTree) ran through:
        // Solution time: 00:05:04.9192450
        // Result is: [105500]
        // But 105500 not the right answer, it is too high :-|
        // (It is not an off-by-one error - 105449 is also too high...)

        var path = AStarPathfinding.FindShortestPath(_startPoint, _endPoint, _boundary, _walls, CostFunction);
        
        // PrintMap = true;
        // PrintMapToConsole(path.Last(), path);
        
        int score = ScorePath(path);
        // 105516 is too high (it is also slightly higher than the answer from the previous semi-successful attempt: 105500...)
        return score;
    }

    private int ScorePath(List<Point> path)
    {
        var score = CostFunction(null, path[0], path[1]);
        for (int i = 2; i < path.Count - 1; i++)
        {
            score += CostFunction(path[i - 1], path[i], path[i + 1]);
        }

        return score + 1; // Not sure why, but there seems to be an off-by-one error somewhere...
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

    // Brute-force attempt...
    private void ScorePaths(Point position, Dictionary<Point, long> visitedPoints, HashSetTree<Point>? parent = null)
    {
        var node = new HashSetTree<Point>(position);
        parent?.Add(node); // If parent is null, this is the root node

        PrintMapToConsole(position, node.GetParents());

        List<Point> path = node.GetParents().Reverse().ToList();
        var score = path.Count < 2 ? 0 : ScorePath(path);

        if (visitedPoints.TryGetValue(position, out var currentBestScore))
        {
            if (score < currentBestScore)
            {
                visitedPoints[position] = score;
            }
            else
            {
                // There is no need to continue with this path, since we've
                // already been at this point with a better score
                return;
            }
        }
        else
        {
            visitedPoints[position] = score;
        }

        if (position == _startPoint)
        {
            // We've traversed the whole map
            return;
        }

        foreach (var direction in Directions.D2)
        {
            var nextPosition = position.Get(direction);
            // Don't run into walls, and don't backtrack on the path we're already on
            if (_walls.Contains(nextPosition) || parent?.Value == nextPosition)
            {
                continue;
            }
            ScorePaths(nextPosition, visitedPoints, node);
        }
    }

    private void PrintMapToConsole(Point position, IEnumerable<Point> path)//, IEnumerable<Point>? visitedPoints = null)
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
