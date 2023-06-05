namespace BoundaryProblem.Calculus.Equation.DataStructures
{
    public readonly struct IndexPermutation
    {
        public int ApplyPermutation(int index) => _permutation[index];
        public int Length => _permutation.Length;

        private readonly int[] _permutation;

        public IndexPermutation(int[] permutation)
        {
            _permutation = permutation;
        }
    }
}
