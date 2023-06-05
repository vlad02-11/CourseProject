namespace BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects
{
    public abstract class LocalObject
    {
        public readonly IndexPermutation IndexPermutation;

        protected LocalObject(IndexPermutation indexPermutation)
        {
            IndexPermutation = indexPermutation;
        }
    }
}
