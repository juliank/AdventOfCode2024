namespace AdventOfCode.Puzzles;

public class Puzzle14 : Puzzle<(Point P, Point V), long>
{
    private const int PuzzleId = 14;

    public Puzzle14() : base(PuzzleId)
    {
        // a space which is 101 tiles wide and 103 tiles tall (when viewed from above)
        _boundary = new Boundary { MinX = 0, MinY = 0, MaxX = 100, MaxY = 102 };
    }

    public Puzzle14(int maxX, int maxY, params IEnumerable<(Point P, Point V)> inputEntries) : base(PuzzleId, inputEntries)
    {
        // a space which is 101 tiles wide and 103 tiles tall (when viewed from above)
        _boundary = new Boundary { MinX = 0, MinY = 0, MaxX = maxX, MaxY = maxY };
    }

    private readonly Boundary _boundary;

    public override long SolvePart1()
    {
        MoveRobots();

        // Calculate quadrant counts:
        long q1 = 0, q2 = 0, q3 = 0, q4 = 0;
        var middleX = _boundary.MaxX / 2;
        var middleY = _boundary.MaxY / 2;

        foreach (var robot in InputEntries)
        {
            if (robot.P.X < middleX) // q1 or q3
            {
                if (robot.P.Y < middleY)
                {
                    q1++;
                }
                else if (robot.P.Y > middleY)
                {
                    q3++;
                }
            }
            else if (robot.P.X > middleX) // q2 or q4
            {
                if (robot.P.Y < middleY)
                {
                    q2++;
                }
                else if (robot.P.Y > middleY)
                {
                    q4++;
                }
            }
        }

        var safetyFactor = q1 * q2 * q3 * q4;

        // 217337280 is too low
        // 224969976
        return safetyFactor;
    }

    private void MoveRobots()
    {
        for (int i = 0; i < 100; i++)
        {
            for (int r = 0; r < InputEntries.Count; r++)
            {
                (var p, var v) = InputEntries[r];
                var newP = new Point(p.X + v.X, p.Y + v.Y);
                if (newP.X < _boundary.MinX)
                {
                    newP = newP with { X = newP.X + _boundary.MaxX!.Value + 1 };
                }
                if (newP.X > _boundary.MaxX)
                {
                    newP = newP with { X = newP.X - _boundary.MaxX!.Value - 1 };
                }
                if (newP.Y < _boundary.MinY)
                {
                    newP = newP with { Y = newP.Y + _boundary.MaxY!.Value + 1 };
                }
                if (newP.Y > _boundary.MaxY)
                {
                    newP = newP with { Y = newP.Y - _boundary.MaxY!.Value - 1 };
                }
                InputEntries[r] = (newP, v);
            }
        }
    }

    public override long SolvePart2()
    {
        throw new NotImplementedException();
    }

    protected internal override (Point P, Point V) ParseInput(string inputItem)
    {
        // p=0,4 v=3,-3
        var items = inputItem.Split(' ');
        var p = items[0].Split('=')[1].Split(',');
        var v = items[1].Split('=')[1].Split(',');
        var point = new Point(int.Parse(p[0]), int.Parse(p[1]));
        var velocity = new Point(int.Parse(v[0]), int.Parse(v[1]));
        return (point, velocity);
    }
}
