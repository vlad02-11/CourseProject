using BoundaryProblem.Calculus.Equation;
using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.Solving;
using BoundaryProblem.Calculus.Equation.Solving.Preconditioner;
using CourseProject.Tests.Asserts;

namespace CourseProject.Tests.Calculus.Equation.Solving;

public class ConjugateGradientTests
{
    public double Precision => 1e-13;
    public int MaxIteration => 30000;

    public SymmetricSparseMatrix Matrix { get; set; }
    public Vector Solution { get; set; }
    public Vector RightSide { get; set; }
    
    public EquationData Equation { get; set; }

    [SetUp]
    public void SetUp ()
    {
        Matrix = new SymmetricSparseMatrix(
            rowIndexes: new[] { 0, 1, 3, 5, 7 },
            columnIndexes: new[] { 0, 0, 1, 0, 2, 1, 3 },
            values: new[] { 2d, 3d, 5d, 1d, 7d, 3d, -2d },
            diagonal: new[] { 3d, 7d, 1d, 5d, 8d }
        );

        Solution = new Vector(5d, 3d, -4d, -1d, 2d);

        RightSide = new Vector(8d, 17d, 19d, -32d, 27d);

        Equation = new EquationData(Matrix, solution: Vector.Create(5), rightSide: RightSide);
    }

    [Test]
    public void DiagonalPreconditionTest()
    {
        var solver = new ConjugateGradientSolver(
            new DiagonalPreconditioner(Matrix.Diagonal),
            precision: Precision,
            maxIteration: MaxIteration
        );

        var result = solver.Solve(Equation);

        Assert.Multiple(() =>
        {
            for (int i = 0; i < result.Length; i++)
                Assert.That(Math.Abs(Solution[i] - result[i]), Is.LessThanOrEqualTo(Precision));
            VectorAssert.AreEquals(result, Equation.Solution);
        });
    }
    
}