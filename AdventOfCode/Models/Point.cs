namespace AdventOfCode.Models;

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
/// </summary>
public readonly record struct Point(int X, int Y, int Z = 0)
{
    public Point Get(Direction direction)
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

    /// <summary>
    /// Get the point north (Y-1) of the current
    /// </summary>
    public Point GetN()
    {
        return this with { Y = Y - 1 };
    }

    /// <summary>
    /// Get the point south (Y+1) of the current
    /// </summary>
    public Point GetS()
    {
        return this with { Y = Y + 1 };
    }

    /// <summary>
    /// Get the point east (X+1) of the current
    /// </summary>
    public Point GetE()
    {
        return this with { X = X + 1 };
    }

    /// <summary>
    /// Get the point west (X-1) of the current
    /// </summary>
    public Point GetW()
    {
        return this with { X = X - 1 };
    }

    /// <summary>
    /// Get the point above (Z+1) the current
    /// </summary>
    public Point GetUp()
    {
        return this with { Z = Z + 1 };
    }

    /// <summary>
    /// Get the point below (Z-1) the current
    /// </summary>
    public Point GetDown()
    {
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