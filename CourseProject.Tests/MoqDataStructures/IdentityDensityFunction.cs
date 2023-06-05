using BoundaryProblem.DataStructures.DensityFunction;

namespace CourseProject.Tests.MoqDataStructures;

internal class IdentityDensityFunction : IDensityFunctionProvider
{
    public double Calc(int globalNodeIndex) => 1d;
}