namespace AdventOfCode.Models;

/// <summary>
/// A point accompanied with a value of a specific type.
/// </summary>
public record Point<T> : Point where T : notnull
{
    public Point(int X, int Y, int Z = 0) : base(X, Y, Z)
    {
        // TODO: Is it possible to set/null out Value here
        // to avoid having the value copied when constructing
        // from another record using 'with'?
    }

    public T? Value { get; set; }

    public override string ToString()
    {
        if (Value == null) return base.ToString();

        return $"{X},{Y},{Z} ({Value})";
    }

    public Point ToPoint()
    {
        return this with { Value = default };
    }
}

/// <summary>
/// A two or three dimensional point, with X and Y (and Z) coordinates.
///
/// - X is east (+1) and west (-1).
/// - Y is south (+1) and north (-1)
/// - Z is up (+1) and down (-1)
///
/// Y is perhaps the opposite of what one might expect, but this is due to most
/// of the puzzles in AoC _increases_ the Y value when moving one row _down_
/// in a grid/matrix.
///
/// It is also possible to create a point with boundary values for X, Y and Z,
/// so that one can check whether getting the next point N/S/E/W/U/P of the
/// current point will succeed or not. By default a point has no boundaries,
/// and thus getting the next will always succeed.
/// </summary>
public record Point(int X, int Y, int Z = 0)
{
    public static readonly Direction[] Directions2D = [Direction.N, Direction.S, Direction.E, Direction.W];
    public static readonly Direction[] Directions3D = [Direction.N, Direction.S, Direction.E, Direction.W, Direction.U, Direction.D];
    public int? MaxX { get; init; } = default!;
    public int? MaxY { get; init; } = default!;
    public int? MinX { get; init; } = default!;
    public int? MinY { get; init; } = default!;
    public int? MinZ { get; init; } = default!;
    public int? MaxZ { get; init; } = default!;

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }

    public Point ToHashKey()
    {
        return new Point(X, Y, Z);
    }

    public bool Has(Direction direction)
    {
        return direction switch
        {
            Direction.N => HasN(),
            Direction.S => HasS(),
            Direction.E => HasE(),
            Direction.W => HasW(),
            Direction.U => HasUp(),
            Direction.D => HasDown(),
            _ => throw new ArgumentException($"Unknown direction {direction}")
        };
    }

    public Point? Get(Direction direction)
    {
        return direction switch
        {
            Direction.N => GetN(),
            Direction.S => GetS(),
            Direction.E => GetE(),
            Direction.W => GetW(),
            Direction.U => GetUp(),
            Direction.D => GetDown(),
            _ => throw new ArgumentException($"Unknown direction {direction}")
        };
    }

    public bool HasN() { return MinY == null || Y - 1 >= MinY; }

    /// <summary>
    /// Get the point north (Y-1) of the current
    /// </summary>
    /// <remarks>If the point is outside the boundary as defined by the point, null is returned.</remarks>
    public Point? GetN()
    {
        if (!HasN()) return null;
        return this with { Y = Y - 1 };
    }

    public bool HasS() { return MaxY == null || Y + 1 <= MaxY; }

    /// <summary>
    /// Get the point south (Y+1) of the current
    /// </summary>
    /// <remarks>If the point is outside the boundary as defined by the point, null is returned.</remarks>
    public Point? GetS()
    {
        if (!HasS()) return null;
        return this with { Y = Y + 1 };
    }

    public bool HasE() { return MaxX == null || X + 1 <= MaxX; }

    /// <summary>
    /// Get the point east (X+1) of the current
    /// </summary>
    /// <remarks>If the point is outside the boundary as defined by the point, null is returned.</remarks>
    public Point? GetE()
    {
        if (!HasE()) return null;
        return this with { X = X + 1 };
    }

    public bool HasW() { return MinX == null || X - 1 >= MinX; }

    /// <summary>
    /// Get the point west (X-1) of the current
    /// </summary>
    /// <remarks>If the point is outside the boundary as defined by the point, null is returned.</remarks>
    public Point? GetW()
    {
        if (!HasW()) return null;
        return this with { X = X - 1 };
    }

    public bool HasUp() { return MaxZ == null || Z + 1 <= MaxZ; }

    /// <summary>
    /// Get the point above (Z+1) the current
    /// </summary>
    /// <remarks>If the point is outside the boundary as defined by the point, null is returned.</remarks>
    public Point? GetUp()
    {
        if (!HasUp()) return null;
        return this with { Z = Z + 1 };
    }

    public bool HasDown() { return MinZ == null || Z - 1 >= MinZ; }

    /// <summary>
    /// Get the point below (Z-1) the current
    /// </summary>
    /// <remarks>If the point is outside the boundary as defined by the point, null is returned.</remarks>
    public Point? GetDown()
    {
        if (!HasDown()) return null;
        return this with { Z = Z - 1 };
    }

    public bool IsAdjacentTo(Point point)
    {
        if (this == point)
        {
            return false;
        }

        return Math.Abs(X - point.X) <= 1
            && Math.Abs(Y - point.Y) <= 1
            && Math.Abs(Z - point.Z) <= 1;
    }

    public int ManhattanDistanceTo(Point point)
    {
        return Math.Abs(X - point.X) + Math.Abs(Y - point.Y) + Math.Abs(Z - point.Z);
    }
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
