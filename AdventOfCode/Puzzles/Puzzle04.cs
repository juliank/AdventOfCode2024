namespace AdventOfCode.Puzzles;

public class Puzzle04 : Puzzle<string, long>
{
    private const int PuzzleId = 04;

    public Puzzle04() : base(PuzzleId) { }

    public Puzzle04(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    /// <summary>
    /// WordSearch[y][x] (rows,columns)
    /// </summary>
    private string[] WordSearch {
        get
        {
            if (field.Length == 0)
            {
                field = InputEntries.ToArray();
            }
            return field;
        }
    } = [];

    public override long SolvePart1()
    {
        var rows = WordSearch.Length;
        var columns = WordSearch[0].Length;

        var hitCount = 0;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                var p = new BoundedPoint(new Point(x, y))
                {
                    MinX = 0, MaxX = columns - 1,
                    MinY = 0, MaxY = rows - 1
                };
                var hitsAtPoint = HasWord(p, "XMAS", Directions.D2Extended);
                hitCount += hitsAtPoint.Count;
            }
        }

        return hitCount;
    }

    public override long SolvePart2()
    {
        var rows = WordSearch.Length;
        var columns = WordSearch[0].Length;

        var aPoints = new List<Point>();

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                var p = new BoundedPoint(new Point(x, y))
                {
                    MinX = 0, MaxX = columns - 1,
                    MinY = 0, MaxY = rows - 1
                };
                var hitsAtPoint = HasWord(p, "MAS", Direction.NE, Direction.SE, Direction.SW, Direction.NW);
                foreach (var direction in hitsAtPoint)
                {
                    aPoints.Add(p.Point.Get(direction)); // Add the point of the A letter to the list
                }
            }
        }

        var xPoints = aPoints.GroupBy(p => p).Where(g => g.Count() > 1);
        var xCount = xPoints.Count();

        return xCount;
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }

    private List<Direction> HasWord(BoundedPoint bp, string word, params Direction[] inDirections)
    {
        if (WordSearch[bp.Point.Y][bp.Point.X] != word[0])
        {
            return [];
        }

        var directionsWithWord = new List<Direction>();
        inDirections ??= Directions.D2Extended;
        foreach (var direction in inDirections)
        {
            if (bp.TryGetDirection(direction, out var nextPoint))
            {
                var nextBp = bp with { Point = nextPoint };
                if (HasWord(direction, nextBp, word[1..]))
                {
                    directionsWithWord.Add(direction);
                }
            }
        }

        return directionsWithWord;
    }

    private bool HasWord(Direction direction, BoundedPoint bp, string word)
    {
        if (WordSearch[bp.Point.Y][bp.Point.X] != word[0])
        {
            return false;
        }

        if (word.Length == 1)
        {
            return true;
        }

        if (!bp.TryGetDirection(direction, out var nextPoint))
        {
            return false;
        }

        var nextBp = bp with { Point = nextPoint };
        return HasWord(direction, nextBp, word[1..]);
    }
}
