namespace BoundaryProblem.DataStructures.DensityFunction
{
    public interface IDensityFunctionProvider
    {
        public double Calc(int globalNodeIndex);
    }
}
