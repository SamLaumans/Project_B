namespace Program;

[TestClass]
public class UnitTestTours
{
    [DataTestMethod]
    [DataRow("1", true)]
    [DataRow("2", true)]
    [DataRow("5000000", false)]
    [DataRow("a", false)]
    [DataRow("", false)]
    public void AddToTour_Returnsbooltrue(string tourid, bool expected)
    {
        // Arange
        Program program = new();
        List<Customer> Listofcustomercodes = new List<Customer>();
        Customer customer1 = new Customer("1234567890");
        Customer customer2 = new Customer("0987654321");
        Listofcustomercodes.Add(customer1);
        Listofcustomercodes.Add(customer2);
        // Act
        bool result = Tours.AddToTour(Listofcustomercodes, tourid);
        // Assert
        Assert.AreEqual(result, expected);
    }

    public void AddToTour_Returnsbool(string tourid, bool expected)
    {
        // Arange
        Program program = new();
        List<Customer> Listofcustomercodes = new List<Customer>();
        Customer customer1 = new Customer("1234567890");
        Customer customer2 = new Customer("09876");
        Listofcustomercodes.Add(customer1);
        Listofcustomercodes.Add(customer2);
        // Act
        bool result = Tours.AddToTour(Listofcustomercodes, tourid);
        // Assert
        Assert.AreEqual(result, expected);
    }
}
