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
                answerValid = ChooseTour();
            }
            else
            {
                Console.WriteLine($"De door u ingevulde code was: '{employeeID}', Probeer alstublieft opnieuw.");
            }
        }
    }
}

public static bool ChooseTour()
{
    bool answerValid = false;

    while (!answerValid)
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
    }
    else
    {
        List<Tours> listOfTours = DataAccess.ReadJsonTours();
        bool tourFound = false;

        foreach (Tours tour in listOfTours)
        {
            if (tour.ID == chosenTour)
            {
                Tours.ShowChosenTour(chosenTour);
                answerValid = ChooseOption();
                tourFound = true;
                break;
            }
        }

        if (!tourFound)
        {
            Console.WriteLine($"Het opgegeven rondleidingsnummer '{chosenTour}' is niet geldig. Voer een geldig tournummer in.");
        }
    }
}


    return answerValid;
}

public static bool ChooseOption()
{
    bool chosenOption = false;

    while (!chosenOption)
    {
        Console.WriteLine("Om terug te gaan naar het overzicht van tours toets (b) om terug te gaan naar het beginscherm toets (q)");
        string guideChoice = Console.ReadLine();

        if (string.IsNullOrEmpty(guideChoice))
        {
            Console.WriteLine("Voer een geldige optie in.");
        }
        else if (guideChoice == "b")
        {
            Tours.ShowToursToGuide("../../../Tourslist.Json");
            break;
        }
        else if (guideChoice == "q")
        {
            Program.Main();
        }
    }

    return chosenOption;
}

}