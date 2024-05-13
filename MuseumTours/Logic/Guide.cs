namespace Program;

using System;
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
    bool answerValid = false;

    while (!answerValid)
    {
        Console.WriteLine("Scan de barcode op uw badge, toets het nummer onder de barcode in of toets 'q' om terug te gaan naar het begin.");
        string employeeID = Console.ReadLine();

        if (string.IsNullOrEmpty(employeeID))
        {
            Console.WriteLine($"De door u ingevulde code was: '{employeeID}'. De code kan niet leeg zijn.");
        }
        else if (employeeID == "q")
        {
            Program.Main();
        }
        else if (employeeID.Length != 6 || Regex.IsMatch(employeeID, pattern))
        {
            Console.WriteLine($"De door u ingevulde code was: '{employeeID}'. De code bestaat altijd uit 6 cijfers.");
        }
        else
        {
            Guide guide = new Guide(employeeID);
            if (guide.CheckIfGuideInList(employeeID))
            {
                Tours.ShowToursToGuide("../../../Tourslist.Json");
                answerValid = GuideChooseTour(listOfTours, scannedCodes);
            }
            else
            {
                Console.WriteLine($"De door u ingevulde code was: '{employeeID}', Probeer alstublieft opnieuw.");
            }
        }
    }
}

public static bool GuideChooseTour(List<Tours> listOfTours, List<string> scannedCodes)
{
    while (true)
    {
        Console.WriteLine("Voer het rondleidingsnummer in waarvan u de deelnemers wilt zien of toets 'q' om terug te gaan naar het begin.");
        string chosenTour = Console.ReadLine();

        if (string.IsNullOrEmpty(chosenTour))
        {
            Console.WriteLine($"Uw invoer was '{chosenTour}'. Voer een geldige tournummer in.");
        }
        else if (chosenTour == "q")
        {
            Program.Main();
            return false; // Return false to indicate not finished
        }
        else
        {
            bool tourFound = false;

            foreach (Tours tour in listOfTours)
            {
                if (tour.ID == chosenTour)
                {
                    Tours.ShowChosenTour(chosenTour);
                    tourFound = true;

                    // Call GuideChooseOption and pass chosenTour to it
                    if (!GuideChooseOption(chosenTour, listOfTours, scannedCodes))
                    {
                        return false; // Return false to indicate not finished
                    }

                    break;
                }
            }

            if (!tourFound)
            {
                Console.WriteLine($"Het opgegeven rondleidingsnummer '{chosenTour}' is niet geldig. Voer een geldig tournummer in.");
            }
        }
    }
}

public static bool GuideChooseOption(string chosenTour, List<Tours> listOfTours, List<string> scannedCodes)
{
    while (true)
    {
        Console.WriteLine("toets (s) om te beginnen met scannen , Om terug te gaan naar het overzicht van tours toets (b) om terug te gaan naar het beginscherm toets (q)");
        string guideChoice = Console.ReadLine();

        if (string.IsNullOrEmpty(guideChoice))
        {
            Console.WriteLine("Voer een geldige optie in.");
        }
        else if (guideChoice == "b")
        {
            Tours.ShowToursToGuide("../../../Tourslist.Json");
            return false; 
        }
        else if (guideChoice == "q")
        {
            Program.Main();
            return false; 
        }
        else if (guideChoice == "s")
        {
            Console.WriteLine("Scan de Customer code die u wilt scannen:");
            string customerCodeToScan = Console.ReadLine();

            GuideScanCustomerCode(chosenTour, customerCodeToScan, listOfTours, scannedCodes);
        }
    }
}



public static void GuideScanCustomerCode(string tourID, string customerCode, List<Tours> listOfTours, List<string> scannedCodes)
{
    foreach (Tours tour in listOfTours)
    {
        if (tour.ID == tourID)
        {
            foreach (Customer customer in tour.Customer_Codes)
            {
                if (customer.CustomerCode == customerCode)
                {
                    scannedCodes.Add(customerCode);
                    Console.WriteLine($"Customer code {customerCode} is succesvol gescanned.");

                    return; 
                    }
            }
            Console.WriteLine($"Customer code {customerCode} niet gevonden in tour {tourID}.");
            return; 
        }
    }
    Console.WriteLine($"Tour met nummer {tourID} niet gevonden.");
}
}