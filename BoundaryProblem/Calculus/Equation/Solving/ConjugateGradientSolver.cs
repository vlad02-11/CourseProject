using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.Solving.Preconditioner;

namespace BoundaryProblem.Calculus.Equation.Solving;

public class ConjugateGradientSolver
{
    private readonly IPreconditioner _preconditioner;
    private readonly double _precision;
    private readonly int _maxIteration;

    private EquationData _equation;
    private Vector r;
    private Vector z;
    private Vector _rNext;
    private Vector _aByZProduct;

    public ConjugateGradientSolver(IPreconditioner preconditioner, double precision, int maxIteration)
    {
        _preconditioner = preconditioner;
        _precision = precision;
        _maxIteration = maxIteration;
    }

    public Vector Solve(EquationData equation)
    {
        InitializeStartValues(equation);

        IterationProcess();

        return equation.Solution;
    }

    private void IterationProcess()
    {
        var fNorm = _equation.RightSide.Norm;
        
        for (var i = 1; i < _maxIteration && (r.Norm / fNorm) >= _precision; i++)
        {
            var preconditionedRScalarProduct = Vector.ScalarProduct(
                _preconditioner.MultiplyOn(r, _aByZProduct), // could pass any memory
                r
            );

            _aByZProduct = LinearAlgebra.Multiply(_equation.Matrix, z, _aByZProduct);
            
            var zScalarProduct = Vector.ScalarProduct(
                _aByZProduct,
                z
            );

            var alpha = preconditionedRScalarProduct / zScalarProduct;
            
            _equation.Solution = LinearAlgebra.LinearCombination(
                _equation.Solution, z,
                1d, alpha,
                _equation.Solution
            );

            _rNext = LinearAlgebra.LinearCombination(
                r, _aByZProduct,
                1d, -alpha,
                _rNext
            );

            var betta = Vector.ScalarProduct(_preconditioner.MultiplyOn(_rNext), _rNext) /
                        preconditionedRScalarProduct;

            z = LinearAlgebra.LinearCombination(
                _preconditioner.MultiplyOn(_rNext), z,
                1d, betta,
                z
            );

            r = _rNext;
        }
    }

    private void InitializeStartValues(EquationData equation)
    {
        _equation = equation;
        var AxProduct = LinearAlgebra.Multiply(equation.Matrix, equation.Solution);
        r = LinearAlgebra.Subtract(
            equation.RightSide,
            AxProduct
        );
        z = _preconditioner.MultiplyOn(r);

        _rNext = Vector.Create(equation.RightSide.Length);
        _aByZProduct = Vector.Create(equation.RightSide.Length);
    }
}