using System.Diagnostics;
namespace Program
{
    [TestClass]
    public class CancelTest
    {
        [TestMethod]
        public void CancelMessageTest()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "1234567890", "2", "1", "1234567890", "498901" },
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
            string expected = "Reservering succesvol gecanceled.";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }

        [TestMethod]
        public void CancelTourslistJsonAdjusted()
        {
            // Arrange
            FakeWorld world = new()
            {
                IncludeLinesReadInLinesWritten = true,
                Now = new DateTime(2024, 11, 10, 10, 30, 0),
                Files = new Dictionary<string, string>
                {
                    ["DataSources/Tourslist.JSON"] = @"[
                    {
                        ""ID"": ""1"",
                        ""Spots"": 11,
                        ""Started"": false,
                        ""Time"": ""2024-11-10T10:40:00"",
                        ""Customer_Codes"": [""1234567890""]
                    }]",
                    ["DataSources/Customers.JSON"] = @"[
                    {
                    }]"
                },
            };

            Program.World = world;
            string customer = "1234567890";

            // Act
            Cancel.CancelAppointment(customer);
            Debug.WriteLine(world);

            // Assert
            string JSON1 = world.Files["DataSources/Tourslist.JSON"];
            Assert.IsFalse(JSON1.Contains(customer));
        }
        [TestMethod]
        public void CancelCustomerJsonAdjusted()
        {
            // Arrange
            FakeWorld world = new()
            {
                IncludeLinesReadInLinesWritten = true,
                Now = new DateTime(2024, 11, 10, 10, 30, 0),
                Files = new Dictionary<string, string>
                {
                    ["DataSources/Tourslist.JSON"] = @"[
                    {
                        ""ID"": ""1"",
                        ""Spots"": 11,
                        ""Started"": false,
                        ""Time"": ""2024-11-10T10:40:00"",
                        ""Customer_Codes"": [""1234567890""]
                    }]",
                    ["DataSources/Customers.JSON"] = @"[
                    {
                    }]"
                },
            };

            Program.World = world;
            string customer = "1234567890";

            // Act
            Cancel.CancelAppointment(customer);
            Debug.WriteLine(world);

            // Assert
            string JSON = world.Files["DataSources/Customer.JSON"];
            Assert.IsTrue(JSON.Contains(customer));
        }
    }
}