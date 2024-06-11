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

  public static void AddToTour(List<Customer> customerid, string tourid)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();
    List<Customer> listofcustomers = DataAccess.ReadJsonCustomers();
    bool booked = false;

    // Get the tour from the list of tours
    Tours tour = checkiftourvalid(tourid);

    if (tour != null)
    {
      foreach (Customer customer in customerid)
      {
        tour.Customer_Codes.Add(customer);
        // Remove customer from listofcustomers
        for (int j = 0; j < listofcustomers.Count; j++)
        {
          if (listofcustomers[j].CustomerCode == customer.CustomerCode)
          {
            listofcustomers.RemoveAt(j);
            break;
          }
        }
      }

      // Update the number of spots
      tour.Spots -= customerid.Count;
      booked = true;

      // Find the index of the tour in listOfTours
      for (int i = 0; i < listOfTours.Count; i++)
      {
        if (listOfTours[i].ID == tourid)
        {
          // Update the tour in the list
          listOfTours[i] = tour;
          break;
        }
      }
      // Write updated lists back to JSON
      DataAccess.WriteJsonToTours(listOfTours);
      DataAccess.WriteJsonToCustomers(listofcustomers);
      string persons = "persoon";
      if (customerid.Count > 1)
      {
        persons = "personen";
      }

      Program.World.WriteLine($"U heeft voor {customerid.Count} {persons} gereserveerd voor de rondleiding om {tour.Time}");
    }
    else
    {
      Program.World.WriteLine($"We hebben geen rondleiding kunnen vinden met het ingevoerde nummer:{tourid}.");
    }
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
        Program.World.WriteLine($"Hieronder zijn alle streepjescodes van de bezoekers in deze rondleiding.");
        Program.World.WriteLine($"=======================================================================");
        foreach (var customerCode in tour.Customer_Codes)
        {
          Program.World.WriteLine($"Streepjescode: {customerCode.CustomerCode}");
        }
        Program.World.WriteLine($"=======================================================================");
      }
    }
  }
  public static void InputMoreCustomercodes(string FirstCustomer)
  {
    List<Customer> listofaddablecustomers = new List<Customer>();
    bool answer = false;
    int AmountOfPeople = 0;
    string Customer_ID;
    while (answer == false)
    {
      bool booleanstuff = true;
      if (AmountOfPeople != 0)
      {
        Program.World.WriteLine("Scan de streepjescode op uw entreeebewijs of toets [q] om terug te gaan naar het begin.");
        Customer_ID = Program.World.ReadLine().ToLower();
      }
      else
      {
        Customer_ID = FirstCustomer;
      }
      foreach (Customer customer in listofaddablecustomers)
      {
        if (customer.CustomerCode == Customer_ID)
        {
          booleanstuff = false;
        }
      }
      if (booleanstuff == false)
      {
        Program.World.WriteLine($"Deze klant {Customer_ID} is al aangemeld.");
        answer = false;
      }
      else if (Customer.CheckIfCustomerInList(Customer_ID) == true)
      {
        AmountOfPeople += 1;
        // Customer customer = new Customer(Customer_ID);
        listofaddablecustomers.Add(new Customer(Customer_ID));
        bool answer2 = false;
        while (answer2 == false)
        {
          Program.World.Write("Deze streepjescodes zijn op dit moment aangemeld: \n");
          foreach (Customer customer in listofaddablecustomers)
          {
            Program.World.Write(customer.CustomerCode + ", ");
          }
          Program.World.WriteLine("\nWilt u nog iemand aanmelden? Ja[1] nee[2]");
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
              Thread.Sleep(250);
              Program.World.Clear();
              Tours.ShowAvailableTours(1, AmountOfPeople);
              bool answerValid = false;
              while (answerValid == false)
              {
                Program.World.WriteLine("Voer het rondleidingsnummer waaraan u zou willen deelnemen in of toets 'q' om terug te gaan naar het begin.");
                string ChosenTour = Program.World.ReadLine().ToLower();
                if (ChosenTour == "q")
                {
                  Program.Main();
                }
                else if (checkiftourvalid(ChosenTour) is Tours)
                {
                  AddToTour(listofaddablecustomers, ChosenTour);
                  answerValid = true;
                }
                else
                {
                  Program.World.WriteLine($"We hebben Rondleiding '{ChosenTour}' niet kunnen vinden.");
                  answerValid = false;
                }
              }
              break;
            default:
              Program.World.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met [1] ja of [2] nee.");
              answer2 = false;
              break;
          }
        }
      }
      else if (Customer_ID == "q")
      {
        Program.Main();
        answer = true;
      }
      else
      {
        Program.World.WriteLine($"De door u ingevulde code was: '{Customer_ID}'. De code bestaat altijd uit 10 cijfers.");
        answer = false;
      }
    }
  }

  public static bool CheckIfCanCancel(string CustomerID)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();
    foreach (Tours tour in listOfTours)
    {
      foreach (Customer customer in tour.Customer_Codes)
      {
        if (customer.CustomerCode == CustomerID)
        {
          return true;
        }
      }
    }
    return false;
  }

  public static string? CheckWhatTour(string CustomerID)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();
    foreach (Tours tour in listOfTours)
    {
      foreach (Customer customer in tour.Customer_Codes)
      {
        if (customer.CustomerCode == CustomerID)
        {
          return $"{tour.Time}";
        }
      }
    }
    return null;
  }

  public static Tours checkiftourvalid(string tourid)
  {
    List<Tours> listOfTours = DataAccess.ReadJsonTours();
    foreach (Tours tour in listOfTours)
    {
      if (tour.ID == tourid && tour.Time > DateTime.Now && tour.Spots > 0)
      {
        return tour;
      }
    }
    return null;
  }
}

