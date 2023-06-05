namespace BoundaryProblem.DataStructures.BoundaryConditions.Second;

public record SecondBoundaryProvider(params FlowUnit[] FlowConditions)
{
    public static SecondBoundaryProvider Deserialize(ProblemFilePathsProvider filesProvider)
    {
        using var stream = new StreamReader(filesProvider.SecondBoundary);
        List<FlowUnit> units = new();

        while (true)
        {
            var line = stream.ReadLine();
            if (String.IsNullOrEmpty(line)) break;

            var values = line.Split(' ');
            if (Enum.GetValues<Bound>()
                .Select(x => (int)x)
                .All(x => x != int.Parse(values[1]))
               )
            {
                throw new FormatException(
                    "Third boundary file contains wrong bound index.\n" +
                    $"String was: \"{line}\""
                );
            }

            units.Add(
                new FlowUnit(
                    ElementId: int.Parse(values[0]),
                    Bound: (Bound)int.Parse(values[1]),
                    Thetta: new []
                    {
                        double.Parse(values[2]),
                        double.Parse(values[3]),
                        double.Parse(values[4]),
                        double.Parse(values[5])
                    }
                ));
        }

        return new SecondBoundaryProvider(units.ToArray());
    }
}