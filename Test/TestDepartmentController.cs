namespace Project3.Controllers;

public class TestDepartmentController
{
    [SetUp]
    public void Setup()
    {}

    [Test]
    public void TestAddDepartment()
    {
        DepartmentController deptControl = new DepartmentController();
        Department dept = new Department(1, "grocery");

        deptControl.Add(dept);

        Assert.AreEqual(deptControl.GetList().Last(), dept);
    }
}