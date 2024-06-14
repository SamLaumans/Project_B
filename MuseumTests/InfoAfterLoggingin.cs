using System.Diagnostics;
namespace Program
{
    [TestClass]
    public class TestInfoAfterLoggingIn
    {
        [TestMethod]
        public void TestInfoAfterLoggingIn1()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "1234567890", "3", "", "1234567890", "498901" },
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
                        ""Customer_Codes"": [""1234567890""]
                    }]",
                    ["DataSources/Customers.JSON"] = @"[
                    {
                        ""CustomerCode"": ""0987654321""
                    }]"
                }

            };
            Program.World = world;

            // Act
            Program.Main();
            Debug.WriteLine(world);

            // Assert
            string expected = "10-11-2024 10:40:00";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }
    }

}