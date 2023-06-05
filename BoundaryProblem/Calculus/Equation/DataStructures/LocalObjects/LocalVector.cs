namespace BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects
{
    public class LocalVector : LocalObject
    {
        public double this[int x] => _vector[x];

        private readonly Vector _vector;

        public LocalVector(Vector vector, IndexPermutation permutation)
            : base(permutation)
        {
            _vector = vector;
        }
    }
}
