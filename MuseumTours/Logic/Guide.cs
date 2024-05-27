namespace Program
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Guide
    {
        private static List<Tours> listOfTours = DataAccess.ReadJsonTours();
        private static List<string> scannedCodes = new List<string>();
        public string EmployeeCode;

        public Guide(string employeecode)
        {
            EmployeeCode = employeecode;
        }

        public bool CheckIfGuideInList(string idguide)
        {
            List<Guide> listOfGuides = DataAccess.ReadJsonEmployees();
            return listOfGuides.Any(guide => guide.EmployeeCode == idguide);
        }

        public static void CheckEmployeeID()
        {
            string pattern = @"[^0-9]";
            bool answerValid = false;

            while (!answerValid)
            {
                Program.World.WriteLine("Scan de barcode op uw badge, toets het nummer onder de barcode in of toets 'q' om terug te gaan naar het begin.");
                string employeeID = Program.World.ReadLine();

                if (string.IsNullOrEmpty(employeeID))
                {
                    Program.World.WriteLine($"De door u ingevulde code was: '{employeeID}'. De code kan niet leeg zijn.");
                }
                else if (employeeID == "q")
                {
                    break;
                }
                else if (employeeID.Length != 6 || Regex.IsMatch(employeeID, pattern))
                {
                    Program.World.WriteLine($"De door u ingevulde code was: '{employeeID}'. De code bestaat altijd uit 6 cijfers.");
                }
                else
                {
                    Guide guide = new Guide(employeeID);
                    if (guide.CheckIfGuideInList(employeeID))
                    {
                        Tours.ShowToursToGuide("../../../Tourslist.Json");
                        answerValid = GuideChooseTour();
                    }
                    else
                    {
                        Program.World.WriteLine($"De door u ingevulde code was: '{employeeID}', Probeer alstublieft opnieuw.");
                    }
                }
            }
        }

        public static bool GuideChooseTour()
        {
            while (true)
            {
                Program.World.WriteLine("Voer het rondleidingsnummer in van de rondleiding die u wilt starten of toets [q] om terug te gaan.");
                string chosenTour = Program.World.ReadLine();

                if (string.IsNullOrEmpty(chosenTour))
                {
                    Program.World.WriteLine($"Uw invoer was '{chosenTour}'. Voer een geldig tournummer in.");
                }
                else if (chosenTour == "q")
                {
                    Program.Main();
                    return false;
                }
                else
                {
                    bool tourFound = false;

                    foreach (Tours tour in listOfTours)
                    {
                        if (tour.ID == chosenTour)
                        {
                            GuideChooseOption(chosenTour);
                            tourFound = true;
                            break;
                        }
                    }

                    if (!tourFound)
                    {
                        Program.World.WriteLine($"Het opgegeven rondleidingsnummer '{chosenTour}' is niet geldig. Voer een geldig tournummer in.");
                    }
                }
            }
        }

        public static void GuideChooseOption(string chosenTour)
        {
            bool answer = false;
            while (answer == false)
            {
                Program.World.WriteLine("Wat wilt u doen?\n[1] Bezoekers scannen \n[2] Bezoekers toevoegen\n[3] Codes van Bezoekers in deze rondleiding bekijken\n[4] Terug gaan");
                string choice = Program.World.ReadLine();
                switch (choice)
                {
                    case "1":
                    GuideScanLogic(chosenTour);
                    break;
                    case "2":
                    Addingcustomerstotour(chosenTour);
                    break;
                    case "3":
                    Tours.ShowChosenTour(chosenTour);
                    break;
                    case "4":
                    Tours.ShowToursToGuide("../../../Tourslist.Json");
                    answer = true;
                    break;
                }
            }
        }
        public static bool GuideScanLogic(string chosenTour)
        {
            bool allCodesScanned = false;
                    while (!allCodesScanned)
                    {
                        allCodesScanned = true;
                        foreach (Tours tour in listOfTours)
                        {
                            if (tour.ID == chosenTour)
                            {
                                foreach (Customer customer in tour.Customer_Codes)
                                {
                                    if (!scannedCodes.Contains(customer.CustomerCode))
                                    {
                                        allCodesScanned = false;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (!allCodesScanned)
                        {
                            Program.World.WriteLine("Scan de Customer code die u wilt scannen of toets (q) om terug te gaan:");
                            string customerCode = Program.World.ReadLine();

                            GuideScanCustomerCode(chosenTour, customerCode);
                            ShowCodesNotScanned(chosenTour);
                        }
                    }
                    Program.World.WriteLine("Alle Bezoekers zijn succesvol gescand");
                    return true;
        }
        public static void GuideScanCustomerCode(string tourID, string customerCode)
        {
            foreach (Tours tour in listOfTours)
            {
                if (tour.ID == tourID)
                {
                    if (customerCode == "q")
                    {
                        Tours.ShowChosenTour(tourID);
                        GuideChooseOption(tourID);
                    }
                    else if (!tour.Customer_Codes.Any(customer => customer.CustomerCode == customerCode))
                    {
                        Program.World.WriteLine($"Customer {customerCode} is niet gevonden in tour {tourID}");
                    }
                    else if (scannedCodes.Contains(customerCode))
                    {
                        Program.World.WriteLine($"Deze customer is al gescand.");
                    }
                    else
                    {
                        scannedCodes.Add(customerCode);
                        Program.World.WriteLine($"Customer code {customerCode} is succesvol gescand.");
                        return;
                    }
                }
            }
        }

        public static void ShowCodesNotScanned(string tourID)
        {
            foreach (Tours tour in listOfTours)
            {
                if (tour.ID == tourID)
                {
                    Program.World.WriteLine("Codes die nog niet gescand zijn in deze rondleiding:");
                    Program.World.WriteLine("================================================================");
                    foreach (Customer customer in tour.Customer_Codes)
                    {
                        if (!scannedCodes.Contains(customer.CustomerCode))
                        {
                            Program.World.WriteLine($"{customer.CustomerCode}");
                        }
                    }
                    Program.World.WriteLine("================================================================");
                }
            }
        }

        public static void Addingcustomerstotour(string ChosenTour)
        {
            bool answerValid = false;
            while (!answerValid)
            {
                if (ChosenTour == "q")
                {
                    break;
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
                        while (!answer)
                        {
                            bool booleanstuff = true;
                            Program.World.WriteLine("Scan de streepjescode op uw ticket of toets [q] om terug te gaan");
                            string Customer_ID = Program.World.ReadLine().ToLower();
                            if (Customer_ID == "q")
                            {
                                break;
                            }
                            if (tour.Customer_Codes.Any(customer => customer.CustomerCode == Customer_ID))
                            {
                                Program.World.WriteLine("Deze klant is al ingeschreven voor deze rondleiding.");
                            }
                            if (listofaddablecustomers.Any(customer => customer.CustomerCode == Customer_ID))
                            {
                                booleanstuff = false;
                            }
                            if (!booleanstuff)
                            {
                                Program.World.WriteLine($"Deze klant {Customer_ID} is al aangemeld.");
                            }
                            else if (Customer.CheckIfCustomerInList(Customer_ID))
                            {
                                AmountOfPeople += 1;
                                listofaddablecustomers.Add(new Customer(Customer_ID));
                                scannedCodes.Add(Customer_ID);
                                bool answer2 = false;
                                while (!answer2)
                                {
                                    Program.World.Write("Deze klantcodes zijn op dit moment aangemeld: ");
                                    foreach (Customer customer in listofaddablecustomers)
                                    {
                                        Program.World.Write(customer.CustomerCode + ", ");
                                    }
                                    Program.World.WriteLine("\nWilt u nog iemand aanmelden? Ja[1] nee[2]");
                                    string yesno = Program.World.ReadLine();
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
    }
}
