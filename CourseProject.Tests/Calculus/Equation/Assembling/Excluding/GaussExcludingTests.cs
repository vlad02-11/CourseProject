using BoundaryProblem.Calculus.Equation;
using BoundaryProblem.Calculus.Equation.Assembling.Algorithms;
using BoundaryProblem.Calculus.Equation.DataStructures;
using CourseProject.Tests.Asserts;

namespace CourseProject.Tests.Calculus.Equation.Assembling.Excluding;

public class GaussExcludingTests
{
    private GaussExcluding Excluder;
    private EquationData Equation;

    [SetUp]
    public void SetUp()
    {
        Excluder = new GaussExcluding();
        Equation = new EquationData(
            new SymmetricSparseMatrix(
                rowIndexes: new[] { 0, 1, 2, 2, 4, 6 },
                columnIndexes: new[] { 0, 1, 1, 2, 1, 3 },
                values: new[] { 2d, 1d, 3d, -2d, 11d, 12d },
                diagonal: new[] { 3d, 4d, 3d, 0d, 2d, 1d }
            ),
            default,
            Vector.Create(6, i => i + 1)
        );

    }

    [Test]
    public void TestSecondRowExcluding()
    {
        var result = Excluder.ExcludeInSymmetric(Equation, 2, 9);

        var expectedMatrix = new SymmetricSparseMatrix(
            rowIndexes: new[] { 0, 1, 2, 2, 4, 6 },
            columnIndexes: new[] { 0, 1, 1, 2, 1, 3 },
            values: new[] { 2d, 0, 3d, 0, 11d, 12d },
            diagonal: new[] { 3d, 4d, 1d, 0d, 2d, 1d }
        );
        var expectedVector = new[]
        {
            1d,
            2d - 1 * 9d,
            9d,
            4d,
            5d - (-2d) * 9d,
            6d
        };

        Assert.Multiple(() =>
        {
            SparseMatrixAssert.AreEqual(result.Matrix, expectedMatrix);
            Assert.That(result.RightSide, Has.Length.EqualTo(expectedVector.Length));

            for (int i = 0; i < expectedVector.Length; i++)
                Assert.That(result.RightSide[i], Is.EqualTo(expectedVector[i]));
        });

    }
}