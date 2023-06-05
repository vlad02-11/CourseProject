namespace BoundaryProblem.DataStructures.BoundaryConditions.Third;

public record FlowExchangeUnit(int ElementId, Bound Bound, double Betta, double[] Environment);