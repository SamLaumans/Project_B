namespace Program;

using Newtonsoft.Json;

public class Tours
{
  public string ID;
  public int Spots;
  public bool Started;
  public DateTime Time;
  public List<Customer>? Customer_Codes;

  public Tours(string id, int spots, bool started, DateTime time)
  {
    ID = id;
    Spots = spots;
    Time = time;
  }

  public static bool AddToTour(List<Customer> customerid, string tourid)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();
    bool booked = false;

    foreach (Tours tour in listOfTours)
    {
      if (tour.ID == tourid && tour.Time > DateTime.Now && tour.Spots > 0)
      {
        foreach (Customer customer in customerid)
        {
          Customer customer1 = new Customer(customer.CustomerCode);
          tour.Customer_Codes.Add(customer1);
        }
        tour.Spots -= customerid.Count;
        booked = true;
        Console.WriteLine(DataAccess.WriteJsonToTours(listOfTours));
        break;
      }
    }

    if (booked)
    {
      Console.WriteLine($"Reservering geplaatst.");
    }
    else
    {
      Console.WriteLine($"We hebben geen rondleiding kunnen vinden met het ingevoerde nummer:{tourid}.");
    }

    return booked;
  }

  public static void ShowAvailableTours(int FiveOrAll)
  {
    if (FiveOrAll == 1)
    {
      List<Tours> listOfTours = DataAccess.ReadJsonTours();
      int Count = 0;
      Console.WriteLine("Dit zijn de nog beschikbare rondleidingen voor vandaag:");
      bool touratleast = false;
      foreach (Tours tour in listOfTours)
      {
        if (tour.Time > DateTime.Now && tour.Spots > 0)
        {
          Count++;
          string timeString = tour.Time.ToString("HH:mm");
          Console.WriteLine($"{tour.ID}. starttijd: {timeString} | duur: 40 minuten | beschikbare plekken: {tour.Spots}");
          touratleast = true;
        }
      }
      if (touratleast == false)
      {
        Console.WriteLine("Er zijn op het moment geen beschikbare rondleidingen.");
        Program.Main();
      }
    }
    else // Alleen voor de eerste 5 laten zien.
    {
      List<Tours> listOfTours = DataAccess.ReadJsonTours();
      int Count = 0;
      Console.WriteLine("Dit zijn de nog beschikbare rondleidingen voor vandaag:");
      bool touratleast = false;
      foreach (Tours tour in listOfTours)
      {
        if (tour.Time > DateTime.Now && tour.Spots > 0)
        {
          Count++;
          string timeString = tour.Time.ToString("HH:mm");
          Console.WriteLine($"{tour.ID}. starttijd: {timeString} | duur: 40 minuten | beschikbare plekken: {tour.Spots}");
          touratleast = true;
          if (Count == 5)
          {
            break;
          }
        }
      }
      if (touratleast == false)
      {
        Console.WriteLine("Er zijn op het moment geen beschikbare rondleidingen.");
        Program.Main();
      }
    }
  }
  public static void ShowToursToGuide(string filename)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();
    Console.WriteLine("Kies een rondleiding om zijn deelnemers te zien:");
    foreach (Tours tour in listOfTours)
    {
      string timeString = tour.Time.ToString("HH:mm");
      Console.WriteLine($"{tour.ID}. starttijd: {timeString} | Aantal deelnemers: {13 - tour.Spots}");
    }
  }
  public static void ShowChosenTour(string tourid)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();
    foreach (Tours tour in listOfTours)
    {
      if (tour.ID == tourid)
      {
        string timeString = tour.Time.ToString("HH:mm");

        Console.WriteLine($"Tour van : {timeString}");
        Console.WriteLine($"Hieronder zijn alle codes van de bezoekers in deze rondleiding.");
        Console.WriteLine($"=======================================================================");
        foreach (var customerCode in tour.Customer_Codes)
        {
          Console.WriteLine($"Customer Code: {customerCode.CustomerCode}");
        }
        Console.WriteLine($"=======================================================================");
      }
    }
  }
  public static void InputMoreCustomercodes()
  {
    List<Customer> listofaddablecustomers = new List<Customer>();
    bool answer = false;
    while (answer == false)
    {
      Console.WriteLine("Scan de barcode op uw ticket, toets het nummer onder de barcode in of toets 'q' om terug te gaan naar het begin.");
      string Customer_ID = Console.ReadLine();
      if (Customer.CheckIfCustomerInList(Customer_ID) == true)
      {
        // Customer customer = new Customer(Customer_ID);
        listofaddablecustomers.Add(new Customer(Customer_ID));
        bool answer2 = false;
        while (answer2 == false)
        {
        Console.WriteLine("Bent u met meerdere mensen en wilt u nog iemand aanmelden? Ja(1) nee(2)");
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
          Tours.ShowAvailableTours(1);
          bool answerValid = false;
          while (answerValid == false)
          {
            Console.WriteLine("Voer het rondleidingsnummer waaraan u zou willen deelnemen in of toets 'q' om terug te gaan naar het begin.");
            string ChosenTour = Console.ReadLine();
            if (ChosenTour == "q")
            {
            }
            else
            {
              answerValid = Tours.AddToTour(listofaddablecustomers, ChosenTour);
              answerValid = true;
            }
          }
          break;
        default:
          Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met '1' ja of '2' nee.");
          answer2 = false;
          break;
        }
      }
    }
    else
    {
        Console.WriteLine($"De door u ingevulde code was: '{Customer_ID}'. De code bestaat altijd uit 10 cijfers.");
        answer = false;
      }
    }
  }
}


