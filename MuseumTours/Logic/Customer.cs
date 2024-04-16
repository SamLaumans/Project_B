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
        List<Customer> listOfCustomers = DataAccess.ReadJsonCustomers();

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