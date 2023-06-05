using BoundaryProblem.Calculus.Equation.DataStructures;
using CourseProject.Tests.Asserts;

namespace CourseProject.Tests.Calculus.Equation.DataStructures;

public class SparseMatrixRowTests
{
    private SymmetricSparseMatrix Matrix;
    [SetUp]
    public void SetUp()
    {
        Matrix = new SymmetricSparseMatrix(
            rowIndexes: new[] { 0, 1, 2, 2, 4, 6 },
            columnIndexes: new[] { 0, 1, 1, 2, 1, 3 },
            values: new[] { 2d, 1d, 3d, -2d, 11d, 12d },
            diagonal: new []{3d, 4d, 3d, 0d, 2d, 1d}
        );
    }

    [TestCase(0)]
    [TestCase(1, 2d)]
    [TestCase(2, 1d)]
    [TestCase(3)]
    [TestCase(4, 3, -2)]
    [TestCase(5, 11, 12)]
    public void TestValues(int row, params double[] expected)
    {
        SparseMatrixAssert.RowEqual(Matrix[row], expected);
    }

    [Test]
    public void TestThrownIndexOutOfRange()
    {
        Assert.Throws<IndexOutOfRangeException>(() =>
        {
            var row = Matrix[5];
            var x = row[0];
        });
    }

    [TestCase(5, 0, false)]
    [TestCase(5, 1, true)]
    [TestCase(5, 2, false)]
    [TestCase(5, 3, true)]
    [TestCase(5, 4, false)]
    [TestCase(5, 5, false)]
    [TestCase(5, 6, false)]
    [TestCase(0, 0, false)]
    [TestCase(1, 0, true)]
    [TestCase(2, 0, false)]
    [TestCase(2, 1, true)]
    [TestCase(3, 0, false)]
    [TestCase(3, 1, false)]
    [TestCase(3, 2, false)]
    [TestCase(3, 3, false)]
    public void TestHasColumn(int row, int column, bool expected)
    {
        var has = Matrix[row].HasColumn(column);

        Assert.That(has, Is.EqualTo(expected));
    }

    [TestCase(5, 1, 11d)]
    [TestCase(5, 3, 12d)]
    [TestCase(1, 0, 2d)]
    [TestCase(2, 1, 1d)]
    public void TestIndexer(int row, int column, double expected)
    {
        var value = Matrix[row][column];

        Assert.That(value, Is.EqualTo(expected));
    }
}