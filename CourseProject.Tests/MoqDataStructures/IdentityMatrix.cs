using BoundaryProblem.Calculus.Equation.DataStructures;

namespace CourseProject.Tests.MoqDataStructures
{
    internal class IdentityMatrix : Matrix
    {
        private const int Size = 16;
        private static readonly double[,] IdentityValues;

        static IdentityMatrix()
        {
            IdentityValues = new double[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                IdentityValues[i, i] = 1;
            }
        }

        public IdentityMatrix()
            : base(IdentityValues)
        { }
    }
}
