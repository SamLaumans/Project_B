using System;
using Newtonsoft.Json;
namespace Program;

public static class DataAccess
{
    private static string pathTourslist = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Tourslist.JSON"));
    private static string pathCustomers = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Customers.JSON"));
    private static string pathEmployees = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Employees.JSON"));
    public static List<Customer> ReadJsonCustomers()
    {
        using StreamReader reader = new(pathCustomers);
        string File2Json = reader.ReadToEnd();
        List<Customer> listOfCustomers = JsonConvert.DeserializeObject<List<Customer>>(File2Json)!;
        return listOfCustomers;
    }
    public static List<Tours> ReadJsonTours()
    {
        string File2Json = File.ReadAllText(pathTourslist);
        List<Tours> listOfTours = JsonConvert.DeserializeObject<List<Tours>>(File2Json)!;
        return listOfTours;
    }
    public static List<Guide> ReadJsonEmployees()
    {
        using StreamReader reader = new(pathEmployees);
        string File2Json = reader.ReadToEnd();
        List<Guide> listOfGuides = JsonConvert.DeserializeObject<List<Guide>>(File2Json)!;
        return listOfGuides;
    }
    public static bool WriteJsonToTours(List<Tours> listoftours)
    {
        try
        {
            string updatedJson = JsonConvert.SerializeObject(listoftours, Formatting.Indented);
            File.WriteAllText(pathTourslist, updatedJson);
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
            File.WriteAllText(pathCustomers, updatedJson);
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }

    }
    public static List<Tours>? LoadTours()
    {
        string json = File.ReadAllText(pathTourslist);
        return JsonConvert.DeserializeObject<List<Tours>>(json);
    }
    public static bool SaveTours(List<Tours> tours)
    {
        string json = JsonConvert.SerializeObject(tours, Formatting.Indented);
        File.WriteAllText(pathTourslist, json);
        return true;
    }
}