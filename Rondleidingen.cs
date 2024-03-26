using Newtonsoft.Json;

public class Rondleidingen
{
    public int ID;
    public string Name;
    public int Spots;
    public bool Started;

    public DateTime Time;
    public List<Customer> Customer_Codes;

    public Rondleidingen(int id, string name, int spots, bool started, DateTime time)
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

    string filename = "Rondleidingen.JSON";
    public void ShowAvailableTours(string filename)
    {
        StreamReader reader = new(filename);
        string File2Json = reader.ReadToEnd();
        List<Rondleidingen> lijstrondleidingen = JsonConvert.DeserializeObject<List<Rondleidingen>>(File2Json)!;
        reader.Close();

        int Count = 0;
        foreach (Rondleidingen rondleiding in lijstrondleidingen)
        {
            if (Started == false)
            {
                Count++;
                Console.WriteLine($"{Count}. {Name} starttijd: {Time} schikbare plekken: {Spots}");
            }
        }
    }
}

