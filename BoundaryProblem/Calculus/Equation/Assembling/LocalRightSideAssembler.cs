using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects;
using BoundaryProblem.DataStructures;
using BoundaryProblem.DataStructures.DensityFunction;

namespace BoundaryProblem.Calculus.Equation.Assembling;

public class LocalRightSideAssembler : LocalAssembler
{
    private readonly IDensityFunctionProvider _functionProvider;

    public LocalRightSideAssembler(
        IDensityFunctionProvider functionProvider,
        Matrix xMassTemplate, Matrix yMassTemplate
    ) : base(xMassTemplate, yMassTemplate)
    {
        _functionProvider = functionProvider;
    }

    public LocalVector Assemble(Element element)
    {
        var localVector = GetFunctionVector(element.NodeIndexes);
        var masses = GetDefaultMasses();

        return AttachIndexes(element.NodeIndexes, localVector * masses);
    }

    private Vector GetFunctionVector(int[] nodeIndexes)
    {
        var result = new double[LocalSize];
            
        for (var i = 0; i < LocalSize; i++)
            result[i] = _functionProvider.Calc(nodeIndexes[i]);

        return new Vector(result);
    }

    private static LocalVector AttachIndexes(int[] indexes, Vector vector)
    {
        return new LocalVector(vector, new IndexPermutation(indexes));
    }
}