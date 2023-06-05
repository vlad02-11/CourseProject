namespace BoundaryProblem.DataStructures;

public readonly record struct Element(int[] NodeIndexes, int MaterialId = 0)
{
    public const int StepsInsideElement = 3;
    public const int NodesOnBound = StepsInsideElement + 1;
    public const int NodesInElement = NodesOnBound * NodesOnBound;

    public int[] GetBoundNodeIndexes(Bound bound) =>
        bound switch
        {
            Bound.Left => new[]
            {
                NodeIndexes[0],
                NodeIndexes[4],
                NodeIndexes[8],
                NodeIndexes[12]
            },
            Bound.Right => new[]
            {
                NodeIndexes[3],
                NodeIndexes[7],
                NodeIndexes[11],
                NodeIndexes[15]
            },
            Bound.Bottom => new[]
            {
                NodeIndexes[0],
                NodeIndexes[1],
                NodeIndexes[2],
                NodeIndexes[3]
            },
            Bound.Top => new[]
            {
                NodeIndexes[12],
                NodeIndexes[13],
                NodeIndexes[14],
                NodeIndexes[15]
            },
            _ => throw new ArgumentOutOfRangeException()
        };
}