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
        Started = started;
        Time = time;
    }

    public void CheckIn()
    {
        Console.WriteLine("U heeft succesvol gereserveerd.");
    }

    public void LoadJson()
    {
        using (StreamReader r = new StreamReader("Rondleidingen.json"))
        {
            string json = r.ReadToEnd();
            List<Tours>? items = JsonConvert.DeserializeObject<List<Tours>>(json);
        }
    }
}
