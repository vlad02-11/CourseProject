using BoundaryProblem.Calculus.Equation.DataStructures;

namespace CourseProject.Tests.Asserts;

public static class VectorAssert
{
    public static void AreEquals(Vector result, Vector expected)
    {
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Length.EqualTo(expected.Length));

            for (var i = 0; i < result.Length; i++)
                Assert.That(result[i], Is.EqualTo(expected[i]));
        });
    }
}