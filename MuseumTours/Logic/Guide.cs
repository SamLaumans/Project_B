namespace Program
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Media;
    using System.Runtime.Serialization;

    public class Guide
    {
        private static List<Tours> listOfTours;
        private static List<string> scannedCodes = new List<string>();
        private static List<Customer> listOfCustomers;
        public string EmployeeCode;

        public Guide(string employeecode)
        {
            EmployeeCode = employeecode;
        }

        public static void Init()
        {
            listOfTours = DataAccess.ReadJsonTours();
            listOfCustomers = DataAccess.ReadJsonCustomers();
        }

        static void GuideScanSound()
        {
            SoundPlayer sound = new SoundPlayer("Ding.wav");
            sound.PlaySync();
        }

        static void GuideAllScannedSound()
        {
            SoundPlayer sound = new SoundPlayer("Pokemon.wav");
            sound.PlaySync();
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
                Program.World.WriteLine("Scan de streepjescode op uw badge, toets het nummer onder de streepjescode in of toets 'q' om terug te gaan naar het begin.");
                string employeeID = Program.World.ReadLine();

                if (string.IsNullOrEmpty(employeeID))
                {
                    Program.World.WriteLine($"De door u ingevulde code was: '{employeeID}'. De code kan niet leeg zijn.");
                }
                else if (employeeID == "q")
                {
                    Program.Main();
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
                            GuideStartTour(chosenTour);
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

        public static void GuideStartTour(string chosenTour)
        {
            GuideScanLogic(chosenTour);
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
                    Program.World.WriteLine("Scan de streepjescode op het entreebewijs die u wilt scannen of toets [1] om te stoppen met scannen:");
                    string customerCode = Program.World.ReadLine();

                    GuideScanCustomerCode(chosenTour, customerCode);
                    GuideScanSound();
                    ShowCodesNotScanned(chosenTour);
                }
            }
            Program.World.WriteLine("Alle Klanten zijn succesvol gescand.");
            GuideAllScannedSound();
            Addingcustomerstotour(chosenTour);
            return true;
        }
        public static void GuideScanCustomerCode(string tourID, string customerCode)
        {
            foreach (Tours tour in listOfTours)
            {
                if (tour.ID == tourID)
                {
                    if (customerCode == "1")
                    {
                        ShowCodesNotScanned(tourID);
                        Program.World.WriteLine($"De volgende Klantnummers zijn nog niet gescand en zullen verwijderd worden als u door gaat. Weet u zeker dat u door wilt gaan?");
                        Program.World.WriteLine($"[1] Ja");
                        Program.World.WriteLine($"[2] Nee");
                        string gidsAnswer = Program.World.ReadLine();
                        if (gidsAnswer == "1")
                        {
                            RemoveCustomersIfNotScanned(tourID);
                            Addingcustomerstotour(tourID);
                        }
                        else if (gidsAnswer == "2")
                        {
                            break;
                        }

                    }
                    else if (!tour.Customer_Codes.Any(customer => customer.CustomerCode == customerCode))
                    {
                        Program.World.WriteLine($"Klant {customerCode} is niet gevonden in tour {tourID}");
                    }
                    else if (scannedCodes.Contains(customerCode))
                    {
                        Program.World.WriteLine($"Deze Klant is al gescand.");
                    }
                    else
                    {
                        scannedCodes.Add(customerCode);
                        Program.World.WriteLine($"Klantnummers {customerCode} is succesvol gescand.");
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
                    Program.World.WriteLine("Klantnummers die nog niet gescand zijn in deze rondleiding:");
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

        public static void RemoveCustomersIfNotScanned(string tourID)
        {
            foreach (Tours tour in listOfTours)
            {
                if (tour.ID == tourID)
                {
                    List<Customer> customersToRemove = new List<Customer>();
                    foreach (Customer customer in tour.Customer_Codes)
                    {
                        if (!scannedCodes.Contains(customer.CustomerCode))
                        {
                            customersToRemove.Add(customer);
                        }
                    }
                    foreach (Customer customer in customersToRemove)
                    {
                        tour.Customer_Codes.Remove(customer);
                        tour.Spots ++;
                        listOfCustomers.Add(customer);
                        DataAccess.WriteJsonToTours(listOfTours);
                        DataAccess.WriteJsonToCustomers(listOfCustomers);
                    }

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
                            Program.World.WriteLine($"Er zijn nog {tour.Spots} plekken.");
                            Program.World.WriteLine("Scan de streepjescode op uw entreebewijs of toets [1] om de rondleiding te starten");
                            string Customer_ID = Program.World.ReadLine().ToLower();
                            if (Customer_ID == "1")
                            {
                                string timeString = tour.Time.ToString("HH:mm");
                                Program.World.WriteLine($"U heeft rondleiding {ChosenTour} voor {timeString} gestart.");
                                Thread.Sleep(2000);
                                Program.Main();
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
                                    Program.World.Write("Deze klantnummers zijn op dit moment aangemeld: ");
                                    foreach (Customer customer in listofaddablecustomers)
                                    {
                                        Program.World.Write(customer.CustomerCode + ", ");
                                    }
                                    Program.World.WriteLine("\nWilt u nog iemand aanmelden? Ja[1] nee[2]");
                                    string yesno = Program.World.ReadLine();
                                    if (yesno == "1")
                                    {
                                        answer2 = true;
                                        answer = false;
                                    }
                                    else if (yesno == "2")
                                    {
                                        answer2 = true;
                                        answer = true;
                                        Tours.AddToTour(listofaddablecustomers, tour.ID);
                                        Addingcustomerstotour(ChosenTour);
                                    }
                                    else
                                    {
                                        Program.World.WriteLine("Voer een geldige optie in.");
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
