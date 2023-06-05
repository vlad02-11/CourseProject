using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.DataStructures;

namespace BoundaryProblem.Calculus.Equation.Assembling;

public abstract class LocalAssembler
{
    protected const int LocalSize = Element.NodesInElement;

    protected readonly Matrix XMassTemplate;
    protected readonly Matrix YMassTemplate;

    protected LocalAssembler(Matrix xMassTemplate, Matrix yMassTemplate)
    {
        XMassTemplate = xMassTemplate;
        YMassTemplate = yMassTemplate;
    }

    protected Matrix GetDefaultMasses()
    {
        var masses = new double[LocalSize, LocalSize];

        for (var i = 0; i < LocalSize; i++)
        for (var j = 0; j < LocalSize; j++)
            masses[i, j] =
                XMassTemplate[IndexFromX(i), IndexFromX(j)] *
                YMassTemplate[IndexFromY(i), IndexFromY(j)];

        return new Matrix(masses);
    }

    protected static int IndexFromX(int i) => i % 4;

    protected static int IndexFromY(int i) => i / 4;
}