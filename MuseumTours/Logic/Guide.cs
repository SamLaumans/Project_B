namespace Program;

using System;
using System.Text.RegularExpressions;

public class Guide
{
    public string EmployeeCode;
    public Guide(string employeecode)
    {
        EmployeeCode = employeecode;
    }
    public bool CheckIfGuideInList(string idguide)
    {
        List<Guide> listOfGuides = DataAccess.ReadJsonEmployees();

        foreach (Guide guide in listOfGuides)
        {
            if (guide.EmployeeCode == idguide)
            {
                return true;
            }
        }
        return false;
    }
    public static void CheckEmployeeID()
    {
        string pattern = @"[^0-9]";
        bool Answer = false;
        while (Answer == false)
        {
            Console.WriteLine("Scan de barcode op uw badge of toets 'q' om terug te gaan naar het begin.");
            string Employee_ID = Console.ReadLine();
            if (Employee_ID == null)
            {
                Console.WriteLine($"De door u ingevulde code was: '{Employee_ID}'. De code kan niet leeg zijn.");
            }
            else if (Employee_ID == "q")
            {
                Program.Main();
            }
            else if (Employee_ID.Count() != 6)
            {
                Console.WriteLine($"De door u ingevulde code was: '{Employee_ID}'. De code bestaat altijd uit 6 cijfers.");
            }
            else if (Regex.IsMatch(Employee_ID, pattern))
            {
                Console.WriteLine($"De door u ingevulde code was: '{Employee_ID}'. De code bestaat altijd uit 6 cijfers.");
            }
            else
            {
                Guide guide = new Guide(Employee_ID);
                if (guide.CheckIfGuideInList(Employee_ID))
                {
                    bool answer = false;
                    while (answer == false)
                    {
                        Console.WriteLine("Wat wilt u doen?\n[1] Rondleidingen en info zien\n[2] Mensen toevoegen aan rondleidingen\n[3] Terug naar het beginscherm en uitloggen");
                        string choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "1":
                                answer = true;
                                Showguidetours();
                                break;
                            case "2":
                                answer = true;
                                Addingcustomerstotour();
                                break;
                            case "3":
                                answer = true;
                                Program.Main();
                                break;
                            default:
                                answer = false;
                                Console.WriteLine("We hebben u niet begrepen.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"De door u ingevulde code was: '{Employee_ID}', Probeer alstublieft opnieuw.");
                }
            }
        }
    }
    public static void Addingcustomerstotour()
    {
    bool answerValid = false;
    while (answerValid == false)
    {
    Console.WriteLine("Voer het rondleidingsnummer waaraan u deelnemers zou willen toevoegen in of toets 'q' om terug te gaan naar het begin.");
    string ChosenTour = Console.ReadLine().ToLower();
    if (ChosenTour == "q")
    {
        Program.Main();
    }
    else
    {
        if (Tours.checkiftourvalid(ChosenTour) != null)
        {
            Tours tour = Tours.checkiftourvalid(ChosenTour);
            answerValid = true;
            List<Customer> listofaddablecustomers = new List<Customer>();
            bool answer = false;
            int AmountOfPeople = 0;
            while (answer == false)
            {
                bool booleanstuff = true;
                Console.WriteLine("Scan de streepjescode op uw ticket of toets [q] om terug te gaan naar het begin.");
                string Customer_ID = Console.ReadLine().ToLower();
                foreach (Customer customer in listofaddablecustomers)
                {
                    if (customer.CustomerCode == Customer_ID)
                    {
                    booleanstuff = false;
                    }
                }
                if (booleanstuff == false)
                {
                    Console.WriteLine($"Deze klant {Customer_ID} is al aangemeld.");
                    answer = false;
                }
                else if (Customer.CheckIfCustomerInList(Customer_ID) == true)
                {
                    AmountOfPeople += 1;
                    listofaddablecustomers.Add(new Customer(Customer_ID));
                    bool answer2 = false;
                    while (answer2 == false)
                    {
                        Console.Write("Deze klantcodes zijn op dit moment aangemeld: ");
                        foreach (Customer customer in listofaddablecustomers)
                        {
                            Console.Write(customer.CustomerCode + ", ");
                        }
                        Console.WriteLine("\nWilt u nog iemand aanmelden? Ja[1] nee[2]");
                        string yesno = Console.ReadLine();
                        switch (yesno)
                        {
                            case "1":
                                answer2 = true;
                                answer = false;
                                break;
                            case "2":
                                answer2 = true;
                                answer = true;
                                Tours.AddToTour(listofaddablecustomers, tour.ID);
                                break;
                        }
                    }
                }
            }
        }
    }
    }
    }
    public static void Showguidetours()
    {
        Tours.ShowToursToGuide("../../../Tourslist.Json");
        bool answerValid = false;
        while (answerValid == false)
        {
            Console.WriteLine("Voer het rondleidingsnummer in waarvan u de deelnemers wilt zien, als u mensen toe wilt voegen aan rondleidingen toets of toets 'q' om terug te gaan naar het begin.");
            string ChosenTour = Console.ReadLine();
            if (ChosenTour == "q")
            {
                Program.Main();
            }
            else
            {
                Tours.ShowChosenTour(ChosenTour);
                bool ChosenOption = false;
                while (ChosenOption == false)
                {
                    Console.WriteLine("Om terug te gaan naar het overzicht van tours toets (b) om terug te gaan naar het beginscherm toets (q)");
                    string GuideChoice = Console.ReadLine();
                    if (GuideChoice == null)
                    {
                        Console.WriteLine("Voer een geldige optie in.");
                    }
                    else if (GuideChoice == "b")
                    {
                        Tours.ShowToursToGuide("../../../Tourslist.Json");
                        break;
                    }
                    else if (GuideChoice == "q")
                    {
                        Program.Main();
                    }
                    else
                    {
                        Console.WriteLine($"De door u ingevulde code was: '{GuideChoice}', Probeer alstublieft opnieuw.");
                    }
                }
            }
        }
    }
}