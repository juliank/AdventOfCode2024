namespace AdventOfCode.Puzzles;

public class Puzzle16 : Puzzle<string, long>
{
    private const int PuzzleId = 16;

    public Puzzle16() : base(PuzzleId) { }

    public Puzzle16(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }
    
    private HashSet<Point> _walls = [];
    private Point _endPoint;
    private Point _startPoint;
    private readonly int[] _rotations = [0, 90, 270]; // Front, Right, Left
    private const char F = 'F', R = 'R', L = 'L';
    private readonly char[] _headings = [ F, R, L ];
    private readonly Dictionary<Point, long> _positionPoints = []; // The (currently) best score at the given position

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

        // var path = GetShortestPath(_startPoint, Direction.E, [], []);
        // var score = CalculateScore(path);
        // var visitedPoints = new Dictionary<Point, long>();
        
        // ScorePaths(_endPoint, visitedPoints);
        // var score = visitedPoints[_startPoint];
        // return score;
        // The last implementation (using HashSetTree) ran through:
        // Solution time: 00:05:04.9192450
        // Result is: [105500]
        // But 105500 not the right answer, it is too high :-|
        // (It is not an off-by-one error - 105449 is also too high...)

        int CostFunction(Point? previous, Point current, Point next)
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

        var path = AStarPathfinding.FindShortestPath(_startPoint, _endPoint, _boundary, _walls, CostFunction);
        
        // PrintMapToConsole(path.Last(), path);
        var score = CostFunction(null, path[0], path[1]);
        for (int i = 2; i < path.Count - 1; i++)
        {
            score += CostFunction(path[i - 1], path[i], path[i + 1]);
        }
        
        return score + 1; // Not sure why, but there seems to be an off-by-one error somewhere...
        // 105516 is too high (it is also slightly higher than the answer from the previous semi-successful attempt: 105500...)
        // return score;
    }

    // Try the following:
    // - Traverse from end
    // - Keep track of the path
    //   - Create method for calculating points based on a list of points
    // - For every visited step
    //   - If: first visit: cache (point, path, score)
    //   - If else: score is lower than cached score: update cache
    //   - Else: Abort traversal
    // - Continue until start point
    //   - Remember start point direction!

    private void ScorePaths(Point position, Dictionary<Point, long> visitedPoints, HashSetTree<Point>? parent = null)
    {
        // List<Point> newPath = [ position, ..path ];
        var node = new HashSetTree<Point>(position);
        parent?.Add(node); // If parent is null, this is the root node

        PrintMapToConsole(position, node.GetParents());

        var score = ScorePath(node.GetParents().ToList());
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

    private long ScorePath(List<Point> path)
    {
        if (path.Count == 1)
        {
            return 0;
        }

        if (path.Count == 2)
        {
            return 1;
        }

        var score = 0L;
        for (int i = path.Count - 1; i >= 1; i--)
        {
            var p = path[i];
            var p1 = path[i - 1];
            Point p2;
            if (i == 1)
            {
                // Special handling when we're at the first point of the path
                if (p1 == _startPoint)
                {
                    // Since the puzzle text says that we should start in the East direction
                    // from the starting point, we "simulate" a point prior to this one step
                    // in the West direction
                    p2 = _startPoint.Get(Direction.W);
                }
                else
                {
                    // We create a virtual point one step further in a "straight" direction
                    var dx = p.X - p1.X;
                    var dy = p.Y - p1.Y;
                    p2 = new Point(p1.X + dx, p1.Y + dy);
                }
            }
            else
            {
                p2 = path[i - 2];
            }
            if ((p.X == p1.X && p1.X == p2.X) ||
                (p.Y == p1.Y && p1.Y == p2.Y) )
            {
                // - p is "in line" with the two previous points
                // - We can get to p from p1 by a simple step forward
                // - Score => 1
                score += 1;
            }
            else
            {
                // - p is not "in line" with the two previous points
                // - We must first *rotate* at p1 (1000) and *then* move a step forward to p
                // - Score => 1001
                score += 1001;
            }
        }

        // We must subtract one point because of the virtual p2 we create in the final iteration
        // score--;

        return score;
    }

    private long CalculateScore(List<char> path)
    {
        var points = 0L;
        foreach (var move in path)
        {
            var movePoints = move == F ? 1 : 1000;
            points += movePoints;
        }
        return points - 1; // There is an off-by-one error somewhere...
    }

    private List<char> GetShortestPath(Point position, Direction direction, HashSet<Point> visitedPoints, List<char> trail)
    {
        PrintMapToConsole(position, direction, visitedPoints);
        // visitedPoints.Add(position);
        visitedPoints = [ ..visitedPoints, position ];

        var possiblePaths = new List<List<char>>();
        foreach (var heading in _headings)
        {
            var rotation = heading switch { L => 270, R => 90, _ => 0 };
            var nextDirection = direction.Rotate(rotation);
            var nextPosition = position.Get(nextDirection);
            if (nextPosition == _endPoint)
            {
                return [ heading, F ]; // Move in 'heading' from the current position, and one step Forward to the end point
            }

            if (_walls.Contains(nextPosition))
            {
                continue; // Dead end
            }
            if (visitedPoints.Contains(nextPosition))
            {
                continue; // Loop
            }

            var t = trail.Append(heading);
            if (heading != F)
            {
                t = t.Append(F);
            }

            var nextTrail = t.ToList();
            var nextTrailScore = CalculateScore(nextTrail);
            if (_positionPoints.TryGetValue(nextPosition, out var nextPositionScore))
            {
                if (nextTrailScore > nextPositionScore)
                {
                    continue; // No need to continue this path, as we're already above the (current) best score for the next point
                }
            }
            var path = GetShortestPath(nextPosition, nextDirection, visitedPoints, nextTrail);
            if (path.Count == 0)
            {
                continue; // Path didn't lead to the end point
            }

            if (heading != F)
            {
                // If a rotation leads on, we must also include a step forward
                path.Insert(0, F);
            }
            path.Insert(0, heading);
            possiblePaths.Add(path);
        }

        // return possiblePaths.OrderBy(CalculateScore).FirstOrDefault() ?? [];
        // return possiblePaths.OrderBy(p => p.Count).FirstOrDefault() ?? [];
        if (possiblePaths.Count == 0)
        {
            return [];
        }

        var bestPath = possiblePaths.Select(p => (Path: p, Score: CalculateScore(p))).OrderBy(p => p.Score).First();
        if (_positionPoints.TryGetValue(position, out var positionScore) || bestPath.Score < positionScore)
        {
            _positionPoints[position] = bestPath.Score;
        }

        return bestPath.Path;
    }

    private void PrintMapToConsole(Point position, IEnumerable<Point> path)//, IEnumerable<Point>? visitedPoints = null)
    {
        var walls = _walls.Select(w => (w, '#'));
        var e = (_endPoint, 'E');
        var s = (_startPoint, 'S');
        var pos = (position, '*');
        var pat = path.Select(vp => (vp, '.'));
        // visitedPoints ??= [];
        // if (PrintMap)
        {
            Console.WriteLine();
            Helper.PrintMap(_boundary, [pos, e, ..pat, ..walls]);
        }
    }
    private void PrintMapToConsole(Point position, Direction direction, HashSet<Point> visitedPoints)
    {
        var walls = _walls.Select(w => (w, '#'));
        var e = (_endPoint, 'E');
        var rd = (position, direction.ToString()[0]);
        var vps = visitedPoints.Select(vp => (vp, '.'));
        if (PrintMap)
        {
            Console.WriteLine();
            Helper.PrintMap(_boundary, [rd, e, ..vps, ..walls]);
        }
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
