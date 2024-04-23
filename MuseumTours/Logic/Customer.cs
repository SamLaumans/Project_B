using Newtonsoft.Json;
using Program;
public class Customer
{
    public string CustomerCode;

    public Customer(string customercode)
    {
        CustomerCode = customercode;
    }
    public static bool CheckIfCustomerInList(string idcustomer)
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

    public static void InputMoreCustomercodes()
    {
        List<Customer> listofaddablecustomers = new List<Customer>();
        bool answer = false;
        while (answer == false)
        {
            Console.WriteLine("Scan de barcode op uw ticket, toets het nummer onder de barcode in of toets 'q' om terug te gaan naar het begin.");
            string Customer_ID = Console.ReadLine();
            if (CheckIfCustomerInList(Customer_ID) == true)
            {
                // Customer customer = new Customer(Customer_ID);
                listofaddablecustomers.Add(new Customer(Customer_ID));
                Console.WriteLine("Bent u met meerdere mensen en wilt u nog iemand aanmelden? Ja(1) nee(2)");
                string yesno = Console.ReadLine();
                bool answer2 = false;
                while (answer2 == false)
                {
                    switch (yesno)
                    {
                        case "1":
                            answer2 = true;
                            answer = false;
                            break;
                        case "2":
                            answer2 = true;
                            answer = true;
                            Tours.ShowAvailableTours(1);
                            bool answerValid = false;
                            while (answerValid == false)
                            {
                                Console.WriteLine("Voer het rondleidingsnummer waaraan u zou willen deelnemen in of toets 'q' om terug te gaan naar het begin.");
                                string ChosenTour = Console.ReadLine();
                                if (ChosenTour == "q")
                                {
                                }
                                else
                                {
                                    answerValid = Tours.AddToTour(listofaddablecustomers, ChosenTour);
                                    answerValid = true;
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met '1' ja of '2' nee.");
                            answer2 = false;
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine($"De door u ingevulde code was: '{Customer_ID}'. De code bestaat altijd uit 10 cijfers.");
                answer = false;
            }
        }
    }
}