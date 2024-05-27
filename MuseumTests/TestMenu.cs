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
            LinesToRead = new() { "498901" }
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
    public void TestSignInThenExit()
    {
        // Arrange
        FakeWorld world = new()
        {
            LinesToRead = new() { "1" , "1234567890" , "2", "q" , "498901" }
        };
        Program.World = world;

        // Act
        Program.Main();

        // Assert
        string expected = "Dit zijn de nog beschikbare rondleidingen voor vandaag(iedere rondleiding duurt 40 minuten):";
        List<string> output = world.LinesWritten;
        Assert.IsTrue(output.Contains(expected));
    }
}
