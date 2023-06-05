using BoundaryProblem.Calculus;
using BoundaryProblem.DataStructures;
using BoundaryProblem.Geometry;

namespace CourseProject.Tests.Calculus;

internal class PortraitBuilderCubicFuncTests
{
    private PortraitBuilder _gridMatrixPortraitBuilder;
    private List<int> _expectedIG;
    private List<int> _expectedJG;
    private Point2D[] _points;

    [SetUp]
    public void Setup()
    {
        _gridMatrixPortraitBuilder = new PortraitBuilder();

        List<List<int>> lowerNodesLists = new();
        for (int i = 0; i < 16; i++)
        {
            var lowerNodes = new List<int>();
            for (int j = 0; j < i; j++)
            {
                lowerNodes.Add(j);
            }
            lowerNodesLists.Add(lowerNodes);
        }
        for (int i = 16; i < 28; i++)
        {
            var lowerNodes = new List<int>();
            for (int j = 12; j < i; j++)
            {
                lowerNodes.Add(j);
            }
            lowerNodesLists.Add(lowerNodes);
        }

        _expectedIG = new List<int>();
        _expectedJG = new List<int>();
        int count = 0;

        foreach (var lowerNodes in lowerNodesLists)
        {
            count += lowerNodes.Count;
            _expectedIG.Add(count);
            _expectedJG.AddRange(lowerNodes);
        }

        _points = new Point2D[28];
    }

    private Grid Splited_1X_To_2Y_Grid => new(
        _points,
        new List<Element>
        {
            new Element(new[]
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,
                12, 13, 14, 15,
            }),
            new Element(new[]
            {
                12, 13, 14, 15,
                16, 17, 18, 19,
                20, 21, 22, 23,
                24, 25, 26, 27,
            })
        },
        default);

    [Test]
    public void TestRowIndexes_Split_1X_To_2Y()
    {
        var grid = Splited_1X_To_2Y_Grid;

        var rowIndexes = _gridMatrixPortraitBuilder.Build(grid)
            .RowIndexes
            .ToArray();

        Assert.That(_expectedIG.SequenceEqual(rowIndexes), Is.True);
    }

    [Test]
    public void TestColumnIndexes_Split_1X_To_2Y()
    {
        var grid = Splited_1X_To_2Y_Grid;

        var rowIndexes = _gridMatrixPortraitBuilder.Build(grid)
            .ColumnIndexes
            .ToArray();

        Assert.That(_expectedJG.SequenceEqual(rowIndexes), Is.True);
    }
}