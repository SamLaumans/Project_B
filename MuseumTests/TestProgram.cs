/*namespace Program;

[TestClass]
public class TestProgram
{
    [DataTestMethod]
    [DataRow("1", true)]
    [DataRow("2", true)]
    [DataRow("5000000", false)]
    public void AddToTourTest(string tourid, bool correctOutcome)
    {
        // Arrange
        Program program = new();
        List<Customer> Listofcustomercodes = new List<Customer>();
        Customer customer1 = new Customer("1234567890");
        Customer customer2 = new Customer("0987654321");
        Listofcustomercodes.Add(customer1);
        Listofcustomercodes.Add(customer2);

        // Act
        bool outcome = Tours.AddToTour(Listofcustomercodes, tourid);

        // Assert
        Assert.AreEqual(outcome, correctOutcome);

    }

    [DataTestMethod]
    [DataRow("1", false)]
    [DataRow("2", false)]
    [DataRow("5000000", false)]
    public void AddToTourTest2(string tourid, bool correctOutcome)
    {
        // Arrange
        Program program = new();
        List<Customer> Listofcustomercodes = new List<Customer>();
        Customer customer1 = new Customer("12345");
        Customer customer2 = new Customer("09876");
        Listofcustomercodes.Add(customer1);
        Listofcustomercodes.Add(customer2);

        // Act
        bool outcome = Tours.AddToTour(Listofcustomercodes, tourid);

        // Assert
        Assert.AreEqual(outcome, correctOutcome);
    }
}*/
