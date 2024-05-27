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

      Console.WriteLine($"U heeft voor {customerid.Count} gereserveerd voor de rondleiding om {tour.Time}");
    }
    else
    {
      Console.WriteLine($"We hebben geen rondleiding kunnen vinden met het ingevoerde nummer: {tourid}.");
    }
  }
  // public static void AddToTour(List<Customer> customerid, string tourid)
  // {
  //   List<Tours> listOfTours = DataAccess.ReadJsonTours();
  //   List<Customer> listofcustomers = DataAccess.ReadJsonCustomers();
  //   bool booked = false;
  //   Tours tour = checkiftourvalid(tourid);
  //   foreach (Customer customer in customerid)
  //   {
  //     tour.Customer_Codes.Add(customer);
  //     foreach (Customer cus in listofcustomers)
  //     {
  //       if (cus.CustomerCode == customer.CustomerCode)
  //       {
  //         listofcustomers.Remove(cus);

  //         break;
  //       }
  //     }
  //   }
  //   tour.Spots -= customerid.Count;
  //   DataAccess.WriteJsonToTours(listOfTours);
  //   DataAccess.WriteJsonToCustomers(listofcustomers);
  //   Console.WriteLine($"U heeft voor {customerid.Count} gereserveerd voor de rondleiding om {tour.Time}");
  // }

  public static void ShowAvailableTours(int FiveOrAll, int People)
  {
    if (FiveOrAll == 1)
    {
      List<Tours> listOfTours = DataAccess.ReadJsonTours();
      int Count = 0;
      Console.WriteLine("Dit zijn de rondleidingen voor vandaag(iedere rondleiding duurt 40 minuten):");
      bool touratleast = false;
      foreach (Tours tour in listOfTours)
      {
        if (tour.Time > DateTime.Now && tour.Spots > People)
        {
          Count++;
          string timeString = tour.Time.ToString("HH:mm");
          Console.WriteLine($"{tour.ID}. starttijd: {timeString} | beschikbare plekken: {tour.Spots}");
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
      bool touratleast = false;
      foreach (Tours tour in listOfTours)
      {
        if (tour.Time > DateTime.Now && tour.Spots > 0)
        {
          Count++;
          string timeString = tour.Time.ToString("HH:mm");
          Console.WriteLine($"{tour.ID}. starttijd: {timeString} | beschikbare plekken: {tour.Spots}");
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
    int AmountOfPeople = 0;
    while (answer == false)
    {
      bool booleanstuff = true;
      Console.WriteLine("Scan de streepjescode op uw entreeebewijs of toets [q] om terug te gaan naar het begin.");
      string Customer_ID = Console.ReadLine().ToLower();
      foreach (Customer customer in listofaddablecustomers)
      {
        if (customer.CustomerCode == Customer_ID)
        {
          booleanstuff = false;
        }
      }
      if (booleanstuff == false)
      {
        Console.WriteLine($"Deze klant {Customer_ID} is al aangemeld.");
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
          Console.Write("Deze klantcodes zijn op dit moment aangemeld: ");
          foreach (Customer customer in listofaddablecustomers)
          {
            Console.Write(customer.CustomerCode + ", ");
          }
          Console.WriteLine("\nWilt u nog iemand aanmelden? Ja[1] nee[2]");
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
              Thread.Sleep(250);
              Console.Clear();
              Tours.ShowAvailableTours(1, AmountOfPeople);
              bool answerValid = false;
              while (answerValid == false)
              {
                Console.WriteLine("Voer het rondleidingsnummer waaraan u zou willen deelnemen in of toets 'q' om terug te gaan naar het begin.");
                string ChosenTour = Console.ReadLine().ToLower();
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
                  Console.WriteLine($"We hebben Rondleiding '{ChosenTour}' niet kunnen vinden.");
                  answerValid = false;
                }
              }
              break;
            default:
              Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met [1] ja of [2] nee.");
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
        Console.WriteLine($"De door u ingevulde code was: '{Customer_ID}'. De code bestaat altijd uit 10 cijfers.");
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