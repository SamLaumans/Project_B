namespace Program;
using System.Diagnostics;

[TestClass]
public class MenuTest
{
    [TestMethod]
    public void TestQuit()
    {
        // Arrange
        FakeWorld world = new()
        {
            LinesToRead = new() { "498901" },
            IncludeLinesReadInLinesWritten = true,
            Files = new Dictionary<string, string>
            {
                ["DataSources/Tourslist.JSON"] = @"[
                {
                    ""ID"": ""5"",
                    ""Spots"": 11,
                    ""Started"": false,
                    ""Time"": ""2024-11-10T10:40:00"",
                    ""Customer_Codes"": []
                }]",
                ["DataSources/Customers.JSON"] = @"[
                {
                    ""CustomerCode"": ""1234567890""
                }]"
            }
        };
        Program.World = world;

        // Act
        Program.Main();
        Debug.WriteLine(world);

        // Assert
        string expected = "Toets het nummer in van de actie die u wilt uitvoeren:";
        List<string> output = world.LinesWritten;
        Assert.IsTrue(output.Contains(expected));
    }

    [TestMethod]
    public void TestSignInThenExit()
    {
        // Arrange
        FakeWorld world = new()
        {
            LinesToRead = new() { "1", "1234567890", "2", "q", "498901" },
            IncludeLinesReadInLinesWritten = true,
            Files = new Dictionary<string, string>
            {
                ["DataSources/Tourslist.JSON"] = @"[
                {
                    ""ID"": ""5"",
                    ""Spots"": 11,
                    ""Started"": false,
                    ""Time"": ""2024-11-10T10:40:00"",
                    ""Customer_Codes"": []
                }]",
                ["DataSources/Customers.JSON"] = @"[
                {
                    ""CustomerCode"": ""1234567890""
                }]"
            }
        };
        Program.World = world;

        // Act
        Program.Main();
        Debug.WriteLine(world);

        // Assert
        string expected = "Dit zijn de nog beschikbare rondleidingen voor vandaag(iedere rondleiding duurt 40 minuten):";
        List<string> output = world.LinesWritten;
        Assert.IsTrue(output.Contains(expected));
    }
}