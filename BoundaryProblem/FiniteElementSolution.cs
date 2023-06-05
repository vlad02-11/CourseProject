using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.DataStructures;
using BoundaryProblem.Geometry;

namespace BoundaryProblem;

public class FiniteElementSolution
{
    private static Func<double, double>[] LocalFunc { get; set; }

    static FiniteElementSolution()
    {
        LocalFunc = new Func<double, double>[]
        {
            x => -9d/2d * (x - 1d/3d)*(x - 2d/3d)*(x - 1d),
            x => 27d/2d * x*(x - 2d/3d)*(x - 1d),
            x => -27d/2d * x*(x - 1/3d)*(x - 1d),
            x => 9d/2d * x*(x - 1d/3d)*(x - 2d/3d)
        };
    }

    public Grid Grid { get; }
    public Vector FunctionWeights { get; }

    public FiniteElementSolution(Grid grid, Vector functionWeights)
    {
        Grid = grid;
        FunctionWeights = functionWeights;
    }

    public double Calculate(Point2D point)
    {
        // find elem
        var elem = Grid.Elements.First(x => ElementHas(x, point));
        var leftBottomNode = Grid.Nodes[elem.NodeIndexes[0]];
        Point2D ksi = new Point2D(
            (point.X - leftBottomNode.X)/ Grid.ElementLength.X,
            (point.Y - leftBottomNode.Y) / Grid.ElementLength.Y
            );

        var sum = 0d;

        for (int i = 0; i < Element.NodesInElement; i++)
        {
            var globalIndex = elem.NodeIndexes[i];
            var xIndex = i % 4;
            var yIndex = i / 4;

            sum += FunctionWeights[globalIndex] * LocalFunc[xIndex](ksi.X) * LocalFunc[yIndex](ksi.Y);
        }

        return sum;
    }

    public bool ElementHas(Element elem, Point2D point)
    {
        var leftBottomIndex= elem.NodeIndexes[0];
        var rightBottomIndex= elem.NodeIndexes[3];
        var leftTopIndex= elem.NodeIndexes[12];
        var rightTopIndex= elem.NodeIndexes[15];

        var rect = new Rectangle(
            LeftBottom: Grid.Nodes[leftBottomIndex],
            RightBottom: Grid.Nodes[rightBottomIndex],
            LeftTop: Grid.Nodes[leftTopIndex],
            RightTop: Grid.Nodes[rightTopIndex]
        );

        return rect.Contains(point);
    }
}