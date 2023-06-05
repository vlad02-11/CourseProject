using BoundaryProblem.Calculus.Equation.Assembling;
using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.DataStructures;
using CourseProject.Tests.MoqDataStructures;

namespace CourseProject.Tests.Calculus.Equation.Assembling.LocalAssembling;

internal class MassTests
{
    private LocalMatrixAssembler _assembler;
    private IMaterialProvider _materialProvider;
    private int[] _identityPermutation;
    [SetUp]
    public void Setup()
    {
        _identityPermutation = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
            
        _materialProvider = new IdentityMaterialProvider();

        _assembler = new LocalMatrixAssembler(
            _materialProvider,
            xMassTemplate: new Matrix(new double[,]
            {
                {6, -1, -20, 27},
                {-4, 3, 17, 41},
                {-5, 4, 7, -17},
                {12, -2, -3, 8}
            }),
            yMassTemplate: new Matrix(new double[,]
            {
                {4, -1, 43, 3},
                {34, 7, 2, 5},
                {-8, -11, 4, -18},
                {-4, 19, 6, -2}
            }),

            xStiffnessTemplate: new ZeroMatrix(),
            yStiffnessTemplate: new ZeroMatrix()
        );
    }

    [TestCase(0, 9, -43d)]
    [TestCase(13, 6, 323d)]
    [TestCase(4, 8, 12d)]
    [TestCase(6, 13, 20d)]
    public void CorrectValuesTest(int x, int y, double expected)
    {
        var assembledMatrix = _assembler.Assemble(
            new Element(_identityPermutation));

        Assert.That(assembledMatrix[x, y], Is.EqualTo(expected));
    }
}