namespace BoundaryProblem.Calculus.Equation.DataStructures
{
    public class Matrix
    {
        public virtual double this[int x, int y] => _values[x, y] * _coefficient;
        public int Length => _values.GetLength(0);

        private readonly double[,] _values;
        private readonly double _coefficient;

        public Matrix(double[,] values)
        {
            _values = values;
            _coefficient = 1;
        }

        protected Matrix(double[,] values, double coefficient)
        {
            _values = values;
            _coefficient = coefficient;
        }

        public static Matrix operator *(Matrix matrix, double coefficient)
        {
            return new Matrix(matrix._values, matrix._coefficient * coefficient);
        }
        public static Matrix operator *(double coefficient, Matrix matrix)
        {
            return matrix * coefficient;
        }
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.Length != b.Length) throw new ArgumentException();

            var values = new double[a.Length, a.Length];
            
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < a.Length; j++)
                    values[i, j] = a[i, j] + b[i, j];

            return new Matrix(values);
        }

        public Matrix Clone()
        {
            var newValues = new double[Length, Length];
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                {
                    newValues[i, j] = this[i, j];
                }
            }

            return new Matrix(newValues, 1d);
        }
    }
}
