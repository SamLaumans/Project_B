using Program;
using Newtonsoft.Json;
using System.Text.Json;

public static class DataAccess
{
    private static string pathTourslist = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Tourslist.JSON")); 
    private static string pathCustomers = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/Customers.JSON"));
    public static List<Customer> ReadJsonCustomers()
    {
<<<<<<< HEAD
        using StreamReader reader = new("../../../Customers.Json");
=======
        using StreamReader reader = new(pathCustomers);
>>>>>>> 32e7c940cf74b7045a88e5fee7cf7dc954b52c4b
        string File2Json = reader.ReadToEnd();
        List<Customer> listOfCustomers = JsonConvert.DeserializeObject<List<Customer>>(File2Json)!;
        return listOfCustomers;
    }
    public static List<Tours> ReadJsonTours()
    {
<<<<<<< HEAD

        string File2Json = File.ReadAllText("../../../tourslist.Json");
=======
        string File2Json = File.ReadAllText(pathTourslist);
>>>>>>> 32e7c940cf74b7045a88e5fee7cf7dc954b52c4b
        List<Tours> listOfTours = JsonConvert.DeserializeObject<List<Tours>>(File2Json)!;
        return listOfTours;
    }
    public static bool WriteJsonToTours(List<Tours> listoftours)
    {
        try
        {
            string updatedJson = JsonConvert.SerializeObject(listoftours, Formatting.Indented);
<<<<<<< HEAD
            File.WriteAllText("../../../Tourslist.Json", updatedJson);
=======
            File.WriteAllText(pathTourslist, updatedJson);
>>>>>>> 32e7c940cf74b7045a88e5fee7cf7dc954b52c4b
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to JSON file: {ex.Message}");
            return false;
        }
    }
    public static List<Tours>? LoadTours()
    {
<<<<<<< HEAD
        string json = File.ReadAllText("../../../tourslist.json");
=======
        string json = File.ReadAllText(pathTourslist);
>>>>>>> 32e7c940cf74b7045a88e5fee7cf7dc954b52c4b
        return JsonConvert.DeserializeObject<List<Tours>>(json);
    }
    public static bool SaveTours(List<Tours> tours)
    {
        string json = JsonConvert.SerializeObject(tours, Formatting.Indented);
<<<<<<< HEAD
        File.WriteAllText("../../../tourslist.json", json);
=======
        File.WriteAllText(pathTourslist, json);
>>>>>>> 32e7c940cf74b7045a88e5fee7cf7dc954b52c4b
        return true;
    }
}