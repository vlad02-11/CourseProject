using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects;

namespace BoundaryProblem.Calculus.Equation.Assembling.Algorithms
{
    public class MatrixInserter
    {
        public void Insert(SymmetricSparseMatrix sparseMatrix, LocalMatrix localMatrix)
        {
            var matrixSize = localMatrix.IndexPermutation.Length;
            for (var i = 0; i < matrixSize; i++)
            {
                var row = localMatrix.IndexPermutation
                    .ApplyPermutation(i);

                for (var j = 0; j < matrixSize; j++)
                {
                    var column = localMatrix.IndexPermutation
                        .ApplyPermutation(j);
                    if (column > row) continue;
                    
                    sparseMatrix[row, column] += localMatrix[i, j];
                }
            }
        }
    }
}
