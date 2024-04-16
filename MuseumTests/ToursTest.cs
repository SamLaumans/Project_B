namespace Program;

[TestClass]
public class UnitTestTours
{
    [TestMethod]
    public void AddToTour_Returnsbooltrue()
    {
        // Arange
        string tourid = "1";
        Customer customer = new Customer("1234567890");
        // Act
        bool result = Tours.AddToTour(customer.CustomerCode, tourid);
        // Assert
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void AddToTour_Returnsboolfalse()
    {
        // Arange
        string tourid = "-1";
        Customer customer = new Customer("1234567890");
        // Act
        bool result = Tours.AddToTour(customer.CustomerCode, tourid);
        // Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void AddToTour_Returnsboolfalse2()
    {
        // Arange
        string tourid = "a";
        Customer customer = new Customer("1234567890");
        // Act
        bool result = Tours.AddToTour(customer.CustomerCode, tourid);
        // Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void AddToTour_Returnsboolfalse3()
    {
        // Arange
        string tourid = "";
        Customer customer = new Customer("1234567890");
        // Act
        bool result = Tours.AddToTour(customer.CustomerCode, tourid);
        // Assert
        Assert.AreEqual(false, result);
    }
}