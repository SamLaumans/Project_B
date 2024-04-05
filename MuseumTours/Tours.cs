namespace Program;

using Newtonsoft.Json;

public class Tours
{
    public string ID;
    public string Name;
    public int Spots;
    public bool Started;

    public DateTime Time;
    public List<Customer>? Customer_Codes;

    public Tours(string id, string name, int spots, bool started, DateTime time)
    {
        ID = id;
        Name = name;
        Spots = spots;
        Time = time;
    }

    public static bool AddToTour(string customerid, string tourid)
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
        tour.Spots--;
        Console.WriteLine($"Reservering geplaatst. U word op {tour.Time} verwacht bij het verzamelpunt.");

        string updatedJson = JsonConvert.SerializeObject(listOfTours, Formatting.Indented);
        File.WriteAllText(filePath, updatedJson);

        return true;
      }
    }
    Console.WriteLine($"We hebben geen tour kunnen vinden met het ingevoerde nummer:{tourid}.");
    return false;
    }
    public static void ShowAvailableTours(string filename)
    {
    using StreamReader reader = new(filename);
    string File2Json = reader.ReadToEnd();
    List<Tours> listOfTours = JsonConvert.DeserializeObject<List<Tours>>(File2Json)!;

    int Count = 0;
    Console.WriteLine("Dit zijn de nog beschikbare rondleidingen voor vandaag:");
    bool touratleast = false;
    foreach (Tours tour in listOfTours)
    {
      if (tour.Time > DateTime.Now && tour.Spots > 0)
      {
        Count++;
        string timeString = tour.Time.ToString("HH:mm");
        Console.WriteLine($"{tour.ID}. starttijd: {timeString} beschikbare plekken: {tour.Spots}");
        touratleast = true;
      }
    }
    if (touratleast == false)
    {
      Console.WriteLine("Er zijn op het moment geen beschikbare rondleidingen.");
    }
  }
}

