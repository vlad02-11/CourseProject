using BoundaryProblem.Calculus.Equation.DataStructures;

namespace BoundaryProblem.Calculus.Equation.Solving.Preconditioner;

public interface IPreconditioner
{
    public Vector MultiplyOn(Vector v, Vector resultMemory = null);
}