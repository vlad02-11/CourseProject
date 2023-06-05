using BoundaryProblem.Calculus.Equation.DataStructures;
using BoundaryProblem.Calculus.Equation.DataStructures.LocalObjects;
using BoundaryProblem.DataStructures;

namespace BoundaryProblem.Calculus.Equation.Assembling;

public class LocalMatrixAssembler : LocalAssembler
{
    private readonly IMaterialProvider _materialProvider;
    private readonly Matrix _xStiffnessTemplate;
    private readonly Matrix _yStiffnessTemplate;

    public LocalMatrixAssembler(
        IMaterialProvider materialProvider,
        Matrix xMassTemplate, Matrix yMassTemplate,
        Matrix xStiffnessTemplate, Matrix yStiffnessTemplate
    ) : base(xMassTemplate, yMassTemplate)
    {
        _materialProvider = materialProvider;
        _xStiffnessTemplate = xStiffnessTemplate;
        _yStiffnessTemplate = yStiffnessTemplate;
    }

    public LocalMatrix Assemble(Element element)
    {
        var material = _materialProvider.GetMaterialById(element.MaterialId);

        var (stiffness, masses) = GetMassesAndStiffnessMatrix(material);

        return AttachIndexes(element.NodeIndexes, stiffness + masses);
    }

    private static LocalMatrix AttachIndexes(int[] indexes, Matrix matrix)
    {
        return new LocalMatrix(
            matrix,
            new IndexPermutation(indexes)
        );
    }

    private (Matrix stiffness, Matrix masses) GetMassesAndStiffnessMatrix(Material material)
    {
        var stiffnessValues = new double[LocalSize, LocalSize];
        var masses = GetDefaultMasses();

        for (int i = 0; i < LocalSize; i++)
        for (int j = 0; j < LocalSize; j++)
            stiffnessValues[i, j] =
                material.Lambda * (
                    _xStiffnessTemplate[IndexFromX(i), IndexFromX(j)] *
                    YMassTemplate[IndexFromY(i), IndexFromY(j)]
                    +
                    XMassTemplate[IndexFromX(i), IndexFromX(j)] *
                    _yStiffnessTemplate[IndexFromY(i), IndexFromY(j)]
                );

        return (new Matrix(stiffnessValues), masses * material.Gamma);
    }
}