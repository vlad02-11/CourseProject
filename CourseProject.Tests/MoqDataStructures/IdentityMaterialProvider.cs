using BoundaryProblem.DataStructures;

namespace CourseProject.Tests.MoqDataStructures;

internal class IdentityMaterialProvider : IMaterialProvider
{
    public Material GetMaterialById(int id)
    {
        return new Material(1, 1);
    }
}