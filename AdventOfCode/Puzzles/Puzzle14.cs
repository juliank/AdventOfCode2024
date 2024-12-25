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

    // Various fields used for debugging part 2
    private bool _printMap;
    private bool _haltPrint;
    private int _printStart;
    private int _printInterval = 1;

    public override long SolvePart1()
    {
        MoveRobots();

        var q = CountQuadrants();
        var safetyFactor = q.Q1 * q.Q2 * q.Q3 * q.Q4;
        return safetyFactor;
    }

    public override long SolvePart2()
    {
        ThrowHardCodedResult(7892, "Must debug visually to find correct answer");
        
        _printMap = true;
        _haltPrint = true;
        _printStart = 7890;

        _printInterval = 5_000;
        MoveRobots(10_000);

        // Found after several rounds of trail and error.
        // - Hints from Reddit indicated a maximum number of iterations needed being 10k
        // - One viable strategy suggested looking at the ratio of robots in the middle of the map
        //   to decide which map picture to print.
        return 7892;
    }

    private (long Q1, long Q2, long Q3, long Q4) CountQuadrants()
    {
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

        return (q1, q2, q3, q4);
    }

    private void MoveRobots(int moves = 100)
    {
        for (int s = 1; s <= moves; s++)
        {
            for (int r = 0; r < InputEntries.Count; r++)
            {
                var (p, v) = InputEntries[r];
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

            if (_printMap && s > _printStart)
            {
                if (s % _printInterval == 0)
                {
                    Console.WriteLine($"{s} seconds...");
                }
                if (!ShouldPrint())
                {
                    continue;
                }
                Console.WriteLine($"Positions after {s} seconds");
                // PrintMap(_boundary, InputEntries.Select(ie => ie.P).ToList());
                Helper.PrintMap(_boundary, InputEntries.Select(ie => ie.P), printBorder: true);
                Console.WriteLine($"Positions after {s} seconds\n");
                if (_haltPrint)
                {
                    _ = Console.ReadKey();
                }
            }
        }
    }

    // Based on if a certain amount of the robots are in the middle of the map
    private bool ShouldPrint()
    {
        var totalRobots = InputEntries.Count;
        var halfOfTotal = totalRobots / 2;
        
        var middleX = _boundary.MaxX / 2;
        var lowerX = middleX - 15;
        var upperX = middleX + 15;
        
        var robotsInMiddle = InputEntries.Count(ie => ie.P.X >= lowerX && ie.P.X <= upperX);
        return robotsInMiddle > halfOfTotal;
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
    
    /*
    Positions after 7892 seconds
    =========================================================================================================
    |                         XX                                                            X             X |
    |                        XX                                                                             |
    |                                    X                       X      X            X                      |
    |                                                                                       X               |
    |                                                                                                       |
    |   X                                                             X                                     |
    |                                X                                                                      |
    |    X                                          X                                                       |
    | X         X                                          X                                         X      |
    |      X                                      X         X                                               |
    |      X                                              X                                                 |
    |  X                               X                                                                    |
    |                                                                                                       |
    |                                                                    X                                  |
    |               X                X                                                                      |
    |             X                            X                                                  X         |
    |       X                             X                                       X                         |
    |                                                              X             X                          |
    |                                                                X                                      |
    |                                             X                                                         |
    |     X                                                     X                         X                 |
    |                                                                                     X                 |
    |            X                                   X                                                      |
    |                                               X             X                                         |
    |                                                                X                                      |
    |                                                                                                 X     |
    |                      X XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                                                |
    |                        X                             X                                     X          |
    |                        X                             X                     X           X              |
    |                        X                             X                                                |
    |                        X                             X                                                |
    | X                      X              X              X                                                |
    |                        X             XXX             X            X                                   |
    |                        X            XXXXX            X                                                |
    |                        X           XXXXXXX           X  X                                         X   |
    |             X          X          XXXXXXXXX          X                                  X             |
    |                        X            XXXXX            X           X                X   X               |
    |      X                 X           XXXXXXX           X                                                |
    |     X                  X          XXXXXXXXX          X                                                |
    |                X       X         XXXXXXXXXXX         X                  X                             |
    |                        X        XXXXXXXXXXXXX        X                           X             X      |
    |               X        X          XXXXXXXXX          X     X            X                             |
    |                        X         XXXXXXXXXXX         X                              X                 |
    |                        X        XXXXXXXXXXXXX        X                                                |
    |     X       X          X       XXXXXXXXXXXXXXX       X                                                |
    |                        X      XXXXXXXXXXXXXXXXX      X       X                                        |
    |       X                X        XXXXXXXXXXXXX        X                                                |
    |                        X       XXXXXXXXXXXXXXX       X     X                                          |
    |                        X      XXXXXXXXXXXXXXXXX      X                                    X           |
    |                        X     XXXXXXXXXXXXXXXXXXX     X                                         X      |
    |                        X    XXXXXXXXXXXXXXXXXXXXX    X                        X               X       |
    |                        X             XXX             X                                                |
    |                        X             XXX             X                                                |
    |                        X             XXX             X                          X       X             |
    |                        X                             X                     X      X                   |
    |                        X                             X                                                |
    |               X   X    X                             X                                                |
    |                        X                             X      X                    X                    |
    |                        XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                                                |
    |                                                                                                       |
    |                                                                                             X         |
    |   X                    X                                                                              |
    |   X                    X              X                                                               |
    |                                                                                                       |
    |                                                                                                       |
    |                              X                                                                        |
    |                    X                                                             X                    |
    |                                                          X                                            |
    |                   X                     X                                       X                     |
    |                                                                                                X      |
    |                                                                                     X                 |
    |                                           X                                                           |
    |           X                       X                                                    X              |
    |                             X                                                X                        |
    |             X                                                                                         |
    |                                                                        X                          X   |
    |                                                                                                       |
    |                                                                                                    X  |
    |     X                                                                                                 |
    |                                X      X               X                                               |
    |                          X                                                                            |
    |                                                                              X                        |
    |                                                                                                       |
    |                                                                                                       |
    |                                  X         X                                                          |
    |                                                                                                 X     |
    |                             X      X                                                                  |
    |                                                                                                       |
    |                      X                                            X                                   |
    |                    X           X                                                                      |
    |                                                     X                                                 |
    |    X                                                                                                  |
    |         X                   X               X                                                         |
    |                                                                               X                       |
    |   X                                                                                       X           |
    |                X                                X                                         X           |
    |                                              X        X                                               |
    |                                                                                                       |
    |                                                                                                       |
    |                                                                   X                                   |
    |                                                                       X   X                           |
    |                                                                                         X             |
    |                                                                                                       |
    =========================================================================================================
    Positions after 7892 seconds
    */
}
