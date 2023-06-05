using BoundaryProblem.Calculus.Equation.DataStructures;

namespace BoundaryProblem.Calculus.Equation;

public static class LinearAlgebra
{
    public static Vector Subtract(Vector v, Vector u, Vector resultMemory=null)
    {
        return LinearCombination(v, u, 1.0, -1.0, resultMemory);
    }

    public static Vector Sum(Vector v, Vector u, Vector resultMemory= null)
    {
        return LinearCombination(v, u, 1.0, 1.0, resultMemory);
    }

    public static Vector LinearCombination(
        Vector v, Vector u,
        double vCoefficient, double uCoefficient,
        Vector resultMemory = null
    )
    {
        ValidateVectors(v, u);

        resultMemory ??= new Vector(new double[v.Length]);

        for (var i = 0; i < v.Length; i++)
            resultMemory[i] = v[i] * vCoefficient + u[i] * uCoefficient;

        return resultMemory;
    }

    public static Vector Multiply(double coefficient, Vector v, Vector resultMemory = null)
    {
        if (resultMemory == null)
            resultMemory = new Vector(new double[v.Length]);
        else
            ValidateVectors(v, resultMemory);

        for (var i = 0; i < v.Length; i++)
            resultMemory[i] = coefficient * v[i];

        return resultMemory;
    }

    public static Vector Multiply(SymmetricSparseMatrix matrix, Vector x, Vector resultMemory = null)
    {
        if (resultMemory == null)
        {
            resultMemory = new Vector(new double[x.Length]);
        }
        else
        {
            ValidateVectors(x, resultMemory);
        }
        if (matrix.RowIndexes.Length != x.Length)
        {
            throw new ArgumentOutOfRangeException($"{nameof(matrix.RowIndexes)} and {nameof(x)} must have the same length");
        }
        
        for (var i = 0; i < x.Length; i++)
        {
            resultMemory[i] = x[i] * matrix.Diagonal[i];
        }

        for (var i = 0; i < x.Length; i++)
        {
            foreach (RefIndexValue iv in matrix[i])
            {
                resultMemory[i] += iv.Value * x[iv.ColumnIndex];
                resultMemory[iv.ColumnIndex] += iv.Value * x[i];
            }
        }

        return resultMemory;
    }

    private static void ValidateVectors(Vector v, Vector u)
    {
        if (v.Length != u.Length)
            throw new ArgumentOutOfRangeException($"{nameof(v)} and {nameof(u)} must have the same length");
    }
}