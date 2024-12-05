namespace AdventOfCode.Puzzles;

public class Puzzle05 : Puzzle<string, long>
{
    private const int PuzzleId = 05;

    public Puzzle05() : base(PuzzleId) { }

    public Puzzle05(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    private Dictionary<int, List<int>> PageOrderFirst { get; } = [];
    private Dictionary<int, List<int>> PageOrderSecond { get; } = [];
    private List<List<int>> PageNumbersInCorrectOrder { get; } = [];
    private List<List<int>> PageNumbersInWrongOrder { get; } = [];

    public override long SolvePart1()
    {
        ProcessInput();
        var middleSum = 0L;

        foreach (var pageNumbers in PageNumbersInCorrectOrder)
        {
            var middle = pageNumbers[pageNumbers.Count / 2];
            middleSum += middle;
        }

        return middleSum;
    }

    private class MyComparer(Puzzle05 _puzzle) : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x == y)
            {
                return 0;
            }

            if (!_puzzle.PageOrderFirst.TryGetValue(x, out var numbersAfter))
            {
                numbersAfter = [];
            }
            if (numbersAfter.Contains(y))
            {
                return -1;
            }

            if (!_puzzle.PageOrderSecond.TryGetValue(x, out var numbersBefore))
            {
                numbersBefore = [];
            }
            if (numbersBefore.Contains(y))
            {
                return 1;
            }

            return 0;
        }
    }

    public override long SolvePart2()
    {
        ProcessInput();
        var pageNumbersInFixedOrder = new List<List<int>>();
        var middleSum = 0L;

        Func<int, int> customKeySelector = x => x;
        var customComparer = new MyComparer(this);

        foreach (var pageNumbers in PageNumbersInWrongOrder)
        {
            var fixedOrder = pageNumbers.OrderBy(customKeySelector, customComparer).ToList();
            pageNumbersInFixedOrder.Add(fixedOrder);
        }

        foreach (var pageNumbers in pageNumbersInFixedOrder)
        {
            var middle = pageNumbers[pageNumbers.Count / 2];
            middleSum += middle;
        }

        return middleSum;
    }

    private void ProcessInput()
    {
        var pageNumberItems = new List<List<int>>();
        
        var isPageOrder = true;
        foreach (var item in InputEntries)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                isPageOrder = false;
                continue;
            }

            if (isPageOrder)
            {
                // Process all page ordering rules, putting the result into two
                // dictionaries, allowing fast lookup of both the first and the
                // second item.
                var pageOrder = item.Split('|').Select(int.Parse).ToArray();
                if (PageOrderFirst.TryGetValue(pageOrder[0], out var numbersAfter))
                {
                    numbersAfter.Add(pageOrder[1]);
                }
                else
                {
                    numbersAfter = [pageOrder[1]];
                    PageOrderFirst.Add(pageOrder[0], numbersAfter);
                }
                if (PageOrderSecond.TryGetValue(pageOrder[1], out var numbersBefore))
                {
                    numbersBefore.Add(pageOrder[0]);
                }
                else
                {
                    numbersBefore = [pageOrder[0]];
                    PageOrderSecond.Add(pageOrder[1], numbersBefore);
                }
            }
            else
            {
                var pageNumbers = item.Split(',').Select(int.Parse).ToList();
                pageNumberItems.Add(pageNumbers);
            }
        }

        // Determine which page numbers are in correct order and wrong order
        foreach (var pageNumbers in pageNumberItems)
        {
            var isCorrect = true;
            foreach ((var index, var pageNumber) in pageNumbers.Index())
            {
                if (!PageOrderFirst.TryGetValue(pageNumber, out var numbersAfter))
                {
                    numbersAfter = [];
                }
                if (!PageOrderSecond.TryGetValue(pageNumber, out var numbersBefore))
                {
                    numbersBefore = [];
                }
                if (pageNumbers[..index].Any(p => numbersAfter.Any(n => n == p)))
                {
                    isCorrect = false;
                    break;
                }
                if (pageNumbers[(index + 1)..].Any(p => numbersBefore.Any(n => n == p)))
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                PageNumbersInCorrectOrder.Add(pageNumbers);
            }
            else
            {
                PageNumbersInWrongOrder.Add(pageNumbers);
            }
        }
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
