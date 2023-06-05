namespace BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects
{
    public class LocalMatrix : LocalObject
    {
        public double this[int x, int y] => _matrix[x, y];

        private readonly Matrix _matrix;

        public LocalMatrix(Matrix matrix, IndexPermutation permutation)
            : base(permutation)
        {
            _matrix = matrix;
        }
    }
}
