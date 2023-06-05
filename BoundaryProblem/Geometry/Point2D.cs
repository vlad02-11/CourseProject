namespace BoundaryProblem.Geometry;

public readonly record struct Point2D(double X, double Y)
{
    public static Point2D operator /(Point2D point, double coefficient) => 
        new Point2D(point.X / coefficient, point.Y / coefficient);
}