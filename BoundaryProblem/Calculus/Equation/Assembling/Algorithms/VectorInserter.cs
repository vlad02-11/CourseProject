using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects;

namespace BoundaryProblem.Calculus.Equation.Assembling.Algorithms
{
    public class VectorInserter
    {
        public void Insert(Vector vector, LocalVector localVector)
        {
            var vectorLength = localVector.IndexPermutation.Length;
            for (var i = 0; i < vectorLength; i++)
            {
                var row = localVector.IndexPermutation
                    .ApplyPermutation(i);
                vector[row] += localVector[i];
            }
        }
    }
}
