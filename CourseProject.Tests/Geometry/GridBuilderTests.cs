using BoundaryProblem.Geometry;
using Rectangle = BoundaryProblem.Geometry.Rectangle;

namespace CourseProject.Tests.Geometry;

internal class GridBuilderTests
{
    private GridBuilder _gridBuilder;
    private Rectangle _rect;

    [SetUp]
    public void Setup()
    {
        _rect = new Rectangle(
            new Point2D(1, 3),
            new Point2D(7, 3),
            new Point2D(1, 15),
            new Point2D(7, 15)
        );

        _gridBuilder = new GridBuilder(_rect);
    }

    [Test]
    public void TestGridNodes()
    {
        var expected = new List<Point2D>();
        for (int y = (int)_rect.LeftBottom.Y; y <= (int)_rect.LeftTop.Y; y += 2)
        for (int x = (int)_rect.LeftBottom.X; x <= (int)_rect.RightBottom.X; x++)
            expected.Add(new Point2D(x, y));

        var computedNodes = _gridBuilder.Build(new AxisSplitParameter(2, 2)).Nodes;

        Assert.That(expected.SequenceEqual(computedNodes), Is.True);
    }

    [Test]
    public void TestNodeIndexes_Split_1X_To_1Y()
    {
        AxisSplitParameter splitting = new(1, 1);
        var nodeIndexes = _gridBuilder.Build(splitting).Elements.First().NodeIndexes;

        var expected = new List<int>();
        for (var i = 0; i < 16; i++)
            expected.Add(i);

        Assert.That(expected.SequenceEqual(nodeIndexes), Is.True);
    }

    [Test]
    public void TestNodeIndexes_Split_2X_To_2Y()
    {
        AxisSplitParameter splitting = new(2, 2);
        var forthElemNodeIndexes = _gridBuilder.Build(splitting)
            .Elements
            .Select(e => e.NodeIndexes)
            .ToArray()
            [3];

        var expected = new List<int>()
        {
            24, 25, 26, 27,
            31, 32, 33, 34,
            38, 39, 40, 41,
            45, 46, 47, 48
        };

        Assert.That(expected.SequenceEqual(forthElemNodeIndexes), Is.True);
    }

    [Test]
    public void TestNodeIndexes_Split_1X_To_2Y()
    {
        AxisSplitParameter splitting = new(1, 2);
        var elemsNodeIndexes = _gridBuilder.Build(splitting).Elements.Select(e => e.NodeIndexes);

        var expected = new List<List<int>>()
        {
            new()
            {
                0, 1, 2, 3,
                4, 5, 6, 7,
                8, 9, 10, 11,
                12, 13, 14, 15,
            },
            new()
            {
                12, 13, 14, 15,
                16, 17, 18, 19,
                20, 21, 22, 23,
                24, 25, 26, 27,
            }
        };

        Assert.Multiple(() =>
        {
            Assert.That(expected[0].SequenceEqual(elemsNodeIndexes.ToArray()[0]), Is.True);
            Assert.That(expected[1].SequenceEqual(elemsNodeIndexes.ToArray()[1]), Is.True);
        });
    }
}