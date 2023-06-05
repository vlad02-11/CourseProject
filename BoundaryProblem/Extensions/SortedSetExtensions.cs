namespace BoundaryProblem.Extensions
{
    public static class SortedSetExtensions
    {
        public static void AddRange<T>(this SortedSet<T> sortedSet, IEnumerable<T> values)
        {
            foreach (var value in values)
                sortedSet.Add(value);
        }
    }
}
