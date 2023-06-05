using BoundaryProblem.Calculus.Equation.DataStructures;

namespace CourseProject.Tests.Asserts;

public class MatrixAssert
{
    public static void Equals(Matrix result, Matrix expected)
    {
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Length.EqualTo(expected.Length));

            for (int i = 0; i < result.Length; i++)
                for (int j = 0; j < result.Length; j++)
                    Assert.That(result[i, j], Is.EqualTo(expected[i, j]));
        });
    }
}