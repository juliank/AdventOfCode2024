namespace AdventOfCode.Models;

public static class Directions
{
    /// <summary>
    /// All possible <see cref="Direction"/> values in a two-dimensional space.
    /// </summary>
    public static readonly Direction[] D2 = [Direction.N, Direction.S, Direction.E, Direction.W];

    /// <summary>
    /// All possible <see cref="Direction"/> values in a three-dimensional space.
    /// </summary>
    public static readonly Direction[] D3 = [Direction.N, Direction.S, Direction.E, Direction.W, Direction.U, Direction.D];
}

public enum Direction
{
    /// <summary>
    /// North => Y - 1
    /// </summary>
    N,
    /// <summary>
    /// South => Y + 1
    /// </summary>
    S,
    /// <summary>
    /// East => X + 1
    /// </summary>
    E,
    /// <summary>
    /// West => X - 1
    /// </summary>
    W,
    /// <summary>
    /// Up => Z + 1
    /// </summary>
    U,
    /// <summary>
    /// Down => Z - 1
    /// </summary>
    D
}
