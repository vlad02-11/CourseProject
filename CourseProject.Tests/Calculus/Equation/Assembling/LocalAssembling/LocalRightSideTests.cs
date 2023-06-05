using BoundaryProblem.Calculus.Equation.Assembling;
using BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects;
using BoundaryProblem.DataStructures;
using BoundaryProblem.DataStructures.DensityFunction;
using CourseProject.Tests.MoqDataStructures;

namespace CourseProject.Tests.Calculus.Equation.Assembling.LocalAssembling;

internal class LocalRightSideTests
{
    private int[] _nodeIndexes;
    private LocalRightSideAssembler _assembler;

    [SetUp]
    public void SetUp()
    {
        _nodeIndexes = new[]
        {
            3, 4, 5, 6,
            10, 11, 12, 13,
            17, 18, 19, 20,
            24, 25, 26, 27,
        };

        var functionValues = new Dictionary<int, double>
        {
            [3] = 0, [4] = 0, [5] = 0, [6] = 0,
            [10] = 0, [11] = 1, [12] = 2, [13] = 3,
            [17] = 0, [18] = 2, [19] = 4, [20] = 6,
            [24] = 0, [25] = 3, [26] = 6, [27] = 9
        };

        _assembler = new LocalRightSideAssembler(
            new FunctionDefinedOnNodes(functionValues),
            new IdentityMatrix(),
            new IdentityMatrix()
        );

    }

    [Test]
    public void ValuesTest()
    {
        LocalVector result = _assembler.Assemble(new Element(_nodeIndexes));

        var expected = new double[]
        {
            0, 0, 0, 0,
            0, 1, 2, 3,
            0, 2, 4, 6,
            0, 3, 6, 9
        };

        Assert.Multiple(() =>
        {
            for (int i = 0; i < _nodeIndexes.Length; i++)
            {
                Assert.That(result[i], Is.EqualTo(expected[i]));
            }
        });
    }

    [Test]
    public void IndexesTest()
    {
        LocalVector result = _assembler.Assemble(new Element(_nodeIndexes));

        var expected = new double[]
        {
            3, 4, 5, 6,
            10, 11, 12, 13,
            17, 18, 19, 20,
            24, 25, 26, 27,
        };

        Assert.Multiple(() =>
        {
            for (int i = 0; i < _nodeIndexes.Length; i++)
            {
                Assert.That(
                    result.IndexPermutation.ApplyPermutation(i),
                    Is.EqualTo(expected[i])
                );
            }
        });
    }
}