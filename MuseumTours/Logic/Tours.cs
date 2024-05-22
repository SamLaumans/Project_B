namespace Program;

using Newtonsoft.Json;

public class Tours
{
  public string ID;
  public int Spots;
  public bool Started;
  public DateTime Time;
  public List<Customer> Customer_Codes;

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
        DataAccess.WriteJsonToTours(listOfTours);
        break;
      }
    }

    if (booked)
    {
      Program.World.WriteLine($"Reservering geplaatst.");
    }
    else
    {
      Program.World.WriteLine($"We hebben geen rondleiding kunnen vinden met het ingevoerde nummer:{tourid}.");
    }

    return booked;
  }

  public static void ShowAvailableTours(int FiveOrAll, int People)
  {
    if (FiveOrAll == 1)
    {
      List<Tours> listOfTours = DataAccess.ReadJsonTours();
      int Count = 0;
      Program.World.WriteLine("Dit zijn de nog beschikbare rondleidingen voor vandaag(iedere rondleiding duurt 40 minuten):");
      bool touratleast = false;
      foreach (Tours tour in listOfTours)
      {
        if (tour.Time > DateTime.Now && tour.Spots > People)
        {
          Count++;
          string timeString = tour.Time.ToString("HH:mm");
          Program.World.WriteLine($"{tour.ID}. starttijd: {timeString} | beschikbare plekken: {tour.Spots}");
          touratleast = true;
        }
      }
      if (touratleast == false)
      {
        Program.World.WriteLine("Er zijn op het moment geen beschikbare rondleidingen.");
      }
    }
    else // Alleen voor de eerste 5 laten zien.
    {
      List<Tours> listOfTours = DataAccess.ReadJsonTours();
      int Count = 0;
      Program.World.WriteLine("Dit zijn de nog beschikbare rondleidingen voor vandaag(iedere rondleiding duurt 40 minuten):");
      bool touratleast = false;
      foreach (Tours tour in listOfTours)
      {
        if (tour.Time > DateTime.Now && tour.Spots > 0)
        {
          Count++;
          string timeString = tour.Time.ToString("HH:mm");
          Program.World.WriteLine($"{tour.ID}. starttijd: {timeString} | beschikbare plekken: {tour.Spots}");
          touratleast = true;
          if (Count == 5)
          {
            break;
          }
        }
      }
      if (touratleast == false)
      {
        Program.World.WriteLine("Er zijn op het moment geen beschikbare rondleidingen.");
      }
    }
  }
  public static void ShowToursToGuide(string filename)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();
    Program.World.WriteLine("Kies een rondleiding om zijn deelnemers te zien:");
    foreach (Tours tour in listOfTours)
    {
      string timeString = tour.Time.ToString("HH:mm");
      Program.World.WriteLine($"{tour.ID}. starttijd: {timeString} | Aantal deelnemers: {13 - tour.Spots}");
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

        Program.World.WriteLine($"Tour van : {timeString}");
        Program.World.WriteLine($"Hieronder zijn alle codes van de bezoekers in deze rondleiding.");
        Program.World.WriteLine($"=======================================================================");
        foreach (var customerCode in tour.Customer_Codes)
        {
          Program.World.WriteLine($"Customer Code: {customerCode.CustomerCode}");
        }
        Program.World.WriteLine($"=======================================================================");
      }
    }
  }
  public static void InputMoreCustomercodes()
  {
    List<Customer> listofaddablecustomers = new List<Customer>();
    bool answer = false;
    int AmountOfPeople = 0;
    while (answer == false)
    {
      Program.World.WriteLine("Scan de streepjescode op uw ticket, toets het nummer onder de barcode in of toets 'q' om terug te gaan naar het begin.");
      string Customer_ID = Program.World.ReadLine().ToLower();
      if (Customer.CheckIfCustomerInList(Customer_ID) == true)
      {
        AmountOfPeople += 1;
        // Customer customer = new Customer(Customer_ID);
        listofaddablecustomers.Add(new Customer(Customer_ID));
        bool answer2 = false;
        while (answer2 == false)
        {
          Program.World.WriteLine("Bent u met meerdere mensen en wilt u nog iemand aanmelden? Ja(1) nee(2)");
          string yesno = Program.World.ReadLine();
          switch (yesno)
          {
            case "1":
              answer2 = true;
              answer = false;
              break;
            case "2":
              answer2 = true;
              answer = true;
              Tours.ShowAvailableTours(1, AmountOfPeople);
              bool answerValid = false;
              while (answerValid == false)
              {
                Program.World.WriteLine("Voer het rondleidingsnummer waaraan u zou willen deelnemen in of toets 'q' om terug te gaan naar het begin.");
                string ChosenTour = Program.World.ReadLine().ToLower();
                if (ChosenTour == "q")
                {
                  break;
                }
                else
                {
                  answerValid = Tours.AddToTour(listofaddablecustomers, ChosenTour);
                  answerValid = true;
                }
              }
              break;
            default:
              Program.World.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met '1' ja of '2' nee.");
              answer2 = false;
              break;
          }
        }
      }
      else if (Customer_ID == "q")
      {
        answer = true;
      }
      else
      {
        Program.World.WriteLine($"De door u ingevulde code was: '{Customer_ID}'. De code bestaat altijd uit 10 cijfers.");
        answer = false;
      }
    }
  }
}


