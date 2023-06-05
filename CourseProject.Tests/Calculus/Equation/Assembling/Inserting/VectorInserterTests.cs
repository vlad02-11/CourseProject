using BoundaryProblem.Calculus.Equation.Assembling;
using BoundaryProblem.Calculus.Equation.Assembling.Algorithms;
using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects;

namespace CourseProject.Tests.Calculus.Equation.Assembling.Inserting;

internal class VectorInserterTests
{
    private VectorInserter _inserter;
    private Vector _targetVector;
    private Vector _insertableVector;

    [SetUp]
    public void Setup()
    {
        _inserter = new VectorInserter();
        _targetVector = new Vector(new double[]
        {
            5,
            6,
            7,
            14,
            -15,
            9,
            -4
        });
        _insertableVector = new Vector(new double[]
        {
            7, 1, 5, -20
        });
    }

    [Test]
    public void InsertionIntoBegin()
    {
        var localVector = new LocalVector(
            _insertableVector,
            new IndexPermutation(
                new[] { 0, 1, 2, 3 }
            ));

        _inserter.Insert(_targetVector, localVector);

        var expected = new []
        {
            5d + 7d,
            6d + 1d,
            7d + 5d,
            14d - 20d,
            -15,
            9,
            -4
        };

        Assert.Multiple(() =>
        {
            for (var i = 0; i < expected.Length; i++)
                Assert.That(expected[i], Is.EqualTo(_targetVector[i]));
        });
    }

    [Test]
    public void InsertionIntoEnd()
    {
        var localVector = new LocalVector(
            _insertableVector,
            new IndexPermutation(
                new[] { 3, 4, 5, 6 }
            ));

        _inserter.Insert(_targetVector, localVector);

        var expected = new[]
        {
            5d,
            6d,
            7d,
            14d +7d,
            -15 + 1d,
            9 + 5d,
            -4 - 20d
        };

        Assert.Multiple(() =>
        {
            for (var i = 0; i < expected.Length; i++)
                Assert.That(expected[i], Is.EqualTo(_targetVector[i]));
        });
    }

    [Test]
    public void VariedPermutation()
    {
        var localVector = new LocalVector(
            _insertableVector,
            new IndexPermutation(
                new[] { 0, 3, 5, 6 }
            ));

        _inserter.Insert(_targetVector, localVector);

        var expected = new[]
        {
            5d + 7d,
            6d,
            7d,
            14d + 1d,
            -15,
            9 + 5d,
            -4 - 20d
        };

        Assert.Multiple(() =>
        {
            for (var i = 0; i < expected.Length; i++)
                Assert.That(expected[i], Is.EqualTo(_targetVector[i]));
        });
    }

}