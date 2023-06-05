namespace BoundaryProblem.DataStructures.DensityFunction;

public class FunctionDefinedOnNodes : IDensityFunctionProvider
{
    private readonly Dictionary<int, double> _values;

    public FunctionDefinedOnNodes(Dictionary<int, double> values)
    {
        _values = values;
    }

    public static IDensityFunctionProvider Deserialize(ProblemFilePathsProvider files)
    {
        using var stream = new StreamReader(files.DensityFunction);
        var functionValues = new Dictionary<int, double>();

        while (true)
        {
            var line = stream.ReadLine();
            if (String.IsNullOrEmpty(line)) break;

            var values = line.Split(' ');
            functionValues.Add(
                int.Parse(values[0]),
                double.Parse(values[1])
            );
        }

        return new FunctionDefinedOnNodes(functionValues);
    }

    public double Calc(int globalNodeIndex)
    {
        if (_values.TryGetValue(globalNodeIndex, out var value))
            return value;

        throw new ArgumentOutOfRangeException($"There no node with index {globalNodeIndex}");
    }
}