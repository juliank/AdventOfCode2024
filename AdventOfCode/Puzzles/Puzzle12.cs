namespace AdventOfCode.Puzzles;

public class Puzzle12 : Puzzle<string, long>
{
    private const int PuzzleId = 12;

    public Puzzle12() : base(PuzzleId) { }

    public Puzzle12(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    private readonly Dictionary<Point, char> _map = [];

    // Only the Point is needed for the key, but we include the char for easier debugging
    private readonly Dictionary<(char, Point), HashSet<Point>> _gardenPlots = [];

    public override long SolvePart1()
    {
        ProcessInput();
        CreateGardenPlots();

        var totalFencingPrice = 0L;
        foreach (var (_, gardenPlot) in _gardenPlots)
        {
            var area = gardenPlot.Count;
            var perimeter = CalculatePerimeter(gardenPlot);
            var fencingPrice = area * perimeter;
            totalFencingPrice += fencingPrice;
        }

        return totalFencingPrice;
    }

    public override long SolvePart2()
    {
        ProcessInput();
        CreateGardenPlots();

        var totalFencingPrice = 0L;
        foreach (var (_, gardenPlot) in _gardenPlots)
        {
            var area = gardenPlot.Count;
            var perimeter = CalculateAlternateDiscountedPerimeter(gardenPlot);
            var fencingPrice = area * perimeter;
            totalFencingPrice += fencingPrice;
        }

        return totalFencingPrice;
    }

    private static int CalculateAlternateDiscountedPerimeter(HashSet<Point> gardenPlot)
    {
        // Create set of all points that are part of the perimeter
        var perimeterPoints = new HashSet<Point>();
        foreach (var point in gardenPlot)
        {
            foreach (var perimeterDirection in Directions.D2)
            {
                var neighbor = point.Get(perimeterDirection);
                if (gardenPlot.Contains(neighbor))
                {
                    continue;
                }
                // The neighbor is not in the garden plot => it is a "perimeter point"
                perimeterPoints.Add(neighbor);
            }
        }

        var perimeterSectionCount = 0;
        foreach (var forward in Directions.D2)
        {
            var processedPerimeterPoints = new HashSet<(Point, Direction)>();
            foreach (var point in perimeterPoints)
            {
                if (processedPerimeterPoints.Contains((point, forward)))
                {
                    continue;
                }

                // Picture we're standing at a perimeter point, facing in forward direction.
                var right = forward.Rotate(90);
                var backward = forward.Rotate(180);
                
                var perimeterSection = 0;
                var perimeterCandidate = point;
                var candidateNeighbor = perimeterCandidate.Get(right);

                // If the candidates right neighbor is in the garden plot, the candidate *is* a perimeter for said neighbor.
                // Continue for every perimeter candidate in the forward direction. If they *also* have a right neighbor
                // which is in the garden plot, that point is *also* a perimeter point in the same (forward) direction.
                while (gardenPlot.Contains(candidateNeighbor))
                {
                    processedPerimeterPoints.Add((perimeterCandidate, forward));
                    perimeterSection++;

                    perimeterCandidate = perimeterCandidate.Get(forward);
                    if (gardenPlot.Contains(perimeterCandidate))
                    {
                        break;
                    }
                    candidateNeighbor = perimeterCandidate.Get(right);
                }
                processedPerimeterPoints.Add((perimeterCandidate, forward));
                
                perimeterCandidate = point.Get(backward);
                if (!gardenPlot.Contains(perimeterCandidate))
                {
                    // Repeat the same check as above, only in the backward direction (still checking right-side neighbors).
                    candidateNeighbor = perimeterCandidate.Get(right);
                    while (gardenPlot.Contains(candidateNeighbor))
                    {
                        processedPerimeterPoints.Add((perimeterCandidate, forward));
                        perimeterSection++;

                        perimeterCandidate = perimeterCandidate.Get(backward);
                        if (gardenPlot.Contains(perimeterCandidate))
                        {
                            break;
                        }
                        candidateNeighbor = perimeterCandidate.Get(right);
                    }
                    processedPerimeterPoints.Add((perimeterCandidate, forward));
                }

                if (perimeterSection > 0)
                {
                    // We have at least *one* perimeter point in *this* direction (for the right-side neighbor garde points)
                    perimeterSectionCount++;
                }
            }
        }

        return perimeterSectionCount;
    }

    /// <summary>
    /// This was the initial attempt for solving part two of today's puzzle.
    /// It worked flawlessly on *all* the available examples in the puzzle text,
    /// but yielded a too low answer (908967, versus the correct 910066).
    /// I've still got no idea what's wrong with this implementation, so I just
    /// started over with an alternative strategy in CalculateAlternateDiscountedPerimeter.
    /// This method is kept for fun and amusement, in case I should find the time
    /// and desire to debug it at a later time :-)
    /// </summary>
    #pragma warning disable IDE0051 // Remove unused private members
    private static int CalculateDiscountedPerimeter(HashSet<Point> gardenPlot)
    #pragma warning restore IDE0051
    {
        // Create set of all points that are part of the perimeter
        var perimeterPoints = new HashSet<Point>();
        foreach (var point in gardenPlot)
        {
            foreach (var perimeterDirection in Directions.D2)
            {
                var neighbor = point.Get(perimeterDirection);
                if (gardenPlot.Contains(neighbor))
                {
                    continue;
                }
                // The neighbor is not in the garden plot => it is a "perimeter point"
                perimeterPoints.Add(neighbor);
            }
        }

        // Key: point/direction identifying the perimeter section (straight fence part)
        // Value: List of points included in the section
        var perimeterSections = new Dictionary<(Point, Direction), HashSet<Point>>();
        var processedPerimeterPoints = new HashSet<(Point, Direction)>();
        foreach (var point in gardenPlot)
        {
            foreach (var perimeterDirection in Directions.D2)
            {
                var neighbor = point.Get(perimeterDirection);
                if (processedPerimeterPoints.Contains((neighbor, perimeterDirection)))
                {
                    continue;
                }
                if (gardenPlot.Contains(neighbor))
                {
                    continue;
                }

                // We're at an *unprocessed* perimeter point (i.e. a *new* perimeter section)
                var perimeterSection = new HashSet<Point> { neighbor };
                perimeterSections.Add((neighbor, perimeterDirection), perimeterSection);

                var neighborDirection = Directions.Rotate(perimeterDirection, 90);
                var perimeterNeighbor = neighbor.Get(neighborDirection);
                while (perimeterPoints.Contains(perimeterNeighbor))
                {
                    processedPerimeterPoints.Add((perimeterNeighbor, perimeterDirection));
                    perimeterSection.Add(perimeterNeighbor);
                    perimeterNeighbor = perimeterNeighbor.Get(neighborDirection);
                }

                neighborDirection = Directions.Rotate(perimeterDirection, 270);
                perimeterNeighbor = neighbor.Get(neighborDirection);
                while (perimeterPoints.Contains(perimeterNeighbor))
                {
                    processedPerimeterPoints.Add((perimeterNeighbor, perimeterDirection));
                    perimeterSection.Add(perimeterNeighbor);
                    perimeterNeighbor = perimeterNeighbor.Get(neighborDirection);
                }
            }
        }

        return perimeterSections.Count;
    }

    private static int CalculatePerimeter(HashSet<Point> gardenPlot)
    {
        var perimeter = 0;
        foreach (var point in gardenPlot)
        {
            foreach (var direction in Directions.D2)
            {
                var neighbor = point.Get(direction);
                if (gardenPlot.Contains(neighbor))
                {
                    continue;
                }
                perimeter++;
            }
        }
        return perimeter;
    }

    private void CreateGardenPlots()
    {
        var processed = new HashSet<Point>();

        foreach (var (point, plant) in _map)
        {
            if (processed.Contains(point))
            {
                continue;
            }

            if (!_gardenPlots.TryGetValue((plant, point), out var gardenPlot))
            {
                gardenPlot = [ point ];
                _gardenPlots[(plant, point)] = gardenPlot; // We identify a garden plot by its first point
                processed.Add(point);
            }

            CreateGardenPlot(point, plant, gardenPlot, processed);
        }
    }

    private void CreateGardenPlot(Point point, char plant, HashSet<Point> gardenPlot, HashSet<Point> processed)
    {
        foreach (var direction in Directions.D2)
        {
            var neighbor = point.Get(direction);
            if (processed.Contains(neighbor))
            {
                continue;
            }

            if (_map.TryGetValue(neighbor, out var neighborPlant) && plant == neighborPlant)
            {
                gardenPlot.Add(neighbor);
                processed.Add(neighbor);
                CreateGardenPlot(neighbor, plant, gardenPlot, processed);
            }
        }
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }

    private void ProcessInput()
    {
        // InputEntries[y][x] (rows,columns)
        var rows = InputEntries.Count;
        var columns = InputEntries[0].Length;

        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < columns; x++)
            {
                var position = new Point(x, y);
                var plant = InputEntries[y][x];
                _map.Add(position, plant);
            }
        }
    }
}
