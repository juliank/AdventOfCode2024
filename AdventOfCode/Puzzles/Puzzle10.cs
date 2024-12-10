namespace AdventOfCode.Puzzles;

public class Puzzle10 : Puzzle<string, long>
{
    private const int PuzzleId = 10;

    public Puzzle10() : base(PuzzleId) { }

    public Puzzle10(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    private int _maxX;
    private int _maxY;
    private readonly Dictionary<Point, int> _map = [];
    private readonly List<Point> _trailheads = [];
    private readonly Dictionary<Point, HashSet<Point>> _trails = []; // Key: trail end (point with height 9), Values: Possible starting points
    private readonly Dictionary<Point, long> _scores = [];


    public override long SolvePart1()
    {
        ProcessInput();
        ScorePositions();
        var scores = _trails.Select(x => (StartingPosition: x.Key, TrailScore: x.Value.Count)).ToArray();
        var scoreSum = scores.Sum(s => s.TrailScore);
        return scoreSum;
    }

    public override long SolvePart2()
    {
        ProcessInput();
        ScorePositions();
        var scoreSum = _scores.Values.Sum();
        return scoreSum;
    }

    private void ScorePositions()
    {
        foreach (var trailhead in _trailheads)
        {
            var score = ScorePosition(trailhead, trailhead);
            _scores.Add(trailhead, score);
        }
    }

    private long ScorePosition(Point position, Point startingPosition)
    {
        var currentHeight = _map[position];
        if (currentHeight == 9)
        {
            if (!_trails.TryGetValue(position, out var startingPositions))
            {
                startingPositions = [];
                _trails.Add(position, startingPositions);
            }
            
            // For solving part 1
            startingPositions.Add(startingPosition);
            
            // For solving part 2
            return 1;
        }
        
        var positionScore = 0L;
        foreach (var direction in Directions.D2)
        {
            var score = ScorePosition(position, direction, startingPosition);
            positionScore += score;
        }

        return positionScore;
    }

    private long ScorePosition(Point position, Direction direction, Point startingPosition)
    {
        var currentHeight = _map[position];
        
        var nextPosition = position.Get(direction);
        if (nextPosition.X < 0 || nextPosition.X > _maxX || nextPosition.Y < 0 || nextPosition.Y > _maxY)
        {
            return 0;
        }
        var nextHeight = _map[nextPosition];
        if (nextHeight != currentHeight + 1)
        {
            return 0;
        }

        return ScorePosition(nextPosition, startingPosition);
    }

    private void ProcessInput()
    {
        // InputEntries[y][x] (rows,columns)
        var rows = InputEntries.Count;
        var columns = InputEntries[0].Length;
        _maxX = columns - 1;
        _maxY = rows - 1;

        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < columns; x++)
            {
                var position = new Point(x, y);
                var height = int.Parse(InputEntries[y][x].ToString());
                _map.Add(position, height);
                if (height == 0)
                {
                    _trailheads.Add(position);
                }
            }
        }
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
