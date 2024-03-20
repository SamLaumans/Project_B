using Newtonsoft.Json;

public class Rondleidingen
{
    public int ID;
    public string Name;
    public int Spots;
    public bool Started;
    public List<Customer> Customer_Codes;

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
