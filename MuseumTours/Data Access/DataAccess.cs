using System;
using Newtonsoft.Json;
namespace Program;

public static class DataAccess
{
    private static string pathTourslist = "DataSources/Tourslist.JSON";
    private static string pathCustomers = "DataSources/Customers.JSON";
    private static string pathEmployees = "DataSources/Employees.JSON";
    public static List<Customer> ReadJsonCustomers()
    {
        string File2Json = Program.World.ReadAllText(pathCustomers);
        List<Customer> listOfCustomers = JsonConvert.DeserializeObject<List<Customer>>(File2Json)!;
        return listOfCustomers;
    }
    public static List<Tours> ReadJsonTours()
    {
        string File2Json = Program.World.ReadAllText(pathTourslist);
        List<Tours> listOfTours = JsonConvert.DeserializeObject<List<Tours>>(File2Json)!;
        return listOfTours;
    }
    public static List<Guide> ReadJsonEmployees()
    {
        string File2Json = Program.World.ReadAllText(pathEmployees);
        List<Guide> listOfGuides = JsonConvert.DeserializeObject<List<Guide>>(File2Json)!;
        return listOfGuides;
    }
    public static bool WriteJsonToTours(List<Tours> listoftours)
    {
        try
        {
            string updatedJson = JsonConvert.SerializeObject(listoftours, Formatting.Indented);
            Program.World.WriteAllText(pathTourslist, updatedJson);
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }

    }
    public static bool WriteJsonToCustomers(List<Customer> listofcustomers)
    {
        try
        {
            string updatedJson = JsonConvert.SerializeObject(listofcustomers, Formatting.Indented);
            Program.World.WriteAllText(pathCustomers, updatedJson);
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }

    }
    public static List<Tours>? LoadTours()
    {
        string json = Program.World.ReadAllText(pathTourslist);
        return JsonConvert.DeserializeObject<List<Tours>>(json);
    }
    public static bool SaveTours(List<Tours> tours)
    {
        string json = JsonConvert.SerializeObject(tours, Formatting.Indented);
        Program.World.WriteAllText(pathTourslist, json);
        return true;
    }
}