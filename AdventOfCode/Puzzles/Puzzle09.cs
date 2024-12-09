namespace AdventOfCode.Puzzles;

public class Puzzle09 : Puzzle<int[], long>
{
    private const int PuzzleId = 09;

    public Puzzle09() : base(PuzzleId) { }

    public Puzzle09(params IEnumerable<int[]> inputEntries) : base(PuzzleId, inputEntries) { }

    public override long SolvePart1()
    {
        var diskMap = InputEntries[0];
        var diskLayout = CreateDiskLayout(diskMap);
        var compactedDiskLayout = CompactDiskLayoutWithFragmentation(diskLayout);
        var checksum = CalculateChecksum(compactedDiskLayout);
        return checksum;
    }

    public override long SolvePart2()
    {
        var diskMap = InputEntries[0];
        var diskLayout = CreateDiskLayout(diskMap);
        var compactedDiskLayout = CompactDiskLayoutWithoutFragmentation(diskLayout);
        var checksum = CalculateChecksum(compactedDiskLayout);
        return checksum;
    }

    internal static List<int> CreateDiskLayout(int[] diskMap)
    {
        var diskLayout = new List<int>(diskMap.Length * 10); // Make initial capacity *large*, to avoid resizes
        
        var fileIndex = 0;
        var isFile = true; // The first digit is always a file
        foreach (var length in diskMap)
        {
            var fileId = -1;
            if (isFile)
            {
                fileId = fileIndex;
                fileIndex++;
            }

            for (int i = 0; i < length; i++)
            {
                diskLayout.Add(fileId);
            }
            
            isFile = !isFile;
        }

        return diskLayout;
    }

    internal static List<int> CompactDiskLayoutWithFragmentation(List<int> diskLayout)
    {
        var lastI = diskLayout.Count - 1;
        for (var i = 0; i <= lastI; i++)
        {
            if (diskLayout[i] != -1)
            {
                // There is already a file at this disk location
                continue;
            }

            while (diskLayout[lastI] == -1)
            {
                lastI--;
            }
            if (lastI <= i)
            {
                break;
            }
            diskLayout[i] = diskLayout[lastI];
            diskLayout[lastI] = -1;
        }
        return diskLayout.Where(i => i != -1).ToList();
    }

    internal static List<int> CompactDiskLayoutWithoutFragmentation(List<int> diskLayout)
    {
        // Start with the last file (will have the highest index)
        var lastIndexCursor = diskLayout.Count - 1;
        for (var fileId = diskLayout[lastIndexCursor]; fileId > 0; fileId--)
        {
            var lastIndex = lastIndexCursor;
            while (diskLayout[lastIndex] != fileId)
            {
                lastIndex--;
            }
            var firstIndex = lastIndex;
            while (diskLayout[firstIndex - 1] == fileId)
            {
                firstIndex--;
            }
            lastIndexCursor = firstIndex - 1; // Position cursor for next iteration of for loop

            var fileLength = lastIndex - firstIndex + 1;

            var availableSpace = 0;
            var firstSpaceIndex = 0;
            for (var i = 0; i < firstIndex; i++)
            {
                if (diskLayout[i] != -1)
                {
                    // Reset available space and start counting at next i
                    availableSpace = 0;
                    continue;
                }

                if (availableSpace == 0)
                {
                    firstSpaceIndex = i;
                }
                availableSpace++;

                if (availableSpace >= fileLength)
                {
                    break;
                }
            }

            if (availableSpace < fileLength)
            {
                continue; // Cannot move file, move to next (lower) file id
            }

            for (var i = firstSpaceIndex; i < firstSpaceIndex + fileLength; i++)
            {
                diskLayout[i] = fileId;
            }
            for (var i = firstIndex; i <= lastIndex; i++)
            {
                diskLayout[i] = -1;
            }
        }

        return diskLayout;
    }

    private static long CalculateChecksum(List<int> compactedDiskLayout)
    {
        var checksum = 0L;
        
        foreach (var (index, fileId) in compactedDiskLayout.Index())
        {
            if (fileId == -1)
            {
                continue;
            }
            checksum += index * fileId;
        }

        return checksum;
    }

    protected internal override int[] ParseInput(string inputItem)
    {
        return inputItem.Select(c => int.Parse(c.ToString())).ToArray();
    }
}
