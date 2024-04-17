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

  public static bool AddToTour(string customerid, string tourid)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();

    foreach (Tours tour in listOfTours)
    {
      if (tour.ID == tourid && tour.Time > DateTime.Now)
      {
        Customer customer = new Customer(customerid);
        tour.Customer_Codes.Add(customer);
        tour.Spots--;
        Console.WriteLine($"Reservering geplaatst. U word op {tour.Time} verwacht bij het verzamelpunt.");

        DataAccess.WriteJsonToTours(listOfTours);

        return true;
      }
    }
    Console.WriteLine($"We hebben geen tour kunnen vinden met het ingevoerde nummer:{tourid}.");
    return false;
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
}


