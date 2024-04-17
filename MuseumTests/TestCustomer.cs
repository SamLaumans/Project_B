using Newtonsoft.Json;

namespace Program;

[TestClass]
public class TestCustomer
{
    [DataTestMethod]
    [DataRow("1337")]
    [DataRow("1234567890")]
    [DataRow("0987654321")]
    [DataRow("customer code lol")]
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
    public void TestCheckIfCustomerInList(string CustomerCode, bool expectedOutcome)
    {
        // Arrange
        Customer customer = new(CustomerCode);
        string toWriteToJson = $"Customer :{customer.CustomerCode}";

        // Act
        string filepath = "Customers.Json";
        string updatedJson = JsonConvert.SerializeObject(toWriteToJson, Formatting.Indented);
        File.WriteAllText(filepath, updatedJson);
        
        // Assert
        Assert.AreEqual(customer.CustomerCode, expectedOutcome);

    }
}