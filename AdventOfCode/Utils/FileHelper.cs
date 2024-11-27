namespace AdventOfCode.Utils;

public static class FileHelper
{
    public static string GetInputFilePath(string fileName)
    {
        var assemblyLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location);
        var solutionDirectory = $"{assemblyLocation}\\..\\..\\..\\..\\";
        var solutionName = new DirectoryInfo(solutionDirectory).Name;
        var path = $"{solutionDirectory}..\\{solutionName}Input\\{fileName}";

        if (!File.Exists(path))
        {
            throw new Exception($"Missing input file [{path}]");
        }

        return path;
    }

    public static List<string> ReadFile(string filePath)
    {
        var lines = new List<string>();

        using var sr = new StreamReader(filePath);

        var lineItem = sr.ReadLine();
        while (lineItem != null)
        {
            lines.Add(lineItem);
            lineItem = sr.ReadLine();
        }

        return lines;
    }
}
