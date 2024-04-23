using Program;
using Newtonsoft.Json;
using System.Text.Json;

class DataAccess
{
    public static List<Customer> ReadJsonCustomers()
    {
        using StreamReader reader = new("../../../Customers.Json");
        string File2Json = reader.ReadToEnd();
        List<Customer> listOfCustomers = JsonConvert.DeserializeObject<List<Customer>>(File2Json)!;
        return listOfCustomers;
    }
    public static List<Tours> ReadJsonTours()
    {

        string File2Json = File.ReadAllText("../../../tourslist.Json");
        List<Tours> listOfTours = JsonConvert.DeserializeObject<List<Tours>>(File2Json)!;
        return listOfTours;
    }
    public static bool WriteJsonToTours(List<Tours> listoftours)
    {
        try
        {
            string updatedJson = JsonConvert.SerializeObject(listoftours, Formatting.Indented);
            File.WriteAllText("../../../Tourslist.Json", updatedJson);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to JSON file: {ex.Message}");
            return false;
        }
    }
    public static List<Tours> LoadTours()
    {
        string json = File.ReadAllText("../../../tourslist.json");
        return JsonConvert.DeserializeObject<List<Tours>>(json);
    }
    public static bool SaveTours(List<Tours> tours)
    {
        string json = JsonConvert.SerializeObject(tours, Formatting.Indented);
        File.WriteAllText("../../../tourslist.json", json);
        return true;
    }
}