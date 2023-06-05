using BoundaryProblem.Calculus.Equation.DataStructures;

namespace BoundaryProblem.Loggining;

public class Logger
{
    public Logger LogVector(Vector v)
    {
        for (int i = 0; i < v.Length; i++)
            Console.WriteLine($"{i}: {v[i]}");

        return this;
    }

    public Logger LogMatrix(Matrix a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < a.Length; j++)
            {
                Console.Write($"{a[i, j]:F4}");
            }
            Console.WriteLine();
        }

        return this;
    }
}