namespace BoundaryProblem.Calculus.Equation.Assembling.Algorithms;

public class GaussExcluding
{
    public EquationData ExcludeInSymmetric(EquationData equation, int row, double fixedValue)
    {
        var matrixSize = equation.Matrix.RowIndexes.Length;

        foreach (var indexValue in equation.Matrix[row])
        {
            equation.RightSide[indexValue.ColumnIndex] -= indexValue.Value * fixedValue;
            indexValue.SetValue(0);
        }

        for (var i = row + 1; i < matrixSize; i++)
        {
            var sparseRow = equation.Matrix[i];
            if (!sparseRow.HasColumn(row))
            {
                continue;
            }

            equation.RightSide[i] -= sparseRow[row] * fixedValue;
            sparseRow[row] = 0;
        }

        equation.Matrix.Diagonal[row] = 1d;
        equation.RightSide[row] = fixedValue;

        return equation;
    }
}