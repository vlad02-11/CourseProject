using BoundaryProblem.Calculus.Equation.DataStructures;

namespace CourseProject.Tests.MoqDataStructures;

internal class OneFilledMatrix : Matrix
{
    private const int Size = 16;
    private static readonly double[,] IdentityValues;

    static OneFilledMatrix()
    {
        IdentityValues = new double[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                IdentityValues[i, j] = 1;
            }
        }
    }

    public OneFilledMatrix()
        : base(IdentityValues)
    { }
}