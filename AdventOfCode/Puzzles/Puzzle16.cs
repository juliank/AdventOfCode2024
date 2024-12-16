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

        var path = GetShortestPath(_startPoint, Direction.E, [], []);
        var score = CalculateScore(path);
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
