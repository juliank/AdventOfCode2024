namespace AdventOfCode.Puzzles;

public class Puzzle06 : Puzzle<string, long>
{
    private const int PuzzleId = 06;

    public Puzzle06() : base(PuzzleId) { }

    public Puzzle06(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }


    readonly HashSet<Point> _obstacles = new HashSet<Point>();
    BoundedPoint _guard = default!; // Guaranteed to be assigned in ProcessInput

    private void ProcessInput()
    {
        /// InputEntries[y][x] (rows,columns)
        var rows = InputEntries.Count;
        var columns = InputEntries[0].Length;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (InputEntries[y][x] == '#')
                {
                    _obstacles.Add(new Point(x, y));
                }
                else if (InputEntries[y][x] == '^')
                {
                    _guard = new BoundedPoint(new Point(x, y))
                    {
                        MinX = 0, MaxX = columns - 1,
                        MinY = 0, MaxY = rows - 1
                    };
                }
            }
        }
    }

    public override long SolvePart1()
    {
        ProcessInput();

        var direction = Direction.N;
        var visitedPositions = new HashSet<Point> { _guard.Point };
        while (_guard.TryGetDirection(direction, out var nextPosition))
        {
            if (_obstacles.Contains(nextPosition))
            {
                direction = direction.Rotate(90);
            }
            else
            {
                visitedPositions.Add(nextPosition);
                _guard = _guard with { Point = nextPosition };
            }
        }

        return visitedPositions.Count;
    }

    /// <summary>
    /// Brute-force. See <see cref="SolvePart2WorkingWithExample"/> for a presumably
    /// smarter solution. Which sadly only works for the example input...
    /// </summary>
    public override long SolvePart2()
    {
        ThrowHardCodedResult(1719, "Solution time (00:00:03.4407690) should be improved", notFromTest: true);
        ProcessInput();

        var direction = Direction.N;
        var newObstacles = new HashSet<Point>();
        var guard = _guard;
        while (guard.TryGetDirection(direction, out var nextPosition))
        {
            if (_obstacles.Contains(nextPosition))
            {
                direction = direction.Rotate(90);
            }
            else
            {
                if (CausesLoop([.. _obstacles, nextPosition]) && nextPosition != _guard.Point)
                {
                    newObstacles.Add(nextPosition);
                }
                guard = guard with { Point = nextPosition };
            }
        }

        return newObstacles.Count;
    }

    private bool CausesLoop(HashSet<Point> obstacles)
    {
        var loop = false;
        var direction = Direction.N;
        var guard = _guard;
        var visitedPositions = new Dictionary<Point, List<Direction>> { {_guard.Point, [ direction ]}};

        while (guard.TryGetDirection(direction, out var nextPosition))
        {
            if (obstacles.Contains(nextPosition))
            {
                direction = direction.Rotate(90);
                continue;
            }
            
            if (visitedPositions.TryGetValue(nextPosition, out var directions))
            {
                if (directions.Contains(direction))
                {
                    loop = true;
                    break;
                }
                directions.Add(direction);
            }
            else
            {
                visitedPositions.Add(nextPosition, [direction]);
            }

            guard = guard with { Point = nextPosition };
        }

        return loop;
    }

    public long SolvePart2WorkingWithExample()
    {
        ProcessInput();

        var direction = Direction.N;

        var visitedPositions = new Dictionary<Point, List<Direction>> { {_guard.Point, [ direction ]}};
        var guardStartingPoint = _guard.Point; // We can't place a new obstacle here
        var newObstacles = new HashSet<Point>();

        while (_guard.TryGetDirection(direction, out var nextPosition))
        {
            if (_obstacles.Contains(nextPosition))
            {
                direction = direction.Rotate(90);
            }
            else
            {
                // When "re-visiting" a point, if we can make the guard turn (90 degrees to the right)
                // in the same direction as the last time the same point was visited, we have a loop.
                if (visitedPositions.TryGetValue(nextPosition, out var directions))
                {
                    if (!directions.Contains(direction))
                    {
                        // We've not visited nextPosition in this direction before,
                        // so add the direction to nextPosition's list of directions
                        directions.Add(direction);
                    }
                    // else: we're already *in* a loop (should probably not happen here?)

                    // If we, by rotation 90 degrees at nextPosition, end up in a direction
                    // we've already visited at nextPosition, we've created a loop.
                    // => Add possible obstacle to force this loop
                    var rotatedDirection = direction.Rotate(90);
                    if (directions.Contains(rotatedDirection))
                    {
                        // By adding an obstacle another point in the current direction,
                        // the guard will be forced to rotate into the looping path.
                        var newObstacle = nextPosition.Get(direction);
                        if (newObstacle != guardStartingPoint)
                        {
                            newObstacles.Add(newObstacle);
                        }
                    }
                }
                // What if we do a "double rotation" on the same spot? Will that be handled correctly?
                // As in we hit an obstacle, rotate, and then immediately place our one new obstacle?
                else
                {
                    // If we instead of moving to the next position can place an obstacle there,
                    // resulting in moving in the rotated direction leads us to a position
                    // where we've been *in the same direction*, we have hit a loop
                    var rotatedDirection = direction.Rotate(90);
                    var loop = false;
                    var g = _guard;
                    while (g.TryGetDirection(rotatedDirection, out var nextPositionIfObstacle))
                    {
                        if (_obstacles.Contains(nextPositionIfObstacle))
                        {
                            break;
                        }
                        if (visitedPositions.TryGetValue(nextPositionIfObstacle, out directions))
                        {
                            if (directions.Contains(rotatedDirection))
                            {
                                loop = true;
                            }
                        }
                        g = g with { Point = nextPositionIfObstacle };
                    }
                    if (loop)
                    {
                        newObstacles.Add(nextPosition);
                    }
                    visitedPositions.Add(nextPosition, [ direction ]);
                }
                _guard = _guard with { Point = nextPosition };
            }
        }

        return newObstacles.Count; // 264 is too low // 654 is also too low
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
