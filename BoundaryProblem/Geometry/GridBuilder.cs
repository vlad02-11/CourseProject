using BoundaryProblem.DataStructures;

namespace BoundaryProblem.Geometry
{
    public class GridBuilder
    {
        private readonly int _stepsInsideElement;
        private readonly Rectangle _area;
        private AxisSplitParameter _splitParameter;
        private Point2D _stepSizeForElements;
        private Point2D _stepSizeForNodes;

        public GridBuilder(Rectangle area)
        {
            _stepsInsideElement = Element.StepsInsideElement;
            _area = area;
        }

        public Grid Build(AxisSplitParameter splitParameter)
        {
            _splitParameter = splitParameter;
            _stepSizeForElements = new Point2D(
                CalcStep(_area.LeftBottom.X, _area.RightBottom.X, _splitParameter.XSteps),
                CalcStep(_area.LeftBottom.Y, _area.LeftTop.Y, _splitParameter.YSteps)
            );
            _stepSizeForNodes = new Point2D(
                CalcStep(_area.LeftBottom.X, _area.RightBottom.X, _splitParameter.XSteps * _stepsInsideElement),
                CalcStep(_area.LeftBottom.Y, _area.LeftTop.Y, _splitParameter.YSteps * _stepsInsideElement)
            );

            return new Grid(
                GenerateNodes,
                GenerateElements,
                _stepSizeForElements
                );
        }

        private static double CalcStep(double loweBound, double upperBound, int stepsCount)
        {
            return (upperBound - loweBound) / stepsCount;
        }

        private IEnumerable<Point2D> GenerateNodes
        {
            get
            {
                for (var i = 0; i <= _splitParameter.YSteps * _stepsInsideElement; i++)
                {
                    for (var j = 0; j <= _splitParameter.XSteps * _stepsInsideElement; j++)
                    {
                        var x = _area.LeftBottom.X + _stepSizeForNodes.X * j;
                        var y = _area.LeftBottom.Y + _stepSizeForNodes.Y * i;

                        yield return new Point2D(x, y);
                    }
                }
            }
        }

        private IEnumerable<Element> GenerateElements
        {
            get
            {
                var elementsPerXAxis = _splitParameter.XSteps;
                var nodesPerXAxis = elementsPerXAxis * _stepsInsideElement + 1;

                for (var i = 0; i < _splitParameter.YSteps; i++)
                {
                    for (var j = 0; j < _splitParameter.XSteps; j++)
                    {
                        var indexes = new List<int>();
                        var leftBottomStartIndex =
                            (i * _stepsInsideElement) * nodesPerXAxis +
                            _stepsInsideElement * j;
                        for (int k = 0; k <= _stepsInsideElement; k++)
                        {
                            for (int l = 0; l <= _stepsInsideElement; l++)
                            {
                                var globalIndex = leftBottomStartIndex + k * nodesPerXAxis + l;
                                indexes.Add(globalIndex);
                            }
                        }

                        yield return new Element(indexes.ToArray());
                    }
                }
            }
        }
    }
}
