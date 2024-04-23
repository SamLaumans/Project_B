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
            Console.WriteLine("Scan de barcode op uw badge, toets het nummer onder de barcode in of toets 'q' om terug te gaan naar het begin.");
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
                    Answer = true;
                    Tours.ShowToursToGuide("../../../Tourslist.Json");
                    bool answerValid = false;
                    while (answerValid == false)
                    {
                        Console.WriteLine("Voer het rondleidingsnummer in waarvan u de deelnemers wilt zien of toets 'q' om terug te gaan naar het begin.");
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
                        }
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
}