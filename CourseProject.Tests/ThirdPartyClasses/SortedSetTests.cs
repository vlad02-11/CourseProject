namespace CourseProject.Tests.ThirdPartyClasses;

public class SortedSetTests
{
    private SortedSet<int> _list;

    [SetUp]
    public void Setup()
    {
        _list = new SortedSet<int>();
    }

    [Test]
    public void Test1()
    {
        _list.Add(6);
        _list.Add(2);
        _list.Add(15);
        _list.Add(4);
        _list.Add(5);
        _list.Add(2222);

        var x = _list.ToList();

        Assert.That(x[1], Is.EqualTo(4));
    }
}