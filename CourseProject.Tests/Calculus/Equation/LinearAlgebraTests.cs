using BoundaryProblem.Calculus.Equation;
using BoundaryProblem.Calculus.Equation.DataStructures;
using CourseProject.Tests.Asserts;

namespace CourseProject.Tests.Calculus.Equation;

internal class LinearAlgebraTests
{
    [Test]
    public void MultiplyOnSparseMatrixTest()
    {
        var matrix = new SymmetricSparseMatrix(
            rowIndexes: new [] {0, 1, 3, 5, 7},
            columnIndexes: new [] {0, 0, 1, 0, 2, 1, 3},
            values: new [] {2d, 3d, 5d, 1d, 7d, 3d, -2d},
            diagonal: new [] { 3d, 7d, 1d, 5d, 8d}
        );

        var vector = new Vector(5d, 3d, -4d, -1d, 2d);

        var expected = new Vector(8d, 17d, 19d, -32d, 27d);

        var result = LinearAlgebra.Multiply(matrix, vector);

        VectorAssert.AreEquals(expected, result);
    }

}