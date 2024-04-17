namespace Program;

[TestClass]
public class TestProgram
{
    [DataTestMethod]
    [DataRow("1234567890", "1", true)]
    [DataRow("1234", "2", false)]
    [DataRow("1234", "5000000", false)]
    public void AddToTourTest(string customerid, string tourid, bool correctOutcome)
    {
        //Arrange

        // Act
        bool outcome = Tours.AddToTour(customerid, tourid);

        // Assert
        Assert.AreEqual(outcome, correctOutcome);
        

    }
}