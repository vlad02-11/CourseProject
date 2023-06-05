namespace BoundaryProblem.DataStructures.BoundaryConditions.First;

public record FirstBoundaryProvider(params ValueUnit[] ValueConditions)
{
    public static FirstBoundaryProvider Deserialize(ProblemFilePathsProvider filesProvider)
    {
        using var stream = new StreamReader(filesProvider.FirstBoundary);
        List<ValueUnit> units = new();

        while (true)
        {
            var line = stream.ReadLine();
            if (String.IsNullOrEmpty(line)) break;

            var values = line.Split(' ');
            units.Add(
                new ValueUnit(
                    NodeIndex: int.Parse(values[0]),
                    Value: double.Parse(values[1])
                ));
        }

        return new FirstBoundaryProvider(units.ToArray());
    }
}