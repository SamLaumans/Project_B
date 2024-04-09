namespace Program;

using System;
using System.Text.RegularExpressions;

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
      Console.WriteLine("Wilt u deelnemen aan een rondleiding of een reservering annuleren?\nAls u details wilt zien over de rondleidingen toets dan 'info'.");
      string answer = Console.ReadLine().ToLower();
      switch (answer)
      {
        case "deelnemen":
          Valid_Answer = true;
          program.CheckCustomerID();
          break;
        case "annuleren":
          Valid_Answer = true;
          Program.CancelAppointment();
          break;
        case "info":
          Valid_Answer = true;
          Tours.ShowAvailableTours("../../../Tourslist.Json");
          break;
        default:
          Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met 'deelnemen', 'annuleren' of 'info'.");
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
          Tours.ShowAvailableTours("../../../Tourslist.Json");
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
          Console.WriteLine($"De door u ingevulde code was: '{Customer_ID}', Probeer alstublieft opnieuw.");
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

