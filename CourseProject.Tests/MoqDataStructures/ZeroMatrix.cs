using BoundaryProblem.Calculus.Equation.DataStructures;

namespace CourseProject.Tests.MoqDataStructures;

internal class ZeroMatrix : Matrix
{
    private const int Size = 16;
    private static readonly double[,] IdentityValues;

    static ZeroMatrix()
    {
        IdentityValues = new double[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                IdentityValues[i, j] = 0;
            }
        }
    }

    public ZeroMatrix()
        : base(IdentityValues, 0)
    { }
}