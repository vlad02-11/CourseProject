using BoundaryProblem.Calculus.Equation;
using BoundaryProblem.Calculus.Equation.Assembling;
using BoundaryProblem.Calculus.Equation.Assembling.Algorithms;
using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.DataStructures;
using BoundaryProblem.DataStructures.BoundaryConditions;
using BoundaryProblem.DataStructures.BoundaryConditions.Second;
using BoundaryProblem.Geometry;
using CourseProject.Tests.Asserts;


namespace CourseProject.Tests.Calculus.Equation.Assembling.Global;

public class SecondBoundaryApplyingTests
{
    public EquationData Equation { get; set; }
    public Vector RightSide { get; set; }
    public Grid Grid { get; set; }
    public GlobalAssembler Assembler { get; set; }

    [SetUp]
    public void SetUp()
    {
        RightSide = new Vector(
            0, 1, 2, 3,
            4, 5, 6, 7,
            8, 9, 10, 11,
            12, 13, 14, 15
        );

        Grid = new Grid(
            nodes: Array.Empty<Point2D>(),
            elements: new Element[]
            {
                new Element(
                    NodeIndexes: new[]
                    {
                        0, 1, 2, 3,
                        4, 5, 6, 7,
                        8, 9, 10, 11,
                        12, 13, 14, 15
                    },
                    MaterialId: default
                )
            },
            elementLength: new Point2D(1d, 2d)
        );

        Equation = new EquationData(default, default, RightSide);

        Assembler = new GlobalAssembler(
            grid: Grid,
            materialProvider: default,
            functionProvider: default
        );
    }


    [Test]
    public void TwoConditionTest()
    {
        #region Expected
        var expected = RightSide.Copy();
        Vector rightBorderAdditive = Vector.Create(4, -1) * Assembler.YMassMatrix;
        Vector bottomBorderAdditive = Vector.Create(4, 2) * Assembler.XMassMatrix;

        for (int i = 0; i < 4; i++)
            expected[i] += bottomBorderAdditive[i];
        expected[3] += rightBorderAdditive[0];
        expected[7] += rightBorderAdditive[1];
        expected[11] += rightBorderAdditive[2];
        expected[15] += rightBorderAdditive[3];
        #endregion

        var conditions = new SecondBoundaryProvider(
            new FlowUnit(0, Bound.Bottom, new[] { 2d, 2d, 2d, 2d }),
            new FlowUnit(0, Bound.Right, new[] { -1d, -1d, -1d, -1d })
        );

        Assembler.ApplySecondBoundaryConditions(Equation, conditions);
        var result = Equation.RightSide;

        VectorAssert.AreEquals(result, expected);
    }
}