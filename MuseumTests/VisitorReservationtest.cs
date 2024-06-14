namespace Program;
using System.Diagnostics;

[TestClass]
public class MenuTest
{
    [TestMethod]
    public void TestVisitorReservation()
    {
        // Arrange
        FakeWorld world = new()
        {
            LinesToRead = new() { "1234567890", "1", "2", "5", "1234567890", "498901" },
            IncludeLinesReadInLinesWritten = true,
            Now = new DateTime(2024, 11, 10, 10, 30, 0),
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
        string expected = "U heeft voor 1 persoon gereserveerd voor de rondleiding om 10:40";
        List<string> output = world.LinesWritten;
        Assert.IsTrue(output.Contains(expected));
    }

    [TestMethod]
    public void ScanBarcodetest()
    {
        // Arrange
        FakeWorld world = new()
        {
            LinesToRead = new() { "1234567890", "498901" },
            IncludeLinesReadInLinesWritten = true,
            Now = new DateTime(2024, 11, 10, 10, 30, 0),
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
        string expected = "Dit zijn de eerst komende 5 rondleidingen: ";
        List<string> output = world.LinesWritten;
        Assert.IsTrue(output.Contains(expected));
    }
}