using System.Diagnostics;
namespace Program
{
    [TestClass]
    public class GuideAddCustomersTest
    {
        [TestMethod]
        public void GuideAddCustomersTest1()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "gids", "1234-5678", "5", "1234567890", "0987654321", "2", "1", "1234567890", "498901" },
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
            string expected = "U heeft voor 1 persoon gereserveerd voor de rondleiding om 10:40";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }
    }

}