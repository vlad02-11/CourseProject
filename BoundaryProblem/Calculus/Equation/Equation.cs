using BoundaryProblem.Calculus.Equation.DataStructures;

namespace BoundaryProblem.Calculus.Equation;

public class EquationData
{
    public SymmetricSparseMatrix Matrix { get; set; }
    public Vector Solution { get; set; }
    public Vector RightSide { get; set; }

    public EquationData(SymmetricSparseMatrix matrix, Vector solution, Vector rightSide)
    {
        Matrix = matrix;
        Solution = solution;
        RightSide = rightSide;
    }
}
