namespace Program;

[TestClass]
public class MenuTest
{
    [TestMethod]
    public void Test()
    {
        // Arrange
        FakeWorld world = new()
        {
            LinesToRead = new() { "1" }
        };
        TestableMenu menu = new(world);

        // Act
        menu.MainProgram();

        // Assert
        string expected = "\nWat wilt u doen?\n[1] Rondleiding reserveren \n[2] Rondleiding annuleren \n[3] Info rondleidingen\n[4] Inloggen werknemers";
        string actual = world.LinesWritten.Last();
        Assert.AreEqual(expected, actual);
    }
}