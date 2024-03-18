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
        
    }
    
    public void LoadJson()
    {
        using (StreamReader r = new StreamReader("file.json"))
        {
            string json = r.ReadToEnd();
            List<Rondleidingen> items = JsonConvert.DeserializeObject<List<Rondleidingen>>(json);
        }
    }
}
