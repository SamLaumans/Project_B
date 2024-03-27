using Newtonsoft.Json;

public class Rondleidingen
{
    public string ID;
    public string Name;
    public int Spots;
    public bool Started;

    public DateTime Time;
    public List<Customer> Customer_Codes;

    public Rondleidingen(string id, string name, int spots, bool started, DateTime time)
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
            List<Rondleidingen> items = JsonConvert.DeserializeObject<List<Rondleidingen>>(json);
        }
    }
}

