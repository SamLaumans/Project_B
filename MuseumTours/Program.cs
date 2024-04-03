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
    Program program = new();
    bool Valid_Answer = false;
    while (Valid_Answer == false)
    {
      Console.WriteLine("Zou u willen deelnemen aan een rondleiding? Als u details wilt zien over de rondleidingen toets dan '1'");
      string answer = Console.ReadLine().ToLower();
      switch (answer)
      {
        case "ja":
          Valid_Answer = true;
          program.CheckCustomerID();
          break;
        case "nee":
          Valid_Answer = true;
          Console.WriteLine("We wensen u een plezierig en inspirerend bezoek!");
          break;
        case "1":
          Valid_Answer = true;
          program.ShowAvailableTours("../../../Tourslist.Json");
          break;
        default:
          Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met 'Ja' of 'Nee'");
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
        if (CheckIfCustomerInList(Customer_ID))
        {
          Answer = true;
          ShowAvailableTours("../../../Tourslist.Json");
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
              answerValid = AddToTour(Customer_ID, ChosenTour);
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
  public bool CheckIfCustomerInList(string idcustomer)
  {
    using StreamReader reader = new("../../../Customers.Json");
    string File2Json = reader.ReadToEnd();
    List<Customer> listOfCustomers = JsonConvert.DeserializeObject<List<Customer>>(File2Json)!;


    foreach (Customer customer in listOfCustomers)
    {
      Console.WriteLine(customer);
      if (customer.CustomerCode == idcustomer)
      {
        return true;
      }
    }
    return false;
  }
  public void ShowAvailableTours(string filename)
  {
    using StreamReader reader = new(filename);
    string File2Json = reader.ReadToEnd();
    List<Tours> listOfTours = JsonConvert.DeserializeObject<List<Tours>>(File2Json)!;

    int Count = 0;
    Console.WriteLine("Dit zijn de nog beschikbare rondleidingen voor vandaag:");
    foreach (Tours tour in listOfTours)
    {
      if (tour.Time > DateTime.Now && tour.Spots > 0)
      {
        Count++;
        string timeString = tour.Time.ToString("HH:mm");
        Console.WriteLine($"{tour.ID}. starttijd: {timeString} beschikbare plekken: {tour.Spots}");
      }
    }
  }
  public bool AddToTour(string customerid, string tourid)
  {
  string filePath = "../../../Tourslist.Json";

  string File2Json = File.ReadAllText(filePath);
  List<Tours> listOfTours = JsonConvert.DeserializeObject<List<Tours>>(File2Json)!;

  foreach (Tours tour in listOfTours)
  {
    if (tour.ID == tourid && tour.Time > DateTime.Now)
    {
      Customer customer = new Customer(customerid);
      tour.Customer_Codes.Add(customer);
      Console.WriteLine($"Reservering geplaatst. U word op {tour.Time} verwacht bij het verzamelpunt.");

      string updatedJson = JsonConvert.SerializeObject(listOfTours, Formatting.Indented);
      File.WriteAllText(filePath, updatedJson);

      return true;
    }
  }
  Console.WriteLine($"We hebben geen tour kunnen vinden met het ingevoerde nummer:{tourid}.");
  return false;
  }
    public static void CancelAppointment()
  {
    Console.WriteLine("Scan de code op uw ticket om een inschrijving te annuleren: ");
    string customerCodeToCancel = Console.ReadLine();

    Cancel cancel = new Cancel();
    cancel.CancelAppointment(customerCodeToCancel);
  }
}

