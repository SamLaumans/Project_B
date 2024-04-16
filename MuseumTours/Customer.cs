namespace Program;

using Newtonsoft.Json;
public class Customer
{
    public string CustomerCode;

    public Customer(string customercode)
    {
        CustomerCode = customercode;
    }
    public bool CheckIfCustomerInList(string idcustomer)
    {
        using StreamReader reader = new("Customers.Json");
        string File2Json = reader.ReadToEnd();
        List<Customer> listOfCustomers = JsonConvert.DeserializeObject<List<Customer>>(File2Json)!;

        foreach (Customer customer in listOfCustomers)
        {
            if (customer.CustomerCode == idcustomer)
            {
                return true;
            }
        }
        return false;
    }
}