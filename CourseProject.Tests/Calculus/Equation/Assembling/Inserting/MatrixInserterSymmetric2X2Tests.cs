using BoundaryProblem.Calculus.Equation.Assembling;
using BoundaryProblem.Calculus.Equation.Assembling.Algorithms;
using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects;
using CourseProject.Tests.Asserts;

namespace CourseProject.Tests.Calculus.Equation.Assembling.Inserting;

internal class MatrixInserterSymmetric2X2Tests
{
    private SymmetricSparseMatrix _sparseMatrix;
    private MatrixInserter _inserter;

    [SetUp]
    public void Setup()
    {
        _inserter = new MatrixInserter();
        _sparseMatrix = new SymmetricSparseMatrix(
            new[] { 0, 1, 2, 4, 6, 8 },
            new[] { 0, 1, 0, 1, 1, 2, 1, 4 }
        );
    }

    [Test]
    public void TestInsert_With_IdentityPermutation()
    {
        Matrix squareMatrix = new(new double[,]
        {
            { -1, 41 },
            { 41, 3 }
        });

        LocalMatrix localMatrix = new(
            squareMatrix,
            new IndexPermutation(new[] { 0, 1 })
        );

        _inserter.Insert(_sparseMatrix, localMatrix);

        var expectedDiagonal = new double[] { -1, 3, 0, 0, 0, 0 };
        var expectedFirstRow = new double[] { 41 };
        Assert.Multiple(() =>
        {
            Assert.That(_sparseMatrix.Diagonal
                    .SequenceEqual(expectedDiagonal),
                Is.True);
            SparseMatrixAssert.RowEqual(_sparseMatrix[1], expectedFirstRow);

            //Assert.That(_sparseMatrix[1].Select(iv => iv.Value)
            //        .SequenceEqual(expectedFirstRow),
            //    Is.True);
        });
    }

    [Test]
    public void TestInsert()
    {
        Matrix squareMatrix = new(new double[,]
        {
            { 50, 21 },
            { 21, -7 }
        });

        LocalMatrix localMatrix = new(
            squareMatrix,
            new IndexPermutation(new[] { 2, 4 })
        );

        _inserter.Insert(_sparseMatrix, localMatrix);

        var expectedDiagonal = new double[] { 0, 0, 50, 0, -7, 0 };
        var expectedSecondRow = new double[] { 0 };
        var expectedFourthRow = new double[] { 0, 21 };

        Assert.Multiple(() =>
        {
            Assert.That(_sparseMatrix.Diagonal
                    .SequenceEqual(expectedDiagonal),
                Is.True);

            SparseMatrixAssert.RowEqual(_sparseMatrix[2], expectedSecondRow);
            SparseMatrixAssert.RowEqual(_sparseMatrix[4], expectedFourthRow);

            //Assert.That(_sparseMatrix[2].Select(iv => iv.Value)
            //        .SequenceEqual(expectedSecondRow),
            //    Is.True);
            //Assert.That(_sparseMatrix[4].Select(iv => iv.Value)
            //        .SequenceEqual(expectedFourthRow),
            //    Is.True);
        });
    }
}