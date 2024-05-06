using Newtonsoft.Json;

namespace Program;

[TestClass]
public class TestCustomer
{
    [DataTestMethod]
    [DataRow("1337")]
    [DataRow("1234567890")]
    [DataRow("0987654321")]
    [DataRow("sample customer code")]
    public void TestCustomerCode(string actualCustomerCode)
    {
        // Arrange
        Customer customer = new(actualCustomerCode);

        // Act

        // Assert
        Assert.AreEqual(customer.CustomerCode, actualCustomerCode);

    }

    [DataTestMethod]
    [DataRow("1337", false)]
    [DataRow("874743983983434789347984978349783479834", false)]
    [DataRow("1357924680", true)]
    [DataRow("0987654321", true)]
    public void TestCheckIfCustomerInList(string CustomerCode, bool ExpectedOutput)
    {
        // Arrange
        Customer customer = new(CustomerCode);

        // Act
        bool IsCustomerInList = Customer.CheckIfCustomerInList(customer.CustomerCode);

        // Assert
        Assert.AreEqual(IsCustomerInList, ExpectedOutput);

    }
}