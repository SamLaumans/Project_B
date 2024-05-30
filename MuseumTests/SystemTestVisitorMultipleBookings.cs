using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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
                LinesToRead = new() { "1", "1234567890", "2", "5", "1", "1234567890", "q", "498901" }
            };
            Program.World = world;

            // Act
            Program.Main();

            // Assert
            string expected = "De door u ingevulde code was: '1234567890'. De code bestaat altijd uit 10 cijfers.";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }

        [TestMethod]
        public void IsTourlistJsonAdjusted()
        {
            // Arrange
            FakeWorld world = new()
            {
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
                LinesToRead = new() { "1", "1234567890", "1", "0123456789", "2", "5", "498901" }
            };
            Program.World = world;

            // Act
            Program.Main();

            // Assert
            string expected = "U heeft voor 2 gereserveerd voor de rondleiding om 10-11-2024 12:00:00";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }

        [TestMethod]
        public void BookingATourWithMultiplePeopleTest2()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "1", "1234567890", "1", "0123456789", "2", "5", "498901" }
            };
            Program.World = world;

            // Act
            Program.Main();

            // Assert
            string expected = "Deze klantcodes zijn op dit moment aangemeld: 1234567890, 0123456789,";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }

        [TestMethod]
        public void TestInfoAfterLoggingIn()
        {
            // Arrange
            FakeWorld world = new()
            {
                LinesToRead = new() { "3", "1234567890", "2", "5", "1", "1234567890", "q", "498901" }
            };
            Program.World = world;

            // Act
            Program.Main();

            // Assert
            string expected = "De door u ingevulde code was: '1234567890'. De code bestaat altijd uit 10 cijfers.";
            List<string> output = world.LinesWritten;
            Assert.IsTrue(output.Contains(expected));
        }
    }
}
