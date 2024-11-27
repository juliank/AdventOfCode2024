using static System.Console;

var assembly = Assembly.GetAssembly(typeof(IPuzzle))!;
var puzzleTypes = assembly.GetTypes()
    .Where(t => t.IsAssignableTo(typeof(IPuzzle)) && !t.IsInterface && !t.IsAbstract)
    .Select(t =>
    {
        var puzzleId = int.Parse(t.Name[^2..]);
        return (Id: puzzleId, Type: t);
    })
    .OrderBy(p => p.Id)
    .Skip(1) // To skip the "dummy" 00 puzzle
    .ToList();
var lastPuzzleId = puzzleTypes.Last().Id;

WriteLine();
if (args.Length == 0)
{
    WriteLine($"No puzzle id specified, using latest {lastPuzzleId}");
    args = new[] { lastPuzzleId.ToString() };
}
if (args[0].Equals("all", StringComparison.InvariantCultureIgnoreCase))
{
    SolveAllPuzzles();
    return;
}

if (!int.TryParse(args[0], out var puzzleId))
{
    throw new Exception($"[{args[0]}] is not a valid int");
}

SolvePuzzle(puzzleId);

void SolvePuzzle(int pid)
{
    var puzzleType = puzzleTypes.SingleOrDefault(p => p.Id == pid).Type;
    if (puzzleType == null)
    {
        throw new Exception($"Unknown puzzle {pid}");
    }
    var puzzle = (IPuzzle)Activator.CreateInstance(puzzleType)!;

    WriteLine($"Preparing to solve puzzle {pid}...");
    WriteLine();

    var result = puzzle.Solve();
    WriteLine($"Result is: [{result}]");
    WriteLine();
}

void SolveAllPuzzles()
{
    Stopwatch sw;
    foreach (var puzzleType in puzzleTypes)
    {
        WriteLine($"Solving puzzle {puzzleType.Id}...");

        try
        {
            var puzzle = (IPuzzle)Activator.CreateInstance(puzzleType.Type)!;
            if (puzzle.SkipPart1WhenSolveAll)
            {
                WriteLine($"- part 1: SKIPPED");
            }
            else
            {
                sw = Stopwatch.StartNew();
                var solutionPart1 = puzzle.SolvePart1();
                sw.Stop();
                var elapsed = sw.Elapsed < TimeSpan.FromSeconds(1) ? $"{sw.ElapsedMilliseconds} ms" : $"{sw.Elapsed}";
                WriteLine($"- part 1: [{solutionPart1}] (time: {elapsed})");
            }
        }
        catch (NotImplementedException)
        {
            WriteLine($"- part 1: NOT YET IMPLEMENTED");
        }

        try
        {
            // Re-create the puzzle instance, just to be sure we don't drag along shared state
            var puzzle = (IPuzzle)Activator.CreateInstance(puzzleType.Type)!;
            if (puzzle.SkipPart2WhenSolveAll)
            {
                WriteLine($"- part 2: SKIPPED");
            }
            else
            {
                sw = Stopwatch.StartNew();
                var solutionPart2 = puzzle.SolvePart2();
                sw.Stop();
                var elapsed = sw.Elapsed < TimeSpan.FromSeconds(1) ? $"{sw.ElapsedMilliseconds} ms" : $"{sw.Elapsed}";
                WriteLine($"- part 2: [{solutionPart2}] (time: {elapsed})");
            }
        }
        catch (NotImplementedException)
        {
            WriteLine($"- part 2: NOT YET IMPLEMENTED");
        }
        WriteLine();
    }
}
