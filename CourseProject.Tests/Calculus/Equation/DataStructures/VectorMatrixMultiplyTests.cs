using BoundaryProblem.Calculus.Equation.DataStructures;

namespace CourseProject.Tests.Calculus.Equation.DataStructures;

internal class VectorMatrixMultiplyTests
{

    [Test]
    public void TestMatrixMultiply()
    {
        Matrix matrix = new(new double[,]
        {
            {5, -8, -2, -3, 5},
            {3, -1, 4, 4, 2},
            {-2, 1, -3, 4, -3},
            {5, 9, 6, 5, 3},
            {2, -1, 1, 5, 4}
        });

        Vector vector = new (new double[] {1, 2, 3, 4, 5});

        var result = vector * matrix;
        var expected = new double[] {-4, 39, -8, 76, 43};

        Assert.Multiple(() =>
        {
            for (int i = 0; i < expected.Length; i++)
                Assert.That(result[i], Is.EqualTo(expected[i]));
        });
    }
}