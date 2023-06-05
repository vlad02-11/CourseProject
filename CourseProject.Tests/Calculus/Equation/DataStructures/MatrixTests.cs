using BoundaryProblem.Calculus.Equation.DataStructures;

namespace CourseProject.Tests.Calculus.Equation.DataStructures;

internal class MatrixTests
{
    private Matrix _matrix;

    [SetUp]
    public void SetUp()
    {
        _matrix = new Matrix(new double[,]
        {
            {-5, 16, 21},
            {7, 13, 50},
            {-20, -9, 444}
        });
    }
        
    [TestCase(0, 0, 4d, -20d)]
    [TestCase(2, 1, 0.1d, 5d)]
    [TestCase(0, 1, -7d, -49d)]
    [TestCase(1, 2, 0, 0)]
    public void MultiplyTest(int x, int y, double coef, double expected)
    {
        var multMatrix = _matrix * coef;

        Assert.That(multMatrix[y, x], Is.EqualTo(expected));
    }
}