namespace AdventOfCode.Puzzles;

public class Puzzle07 : Puzzle<string, long>
{
    private const int PuzzleId = 07;

    public Puzzle07() : base(PuzzleId) { }

    public Puzzle07(params IEnumerable<string> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var validValues = new List<long>();

        foreach (var equation in InputEntries)
        {
            var parts = equation.Split(':');
            var answer = long.Parse(parts[0]);
            var values = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => long.Parse(s))
                .ToArray();
            
            var solutions = new List<long> { values[0] };
            var isValid = false;
            for (var i = 1; i < values.Length; i++)
            {
                var temp = new List<long>();
                foreach (var s in solutions)
                {
                    var sum = s + values[i];
                    if (sum > answer)
                    {
                        // If the sum is > answer, the product will also be >,
                        // so we can safely move on
                        continue;
                    }
                    temp.Add(sum);
                    if (sum == answer)
                    {
                        isValid = true;
                        break;
                    }

                    var product = s * values[i];
                    if (product > answer)
                    {
                        continue;
                    }
                    temp.Add(product);
                    if (product == answer)
                    {
                        isValid = true;
                        break;
                    }
                }
                solutions = temp;
            }

            if (isValid)
            {
                validValues.Add(answer);
            }
        }

        var validSum = validValues.Sum();
        return validSum;
    }

    public override long SolvePart2()
    {
        var validValues = new List<long>();

        foreach (var equation in InputEntries)
        {
            var parts = equation.Split(':');
            var answer = long.Parse(parts[0]);
            var values = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => long.Parse(s))
                .ToArray();
            
            var solutions = new List<long> { values[0] };
            var isValid = false;
            for (var i = 1; i < values.Length; i++)
            {
                var temp = new List<long>();
                foreach (var s in solutions)
                {
                    var sum = s + values[i];
                    if (sum > answer)
                    {
                        // If the sum is > answer, the same will be the case for
                        // both the product and the concatenation, so we can
                        // safely move on.
                        continue;
                    }
                    temp.Add(sum);
                    if (sum == answer)
                    {
                        isValid = true;
                        break;
                    }

                    var product = s * values[i];
                    if (product > answer)
                    {
                        // If the product is > answer, the same will be the case
                        // for the concatenation, so we can safely move on.
                        continue;
                    }
                    temp.Add(product);
                    if (product == answer)
                    {
                        isValid = true;
                        break;
                    }

                    var concatenated = long.Parse(s.ToString() + values[i]);
                    if (concatenated > answer)
                    {
                        continue;
                    }
                    temp.Add(concatenated);
                    if (concatenated == answer)
                    {
                        isValid = true;
                        break;
                    }
                }
                solutions = temp;
            }

            if (isValid)
            {
                validValues.Add(answer);
            }
        }

        var validSum = validValues.Sum();
        return validSum;
    }

    protected internal override string ParseInput(string inputItem)
    {
        return inputItem;
    }
}
