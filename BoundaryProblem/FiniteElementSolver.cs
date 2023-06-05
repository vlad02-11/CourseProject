using BoundaryProblem.Calculus.Equation.Assembling;
using BoundaryProblem.Calculus.Equation.Solving;
using BoundaryProblem.Calculus.Equation.Solving.Preconditioner;
using BoundaryProblem.DataStructures.BoundaryConditions.First;
using BoundaryProblem.DataStructures.BoundaryConditions.Second;
using BoundaryProblem.DataStructures.BoundaryConditions.Third;
using BoundaryProblem.DataStructures.DensityFunction;
using BoundaryProblem.DataStructures;
using BoundaryProblem.Geometry;

namespace BoundaryProblem;

public class FiniteElementSolver
{
    public required double SolutionPrecision { get; init; }
    public required int MaxIteration { get; init; }

    private readonly ProblemFilePathsProvider _files;
    private readonly FirstBoundaryProvider _firstBoundary;
    private readonly SecondBoundaryProvider _secondBoundary;
    private readonly ThirdBoundaryProvider _thirdBoundary;
    private readonly Grid _grid;
    private readonly IDensityFunctionProvider _densityFunction;
    private readonly IMaterialProvider _materials;
    private readonly GlobalAssembler _globalAssembler;
    private ConjugateGradientSolver SLAESolver;

    public FiniteElementSolver(ProblemFilePathsProvider files)
    {
        _files = files;
            
        _grid = Grid.Deserialize(files);

        _firstBoundary = FirstBoundaryProvider.Deserialize(files);
        _secondBoundary = SecondBoundaryProvider.Deserialize(files);
        _thirdBoundary = ThirdBoundaryProvider.Deserialize(files);

        _densityFunction = FunctionDefinedOnNodes.Deserialize(files);
        _materials = MaterialProvider.Deserialize(files);

        _globalAssembler = new GlobalAssembler(_grid, _materials, _densityFunction);

    }

    public FiniteElementSolution Solve()
    {
        var equation = _globalAssembler.BuildEquation();

        var a = equation.Matrix;

        //for (int i = 0; i < a.Diagonal.Length; i++)
        //{
        //    for (int j = 0; j < a.Diagonal.Length; j++)
        //    {
        //        try
        //        {
        //            Console.Write($"{a[i, j]:F2}   ");
        //        }
        //        catch (IndexOutOfRangeException e)
        //        {
        //            Console.Write($"      ");
        //        }
        //    }
        //    Console.WriteLine();
        //}
        //Console.WriteLine();

        _globalAssembler.ApplySecondBoundaryConditions(equation, _secondBoundary)
            .ApplyThirdBoundaryConditions(equation, _thirdBoundary)
            .ApplyFirstBoundaryConditions(equation, _firstBoundary);

        

        IPreconditioner preconditioner = new DiagonalPreconditioner(equation.Matrix.Diagonal);
        SLAESolver = new ConjugateGradientSolver(preconditioner, SolutionPrecision, MaxIteration);

        var solution = SLAESolver.Solve(equation);

        return new FiniteElementSolution(_grid, solution);
    }
}