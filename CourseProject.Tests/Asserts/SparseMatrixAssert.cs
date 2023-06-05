using BoundaryProblem.Calculus.Equation.DataStructures;

namespace CourseProject.Tests.Asserts;

public static class SparseMatrixAssert
{
    public static void RowEqual(SparseMatrixRow row, double[] expected)
    {
        var rowValues = CloneRowValues(row);

        CollectionAssert.AreEqual(rowValues, expected);
    }

    public static void AreEqual(SymmetricSparseMatrix result, SymmetricSparseMatrix expected)
    {
        Assert.Multiple(() =>
        {
            CollectionAssert.AreEqual(result.Diagonal, expected.Diagonal);
            CollectionAssert.AreEqual(result.Values, expected.Values);
            CollectionAssert.AreEqual(result.RowIndexes.ToArray(), expected.RowIndexes.ToArray());
            CollectionAssert.AreEqual(result.ColumnIndexes.ToArray(), expected.ColumnIndexes.ToArray());
        });
    }

    private static List<double> CloneRowValues(SparseMatrixRow row)
    {
        var result = new List<double>();
        foreach (RefIndexValue iv in row)
        {
            result.Add(iv.Value);
        }
        return result;
    }
}