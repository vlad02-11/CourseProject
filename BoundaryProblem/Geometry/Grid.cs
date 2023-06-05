using BoundaryProblem.DataStructures;

namespace BoundaryProblem.Geometry;

public class Grid
{
    public Point2D[] Nodes => _nodes;
    public Element[] Elements { get; }
    public Point2D ElementLength { get; }
    public Point2D DistanceBetweenNodes => ElementLength / Element.StepsInsideElement;

    private readonly Point2D[] _nodes;

    public Grid(
        IEnumerable<Point2D> nodes,
        IEnumerable<Element> elements,
        Point2D elementLength
    )
    {
        _nodes = nodes.ToArray();
        Elements = elements.ToArray();
        ElementLength = elementLength;
    }

    public static void Serialize(Grid grid, ProblemFilePathsProvider filesProvider)
    {
        using StreamWriter nodesWriter = new(filesProvider.Nodes);
        SerializeNodes(grid._nodes, nodesWriter);

        using StreamWriter elementsWriter = new(filesProvider.Elems);
        SerializeElems(grid.Elements, grid.ElementLength, elementsWriter);
    }
    
    public static Grid Deserialize(ProblemFilePathsProvider filesProvider)
    {
        using StreamReader nodesReader = new(filesProvider.Nodes);
        using StreamReader elementsReader = new(filesProvider.Elems);

        var points = new Point2D[int.Parse(nodesReader.ReadLine())];

        var elementsInfo = elementsReader.ReadLine().Split(' ');
        var elements = new Element[int.Parse(elementsInfo[0])];
        var elementLength = new Point2D(
            double.Parse(elementsInfo[1]),
            double.Parse(elementsInfo[2])
        );

        for (var i = 0; ; i++)
        {
            var line = nodesReader.ReadLine();
            if (String.IsNullOrEmpty(line)) break;

            var values = line.Split(' ');

            points[i] = new Point2D(
                double.Parse(values[0]),
                double.Parse(values[1])
                );
        }

        var nodeIndexes = new int[Element.NodesInElement];

        for (var i = 0; ; i++)
        {
            var line = elementsReader.ReadLine();
            if (String.IsNullOrEmpty(line)) break;

            var values = line.Split(' ');
            
            for (var j = 0; j < nodeIndexes.Length; j++)
            {
                nodeIndexes[j] = int.Parse(values[j]);
            }

            elements[i] = new Element(
                nodeIndexes.ToArray(),
                int.Parse(values[nodeIndexes.Length])
            );
        }

        return new Grid(points, elements, elementLength);
    }

    private static void SerializeNodes(IReadOnlyCollection<Point2D> nodes, TextWriter writer)
    {
        writer.WriteLine(nodes.Count);
        foreach (var (x, y) in nodes)
            writer.WriteLine($"{x:F15} {y:F15}");
    }

    private static void SerializeElems(IReadOnlyCollection<Element> elems, Point2D elementLength, TextWriter writer)
    {
        writer.WriteLine($"{elems.Count} {elementLength.X:F15} {elementLength.Y:F15}");
        foreach (var elem in elems)
        {
            foreach (var index in elem.NodeIndexes)
            {
                writer.Write(index);
                writer.Write(' ');
            }
            writer.Write(elem.MaterialId);
            writer.WriteLine();
        }
    }
}