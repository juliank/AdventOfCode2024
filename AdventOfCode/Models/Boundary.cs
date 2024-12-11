namespace AdventOfCode.Models;

public record class Boundary
{
    public int? MaxX { get; init; }
    public int? MaxY { get; init; }
    public int? MinX { get; init; }
    public int? MinY { get; init; }
    public int? MinZ { get; init; }
    public int? MaxZ { get; init; }
}
