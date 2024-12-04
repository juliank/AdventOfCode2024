namespace AdventOfCode.Puzzles;

public class Puzzle04 : Puzzle<char[], long>
{
    private const int PuzzleId = 04;

    public Puzzle04() : base(PuzzleId) { }

    public Puzzle04(params IEnumerable<char[]> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var wordSearch = InputEntries.ToArray();
        // wordSearch[y][x] (rows,columns)
        var rows = wordSearch.Length;
        var columns = wordSearch[0].Length;

        var hitCount = 0;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (wordSearch[y][x] != 'X')
                {
                    continue;
                }
                
                var p = new BoundedPoint(new Point(x, y))
                {
                    MinX = 0, MaxX = columns - 1,
                    MinY = 0, MaxY = rows - 1
                };
                // var nextP = nextBP.Point;
                foreach (var direction in Directions.D2Extended)
                {
                    var nextBp = p;
                    if (!nextBp.TryGetDirection(direction, out var nextP))
                    {
                        continue;
                    }
                    if (wordSearch[nextP.Y][nextP.X] != 'M')
                    {
                        continue;
                    }
                    nextBp = nextBp with { Point = nextP };

                    if (!nextBp.TryGetDirection(direction, out nextP))
                    {
                        continue;
                    }
                    if (wordSearch[nextP.Y][nextP.X] != 'A')
                    {
                        continue;
                    }
                    nextBp = nextBp with { Point = nextP };

                    if (!nextBp.TryGetDirection(direction, out nextP))
                    {
                        continue;
                    }
                    if (wordSearch[nextP.Y][nextP.X] == 'S')
                    {
                        hitCount++;
                    }
                }
            }
        }

        return hitCount;
    }

    public override long SolvePart2()
    {
        var wordSearch = InputEntries.ToArray();
        // wordSearch[y][x] (rows,columns)
        var rows = wordSearch.Length;
        var columns = wordSearch[0].Length;

        var xDirections = new[] { Direction.NE, Direction.SE, Direction.SW, Direction.NW };
        var aPoints = new List<Point>();

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                if (wordSearch[y][x] != 'M')
                {
                    continue;
                }
                
                var p = new BoundedPoint(new Point(x, y))
                {
                    MinX = 0, MaxX = columns - 1,
                    MinY = 0, MaxY = rows - 1
                };
                
                foreach (var direction in xDirections)
                {
                    var nextBp = p;
                    if (!nextBp.TryGetDirection(direction, out var nextP))
                    {
                        continue;
                    }
                    if (wordSearch[nextP.Y][nextP.X] != 'A')
                    {
                        continue;
                    }

                    var aPoint = nextP;
                    nextBp = nextBp with { Point = nextP };

                    if (!nextBp.TryGetDirection(direction, out nextP))
                    {
                        continue;
                    }
                    if (wordSearch[nextP.Y][nextP.X] == 'S')
                    {
                        aPoints.Add(aPoint);
                    }
                }
            }
        }

        var xPoints = aPoints.GroupBy(p => p).Where(g => g.Count() > 1);
        var xCount = xPoints.Count();

        return xCount;
    }

    protected internal override char[] ParseInput(string inputItem)
    {
        return inputItem.ToCharArray();
    }
}
