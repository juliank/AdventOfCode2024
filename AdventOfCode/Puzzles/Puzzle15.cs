namespace AdventOfCode.Puzzles;

public class Puzzle15 : Puzzle<string, long>
{
    private const int PuzzleId = 15;

    public Puzzle15() : base(PuzzleId) { }

    public Puzzle15(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    private readonly HashSet<Point> _boxes = []; // Used by part 1
    // Key: A box part - Value: The "other" box part
    // => Each box will be represented with two keys, so one can know which "other" part also has to be moved
    private readonly Dictionary<Point, Point> _boxesX2 = []; // Used by part 2, the boxes are of double size.
    private Boundary _boundary = null!; // Will be assigned in ProcessInput2 (only used by part 2)
    
    private readonly HashSet<Point> _walls = [];
    private Point _robot = default!; // Will be properly initialized in ProcessInput
    private readonly List<Direction> _moves = [];

    public override long SolvePart1()
    {
        ProcessInput();
        MoveRobot();
        var gpsSum = CalculateGpsSum(_boxes);
        return gpsSum;
    }

    private static long CalculateGpsSum(IEnumerable<Point> boxes)
    {
        var gpsSum = 0L;
        foreach (var box in boxes)
        {
            // The GPS coordinate of a box is equal to
            //  - 100 times its distance from the top edge of the map
            //  - plus its distance from the left edge of the map.
            var gps = box.Y * 100 + box.X;
            gpsSum += gps;
        }
        return gpsSum;
    }

    private void MoveRobot()
    {
        foreach (var direction in _moves)
        {
            var nextPosition = _robot.Get(direction);
            var success = Move(nextPosition, direction);
            if (success)
            {
                _robot = nextPosition;
            }
        }
    }

    // Is it possible to move to the requested position?
    //  - Hit a wall => false
    //  - Hit a box => try to move box
    //  - ... else => true
    private bool Move(Point position, Direction direction)
    {
        // Hit wall
        if (_walls.Contains(position))
        {
            return false;
        }

        // Empty space
        if (!_boxes.Contains(position))
        {
            return true;
        }

        // Hit box
        var nextPosition = position.Get(direction);
        var canMoveBox = Move(nextPosition, direction);
        if (!canMoveBox)
        {
            // Can't move
            return false;
        }
        // Move box
        _boxes.Remove(position);
        _boxes.Add(nextPosition);
        return true;
    }

    private void ProcessInput()
    {
        // Example input:
        // ########
        // #..O.O.#
        // ##@.O..#
        // #...O..#
        // #.#.O..#
        // #...O..#
        // #......#
        // ########
        //
        // <^^>>>vv<v>>v<<
        var processMap = true;
        foreach (var (y, line) in InputEntries.Index())
        {
            if (line == string.Empty)
            {
                processMap = false;
                continue;
            }

            foreach (var (x, c) in line.Index())
            {
                if (processMap)
                {
                    switch (c)
                    {
                        case 'O':
                            _boxes.Add(new Point(x, y));
                            break;
                        case '#':
                            _walls.Add(new Point(x, y));
                            break;
                        case '@':
                            _robot = new Point(x, y);
                            break;
                    }
                }
                else // Processing directions
                {
                    // <^^>>>vv<v>>v<<
                    var d = c switch
                    {
                        '^' => Direction.N,
                        'v' => Direction.S,
                        '>' => Direction.E,
                        '<' => Direction.W,
                          _ => throw new ArgumentOutOfRangeException($"Invalid direction value '{c}'")
                    };
                    _moves.Add(d);
                }
            }
        }
    }

    public override long SolvePart2()
    {
        // _printDebugOutput = true;
        ProcessInput2();
        MoveRobot2();
        
        var leftBoxes = _boxesX2.Where(b => b.Key.X < b.Value.X).Select(b => b.Key);
        var gpsSum = CalculateGpsSum(leftBoxes);
        return gpsSum;
    }

    private bool _printDebugOutput = false;
    private void PrintCurrentMap()
    {
        if (!_printDebugOutput)
        {
            return;
        }
        var walls = _walls.Select(p => (w: p, '#'));
        var leftBoxes = _boxesX2.Where(b => b.Key.X < b.Value.X).Select(b => (b.Key, '['));
        var rightBoxes = _boxesX2.Where(b => b.Key.X > b.Value.X).Select(b => (b.Key, ']'));
        IEnumerable<(Point, char)> currentMap = [..walls, ..leftBoxes, ..rightBoxes, (_robot, '@')];
        
        Helper.PrintMap(_boundary, currentMap, w: '.');
    }

    private void MoveRobot2()
    {
        PrintCurrentMap();
        foreach (var direction in _moves)
        {
            var nextPosition = _robot.Get(direction);
            var success = Move2(nextPosition, direction, whatIf: true);
            if (success)
            {
                Move2(nextPosition, direction, whatIf: false);
                _robot = nextPosition;
                if (_printDebugOutput)
                {
                    Console.WriteLine($"Robot moved successfully {direction}");
                    PrintCurrentMap();
                }
            }
            else if (_printDebugOutput)
            {
                Console.WriteLine($"Robot couldn't move {direction}");                
            }
        }
    }

    // Is it possible to move to the requested position?
    //  - Hit a wall => false
    //  - Hit a box => try to move box
    //  - ... else => true
    private bool Move2(Point position, Direction direction, bool whatIf = false)
    {
        // Hit wall
        if (_walls.Contains(position))
        {
            return false;
        }

        // Empty space
        if (!_boxesX2.TryGetValue(position, out var boxPart))
        {
            return true;
        }

        // Hit a box...
        var boxLeft = position.X < boxPart.X ? position : boxPart;
        var boxRight = position.X > boxPart.X ? position : boxPart;
        var positionsToMove = direction switch
        {
            Direction.W => new[] { boxLeft }, // Sufficient to test is the left part of the box can move
            Direction.E => new[] { boxRight }, // Sufficient to test if the right part of the box can move
                      _ => new[] { boxLeft, boxRight } // For up and down, we must check if both parts of the box can move
        };
        
        var canMoveBox = true;
        foreach (var p in positionsToMove)
        {
            canMoveBox = Move2(p.Get(direction), direction, whatIf);
            if (!canMoveBox)
            {
                break; // No need to check the other part of the box if the first can't move
            }
        }
        if (!canMoveBox)
        {
            // Can't move
            return false;
        }

        if (!whatIf)
        {
            // Actually move the box
            _boxesX2.Remove(boxLeft);
            _boxesX2.Remove(boxRight);
            var newLeftPart = boxLeft.Get(direction);
            var newRightPart = boxRight.Get(direction);
            _boxesX2.Add(newLeftPart, newRightPart);
            _boxesX2.Add(newRightPart, newLeftPart);
        }
        return true;
    }

    private void ProcessInput2()
    {
        // Example input => Initial state
        // #######       => ##############
        // #...#.#       => ##......##..##
        // #.....#       => ##..........##
        // #..OO@#       => ##....[][]@.##
        // #..O..#       => ##....[]....##
        // #.....#       => ##..........##
        // #######       => ##############
        //
        // <vv<<^^<<^^
        var maxX = InputEntries[0].Length * 2 - 1;
        var maxY = 0;
        
        var processMap = true;
        foreach (var (y, line) in InputEntries.Index())
        {
            if (line == string.Empty)
            {
                maxY = y - 1;
                processMap = false;
                continue;
            }

            foreach (var (x, c) in line.Index())
            {
                if (processMap)
                {
                    switch (c)
                    {
                        case 'O':
                            var boxPart1 = new Point(x * 2, y);
                            var boxPart2 = new Point(x * 2 + 1, y);
                            _boxesX2.Add(boxPart1, boxPart2);
                            _boxesX2.Add(boxPart2, boxPart1);
                            break;
                        case '#':
                            _walls.Add(new Point(x * 2, y));
                            _walls.Add(new Point(x * 2 + 1, y));
                            break;
                        case '@':
                            _robot = new Point(x * 2, y);
                            break;
                    }
                }
                else // Processing directions
                {
                    // <^^>>>vv<v>>v<<
                    var d = c switch
                    {
                        '^' => Direction.N,
                        'v' => Direction.S,
                        '>' => Direction.E,
                        '<' => Direction.W,
                          _ => throw new ArgumentOutOfRangeException($"Invalid direction value '{c}'")
                    };
                    _moves.Add(d);
                }
            }
        }
        
        _boundary = new Boundary { MinX = 0, MinY = 0, MaxX = maxX, MaxY = maxY };
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
