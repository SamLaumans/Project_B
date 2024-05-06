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
}


