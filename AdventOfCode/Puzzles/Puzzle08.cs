namespace AdventOfCode.Puzzles;

public class Puzzle08 : Puzzle<string, long>
{
    private const int PuzzleId = 08;

    public Puzzle08() : base(PuzzleId) { }

    public Puzzle08(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    // key: antenna frequency, value: positions
    private readonly Dictionary<char, List<Point>> _antennas = [];
    private readonly List<(Point, Point)> _antennaPairs = [];
    private HashSet<Point> _antinodes = default!;
    private int _maxX = -1;
    private int _maxY = -1;

    public override long SolvePart1()
    {
        _antinodes = [];

        ProcessInput();
        ProcessAntinodesPart1();

        return _antinodes.Distinct().Count();
    }

    public override long SolvePart2()
    {
        _antinodes = [];

        ProcessInput();
        ProcessAntinodesPart2();

        return _antinodes.Distinct().Count();
    }

    private void ProcessAntinodesPart1()
    {
        // E.g. (1, 3) and (2, 2)
        foreach (var (a, b) in _antennaPairs)
        {
            var dX = b.X - a.X; // 2 - 1 = 1
            var dY = b.Y - a.Y; // 2 - 3 = -1

            var antinodeA = new Point(a.X - dX, a.Y - dY);
            var antinodeB = new Point(b.X + dX, b.Y + dY);

            if (antinodeA.X >= 0 && antinodeA.X <= _maxX &&
                antinodeA.Y >= 0 && antinodeA.Y <= _maxY)
            {
                _antinodes.Add(antinodeA);
            }
            if (antinodeB.X >= 0 && antinodeB.X <= _maxX &&
                antinodeB.Y >= 0 && antinodeB.Y <= _maxY)
            {
                _antinodes.Add(antinodeB);
            }
        }
    }

    private void ProcessAntinodesPart2()
    {
        // E.g. (1, 3) and (2, 2)
        foreach (var (a, b) in _antennaPairs)
        {
            var dX = b.X - a.X; // 2 - 1 = 1
            var dY = b.Y - a.Y; // 2 - 3 = -1

            var slope = (double)dY/dX;
            for (int x = 0; x <= _maxX; x++)
            {
                var y = slope * (x - a.X) + a.Y;
                if (y >= 0 && y <= _maxY && y == Math.Floor(y))
                {
                    var point = new Point(x, (int)y);
                    _antinodes.Add(point);
                }
            }
        }
    }

    private void ProcessInput()
    {
        if (_antennas.Count > 0)
        {
            // The input has already been processed
            return;
        }

        /// InputEntries[y][x] (rows,columns)
        _maxX = InputEntries[0].Length - 1;
        _maxY = InputEntries.Count - 1;

        ProcessAntennas();
        ProcessAntennaPairs();
    }

    private void ProcessAntennas()
    {
        for (var y = 0; y <= _maxY; y++)
        {
            for (var x = 0; x <= _maxX; x++)
            {
                var item = InputEntries[y][x];
                if (item == '.' || item == '#')
                {
                    // To make it easier to test on the various examples,
                    // we also ignore #, which is used to mark the antinode
                    // positions. # doesn't seem to appear in the puzzle input,
                    // so we can safely ignore it here.
                    continue;
                }
                if (!_antennas.TryGetValue(item, out var positions))
                {
                    positions = [];
                    _antennas.Add(item, positions);
                }
                positions.Add(new Point(x, y));
            }
        }
    }

    private void ProcessAntennaPairs()
    {
        foreach (var (_, antennaPositions) in _antennas)
        {
            for (var i = 0; i < antennaPositions.Count - 1; i++)
            {
                // For each antenna A, we loop through the remaining antennas (B)
                // to create all possible antenna pairs for the given frequency.
                var a = antennaPositions[i];
                foreach (var b in antennaPositions[(i + 1)..])
                {
                    _antennaPairs.Add((a, b));
                }
            }
        }
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
