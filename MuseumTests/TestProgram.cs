namespace Program;

[TestClass]
public class TestProgram
{
    [DataTestMethod]
    [DataRow("1234567890", "1", true)]
    [DataRow("1234567890", "500", false)]
    public void AddToTourInvalidID(string customerid, string tourid, bool correctOutcome)
    {
        // Arrange
        Program program = new();

        // Act
        bool outcome = program.AddToTour(customerid, tourid);

        // Assert
        Assert.AreEqual(outcome, correctOutcome);

    }
}