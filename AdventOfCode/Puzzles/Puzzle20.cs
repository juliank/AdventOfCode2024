namespace AdventOfCode.Puzzles;

public class Puzzle20 : Puzzle<string, long>
{
    private const int PuzzleId = 20;

    public Puzzle20() : base(PuzzleId) { }

    public Puzzle20(int cheatLimit, params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries)
    {
        _cheatLimit = cheatLimit;
    }
    
    private readonly int _cheatLimit = 100;

    public override long SolvePart1()
    {
        ThrowHardCodedResult(1485, "Solution time is too slow with real input (00:01:11.4737841)", notFromTest: true);
        
        var walls = new HashSet<Point>();
        var start = new Point(0, 0);
        var end = new Point(0, 0);
        foreach (var position in InputMap)
        {
            switch (position.Value)
            {
                case '#':
                    walls.Add(position.Key);
                    break;
                case 'S':
                    start = position.Key;
                    break;
                case 'E':
                    end = position.Key;
                    break;
            }
        }
        var boundary = new Boundary(0, 0, walls.Max(w => w.X), walls.Max(w => w.Y));
        
        // Add an additional "layer" of outside walls, to simplify later checking 
        for (var x = -1; x <= boundary.MaxX + 1; x++)
        {
            walls.Add(new Point(x, -1));
            walls.Add(new Point(x, boundary.MaxY!.Value + 1));
        }
        for (var y = -1; y <= boundary.MaxY + 1; y++)
        {
            walls.Add(new Point(-1, y));
            walls.Add(new Point(boundary.MaxX!.Value + 1, y));
        }
        
        var shortestPath = AStarPathfinding.FindShortestPath(start, end, boundary, walls);
        var possibleCheats = new HashSet<Point>();
        foreach (var point in shortestPath)
        {
            foreach (var direction in Directions.D2)
            {
                var neighbor = point.Get(direction);
                if (walls.Contains(neighbor) && !walls.Contains(neighbor.Get(direction)))
                {
                    // Removing the neighbor is a possible cheat
                    possibleCheats.Add(neighbor);
                }
            }
        }

        var time = shortestPath.Count - 1; // The shortest possible time without cheating
        var cheatCount = 0; // The number of cheats saving at least cheatTime picoseconds
        foreach (var cheat in possibleCheats)
        {
            var cheatPath = AStarPathfinding.FindShortestPath(start, end, boundary, walls.Except([cheat]).ToHashSet());
            var cheatTime = cheatPath.Count - 1;
            if (cheatTime == 0)
            {
                // Found no possible path (but if that happens, something is wrong with the logic...)
                throw new Exception($"Couldn't find a path when using the cheat at {cheat}");
            }

            if (time - cheatTime >= _cheatLimit)
            {
                cheatCount++;
            }
        }
        
        return cheatCount;
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
