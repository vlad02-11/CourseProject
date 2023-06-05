using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.Assembling;
using BoundaryProblem.Calculus.Equation.Assembling.Algorithms;
using BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects;
using CourseProject.Tests.Asserts;

namespace CourseProject.Tests.Calculus.Equation.Assembling.Inserting;

internal class MatrixInserterBigTests
{
    private SymmetricSparseMatrix _sparseMatrix;
    private MatrixInserter _inserter;

    [SetUp]
    public void Setup()
    {
        _inserter = new MatrixInserter();
        _sparseMatrix = new SymmetricSparseMatrix(
            new[] { 0, 0, 2, 4, 7, 12, 17, 21 },
            new[]
            {
                0, 1,
                0, 1,
                1, 2, 3,
                0, 1, 2, 3, 4,
                1, 2, 3, 4, 5,
                1, 2, 3, 4
            }
        );
    }

    [Test]
    public void TestInsert_3x3Symmetric()
    {
        Matrix squareMatrix = new(new double[,]
        {
            { 3, 8, 3 },
            { 8, 1, 16 },
            { 3, 16, 2 }
        });

        LocalMatrix localMatrix = new(
            squareMatrix,
            new IndexPermutation(new[] { 4, 5, 6 })
        );

        _inserter.Insert(_sparseMatrix, localMatrix);

        var expectedDiagonal = new double[] { 0, 0, 0, 0, 3, 1, 2, 0 };
        Assert.Multiple(() =>
        {
            Assert.That(_sparseMatrix.Diagonal
                    .SequenceEqual(expectedDiagonal),
                Is.True);
            SparseMatrixAssert.RowEqual(_sparseMatrix[4], new double[] { 0, 0, 0 });
            SparseMatrixAssert.RowEqual(_sparseMatrix[5], new double[] { 0, 0, 0, 0, 8 });
            SparseMatrixAssert.RowEqual(_sparseMatrix[6], new double[] { 0, 0, 0, 3, 16 });

            //Assert.That(_sparseMatrix[4].Select(iv => iv.Value)
            //        .SequenceEqual(new double[] { 0, 0, 0 }),
            //    Is.True);
            //Assert.That(_sparseMatrix[5].Select(iv => iv.Value)
            //        .SequenceEqual(new double[] { 0, 0, 0, 0, 8 }),
            //    Is.True);
            //Assert.That(_sparseMatrix[6].Select(iv => iv.Value)
            //        .SequenceEqual(new double[] { 0, 0, 0, 3, 16 }),
            //    Is.True);
        });
    }
}