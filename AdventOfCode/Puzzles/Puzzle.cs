[assembly: InternalsVisibleTo("AdventOfCode.Tests")]
namespace AdventOfCode.Puzzles;

/// <summary>
/// Base class for puzzle implementations, offering generically typed versions
/// of the different solve methods.
/// </summary>
/// <typeparam name="TInput">The type each line of the puzzle input will be parsed as.</typeparam>
/// <typeparam name="TResult">The type of the result from solving the puzzle, typically an int or a long.</typeparam>
public abstract class Puzzle<TInput, TResult>(int id) : IPuzzle
    where TInput : notnull
    where TResult : notnull
{
    public int Id { get; } = id;

    private List<TInput>? _inputEntries;

    /// <summary>
    /// The list of parsed entries for the puzzle input, one item per line in the input.
    /// </summary>
    /// <remarks>The input file is loaded and parsed the first time the property is read.</remarks>
    public List<TInput> InputEntries
    {
        get
        {
            _inputEntries ??= LoadInput();
            return _inputEntries;
        }
        protected set
        {
            _inputEntries = value;
        }
    }

    /// <summary>
    /// Alternate ctor to allow creating the puzzle with a hardcode set of entries (for easier testing).
    /// </summary>
    protected Puzzle(int id, params List<TInput> inputEntries) : this(id)
    {
        _inputEntries = inputEntries;
    }

    object IPuzzle.Solve() => Solve();

    public virtual bool SkipPart1WhenSolveAll => false;

    object IPuzzle.SolvePart1() => SolvePart1();

    public virtual bool SkipPart2WhenSolveAll => false;

    object IPuzzle.SolvePart2() => SolvePart2();

    /// <summary>
    /// Will first try and solve part 2 of the puzzle. If part 2 isn't implemented,
    /// part 1 will be solved instead.
    /// </summary>
    public TResult Solve()
    {
        TResult result;

        Stopwatch sw;
        try
        {
            sw = Stopwatch.StartNew();
            result = SolvePart2();
        }
        catch (NotImplementedException)
        {
            Console.WriteLine("Solution to part 2 is not implemented yet, solving part 1 instead");
            sw = Stopwatch.StartNew();
            result = SolvePart1();
        }

        sw.Stop();
        var elapsed = sw.Elapsed < TimeSpan.FromSeconds(1) ? $"{sw.ElapsedMilliseconds} ms" : $"{sw.Elapsed}";
        Console.WriteLine($"Solution time: {elapsed}");

        return result;
    }

    /// <summary>
    /// Implement this method to solve part 1 of the puzzle.
    /// </summary>
    public abstract TResult SolvePart1();

    /// <summary>
    /// Implement this method to solve part 2 of the puzzle.
    /// </summary>
    public abstract TResult SolvePart2();

    /// <summary>
    /// Implement this method to parse the puzzle's input. All the returned
    /// elements from the method will be added to the puzzle's <see cref="InputEntries"/>,
    /// but the most typical implementation will only return a single item with:
    /// <code>yield return</code>
    /// </summary>
    /// <param name="inputItem">One line from the puzzle input.</param>
    /// <returns>One (or more) elements parsed from the input line.</returns>
    protected internal abstract IEnumerable<TInput> ParseInput(string inputItem);

    private List<TInput> LoadInput()
    {
        var fileName = $"{Id:D2}.txt";
        var path = FileHelper.GetInputFilePath(fileName);

        var entries = new List<TInput>();
        foreach (var line in FileHelper.ReadFile(path))
        {
            entries.AddRange(ParseInput(line));
        }

        return entries;
    }
}
