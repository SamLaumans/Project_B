using System.Diagnostics;

namespace Program
{
    [TestClass]
    public class SystemTestVisitorMultipleBookings
    {
        [TestMethod]
        public void ErrorMessageTest()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "1", "1234567890", "2", "5", "1", "1234567890", "q", "498901" },
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
            string expected = "Het door u ingevulde klantnummers was: '1234567890'. De code bestaat altijd uit 10 cijfers.";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }

        [TestMethod]
        public void IsTourlistJsonAdjusted()
        {
            // Arrange
            FakeWorld world = new()
            {
                IncludeLinesReadInLinesWritten = true,
                Files = new Dictionary<string, string>
                {
                    ["DataSources/Tourslist.JSON"] = @"[
                    {
                        ""ID"": ""1"",
                        ""Spots"": 11,
                        ""Started"": false,
                        ""Time"": ""2024-11-10T10:40:00"",
                        ""Customer_Codes"": []
                    }]",
                    ["DataSources/Customers.JSON"] = @"[
                    {
                        ""CustomerCode"": ""1234567890""
                    }]"
                },
            };

            Program.World = world;
            Customer customer = new Customer("1234567890");
            List<Customer> customerList = new List<Customer> { customer };

            // Act
            Tours.AddToTour(customerList, "1");
            Debug.WriteLine(world);

            // Assert
            string JSON1 = world.Files["DataSources/Tourslist.JSON"];
            Assert.IsTrue(JSON1.Contains(customer.CustomerCode));
        }

        [TestMethod]
        public void IsCustomerJsonAdjusted()
        {
            // Arrange
            FakeWorld world = new()
            {
                IncludeLinesReadInLinesWritten = true,
                Files = new Dictionary<string, string>
                {
                    ["DataSources/Tourslist.JSON"] = @"[
                    {
                        ""ID"": ""1"",
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
            Customer customer = new Customer("1234567890");
            List<Customer> customerList = new List<Customer> { customer };

            // Act
            Tours.AddToTour(customerList, "1");
            Debug.WriteLine(world);

            // Assert
            string JSON = world.Files["DataSources/Customers.JSON"];
            Assert.IsFalse(JSON.Contains(customer.CustomerCode));
        }

        [TestMethod]
        public void BookingATourWithMultiplePeopleTest1()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "1", "1234567890", "1", "0123456789", "2", "5", "498901" },
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
                        ""CustomerCode"": ""0123456789""
                    },
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
            string expected = "U heeft voor 2 personen gereserveerd voor de rondleiding om 10-11-2024 10:40:00";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }

        [TestMethod]
        public void BookingATourWithMultiplePeopleTest2()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "1", "1234567890", "1", "0123456789", "2", "5", "498901" },
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
                        ""CustomerCode"": ""0123456789""
                    },
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
            string expected = "1234567890, ";
            string expected1 = "0123456789, ";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
            Assert.IsTrue(output.Contains(expected1));
        }

        [TestMethod]
        public void TestInfoAfterLoggingIn()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "3", "1234567890", "2", "5", "1", "1234567890", "q", "498901" },
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
            string expected = "Het door u ingevulde klantnummer was: '1234567890'. De code bestaat altijd uit 10 cijfers.";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }
    }
}

