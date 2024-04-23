namespace Program;

using System;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class Program
{
  public static void Main()
  {
    TimeSpan startTime = new TimeSpan(10, 30, 0);
    TimeSpan endTime = new TimeSpan(17, 30, 0);
    TimeSpan currentTime = DateTime.Now.TimeOfDay;
    while (currentTime >= startTime && currentTime <= endTime)
    {
      Thread.Sleep(2500);
      Console.WriteLine("\n=========================================================================");
      Program.MainProgram();
    }
    Console.WriteLine("Het museum is gesloten.");
  }
  public static void MainProgram()
  {
    Program program = new();
    bool Valid_Answer = false;
    while (Valid_Answer == false)
    {
      Console.WriteLine("Dit zijn de eerst komende 5 rondleidingen: ");
      Tours.ShowAvailableTours(2);
      Console.WriteLine("Wilt u deelnemen aan een rondleiding of een reservering annuleren?\nAls u info wilt zien over de rondleidingen toets[3].");
      Console.WriteLine($"[1] Deelnemen \n[2] Annuleren \n[3] Info \n [4] Gids ");
      string answer = Console.ReadLine().ToLower();
      switch (answer)
      {
        case "1":
          Valid_Answer = true;
          program.CheckCustomerID();
          break;
        case "2":
          Valid_Answer = true;
          Program.CancelAppointment();
          break;
        case "3":
          Valid_Answer = true;
          Tours.ShowAvailableTours(1);
          break;
        case "4":
          Valid_Answer = true;
          Guide.CheckEmployeeID();
          break;
        default:
          Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met 'd'(deelnemen), 'a'(annuleren) of 'i'(info).");
          break;
      }
    }
  }
  public void CheckCustomerID()
  {
    string pattern = @"[^0-9]";
    bool Answer = false;
    while (Answer == false)
    {
      Console.WriteLine("Scan de barcode op uw ticket, toets het nummer onder de barcode in of toets 'q' om terug te gaan naar het begin.");
      string Customer_ID = Console.ReadLine();
      if (Customer_ID == null)
      {
        Console.WriteLine($"De door u ingevulde code was: '{Customer_ID}'. De code kan niet leeg zijn.");
      }
      else if (Customer_ID == "q")
      {
        Program.Main();
      }
      else if (Customer_ID.Count() != 10)
      {
        Console.WriteLine($"De door u ingevulde code was: '{Customer_ID}'. De code bestaat altijd uit 10 cijfers.");
      }
      else if (Regex.IsMatch(Customer_ID, pattern))
      {
        Console.WriteLine($"De door u ingevulde code was: '{Customer_ID}'. De code bestaat altijd uit 10 cijfers.");
      }
      else
      {
        Customer customer = new Customer(Customer_ID);
        if (customer.CheckIfCustomerInList(Customer_ID))
        {
          Answer = true;
          Tours.ShowAvailableTours(1);
          bool answerValid = false;
          while (answerValid == false)
          {
            Console.WriteLine("Voer het rondleidingsnummer waaraan u zou willen deelnemen in of toets 'q' om terug te gaan naar het begin.");
            string ChosenTour = Console.ReadLine();
            if (ChosenTour == "q")
            {
              Program.Main();
            }
            else
            {
              answerValid = Tours.AddToTour(Customer_ID, ChosenTour);
            }
          }
        }
        else
        {
          Console.WriteLine($"De door u ingevulde code was: '{Customer_ID}', Probeer het ;alstublieft opnieuw.");
        }
      }
    }
  }
  public static void CancelAppointment()
  {
    Console.WriteLine("Scan de code op uw ticket om een inschrijving te annuleren: ");
    string customerCodeToCancel = Console.ReadLine();

    Cancel cancel = new Cancel();
    cancel.CancelAppointment(customerCodeToCancel);
  }
}


