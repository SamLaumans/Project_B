namespace Program;

[TestClass]
public class MenuTest
{
    [TestMethod]
    public void TestQuit()
    {
        // Arrange
        FakeWorld world = new()
        {
            LinesToRead = new() { "4@8B" }
        };
        Program.World = world;

        // Act
        Program.Main();

        // Assert
        string expected = "Wat wilt u doen?";
        List<string> output = world.LinesWritten;
        Assert.IsTrue(output.Contains(expected));
    }

    [TestMethod]
    public void Test()
    {
        // Arrange
        FakeWorld world = new()
        {
            LinesToRead = new() { "q" }
        };
        Program.World = world;

        // Act
        Program.Main();

        // Assert
        string expected = "Wat wilt u doen?";
        List<string> output = world.LinesWritten;
        Assert.IsTrue(output.Contains(expected));
    }
}