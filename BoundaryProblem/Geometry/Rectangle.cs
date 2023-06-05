namespace BoundaryProblem.Geometry
{
    /// <summary>
    /// --------
    /// |3    4|
    /// |      |
    /// |1    2|
    /// --------
    /// </summary>
    public readonly record struct Rectangle(
        Point2D LeftBottom,
        Point2D RightBottom,
        Point2D LeftTop,
        Point2D RightTop
    )
    {
        public bool Contains(Point2D point)
        {
            bool lowerThanTop = point.Y <= LeftTop.Y;
            bool upperThanBot = point.Y >= LeftBottom.Y;
            bool righterThanLeft = point.X >= LeftBottom.X;
            bool lefterThanRight = point.X <= RightBottom.X;

            return lowerThanTop && upperThanBot && righterThanLeft && lefterThanRight;
        }
    }
}
