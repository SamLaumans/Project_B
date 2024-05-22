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
            Program.World.WriteLine("Scan de barcode op uw badge, toets het nummer onder de barcode in of toets 'q' om terug te gaan naar het begin.");
            string Employee_ID = Program.World.ReadLine();
            if (Employee_ID == null)
            {
                Program.World.WriteLine($"De door u ingevulde code was: '{Employee_ID}'. De code kan niet leeg zijn.");
            }
            else if (Employee_ID == "q")
            {
                break;
            }
            else if (Employee_ID.Count() != 6)
            {
                Program.World.WriteLine($"De door u ingevulde code was: '{Employee_ID}'. De code bestaat altijd uit 6 cijfers.");
            }
            else if (Regex.IsMatch(Employee_ID, pattern))
            {
                Program.World.WriteLine($"De door u ingevulde code was: '{Employee_ID}'. De code bestaat altijd uit 6 cijfers.");
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
                        Program.World.WriteLine("Voer het rondleidingsnummer in waarvan u de deelnemers wilt zien of toets 'q' om terug te gaan naar het begin.");
                        string ChosenTour = Program.World.ReadLine();
                        if (ChosenTour == "q")
                        {
                            break;
                        }
                        else
                        {
                            Tours.ShowChosenTour(ChosenTour);
                            bool ChosenOption = false;
                            while (ChosenOption == false)
                            {
                                Program.World.WriteLine("Om terug te gaan naar het overzicht van tours toets (b) om terug te gaan naar het beginscherm toets (q)");
                                string GuideChoice = Program.World.ReadLine();
                                if (GuideChoice == null)
                                {
                                    Program.World.WriteLine("Voer een geldige optie in.");
                                }
                                else if (GuideChoice == "b")
                                {
                                    Tours.ShowToursToGuide("../../../Tourslist.Json");
                                    break;
                                }
                                else if (GuideChoice == "q")
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Program.World.WriteLine($"De door u ingevulde code was: '{Employee_ID}', Probeer alstublieft opnieuw.");
                }
            }
        }
    }
}