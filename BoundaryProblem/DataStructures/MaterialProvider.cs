using BoundaryProblem.DataStructures.BoundaryConditions.First;
using BoundaryProblem.DataStructures.DensityFunction;

namespace BoundaryProblem.DataStructures
{
    public class MaterialProvider : IMaterialProvider
    {
        private readonly Material[] _materials;

        public MaterialProvider(Material[] materials)
        {
            _materials = materials;
        }

        public Material GetMaterialById(int id) => _materials[id];

        public static IMaterialProvider Deserialize(ProblemFilePathsProvider files)
        {
            using var stream = new StreamReader(files.Material);
            
            var materials = new Material[int.Parse(stream.ReadLine())];

            for (int i = 0; ; i++)
            {
                var line = stream.ReadLine();
                if (String.IsNullOrEmpty(line)) break;

                var values = line.Split(' ');
                materials[i] = 
                    new Material(
                        double.Parse(values[0]),
                        double.Parse(values[1])
                    );
            }

            return new MaterialProvider(materials);
        }
    }
}
