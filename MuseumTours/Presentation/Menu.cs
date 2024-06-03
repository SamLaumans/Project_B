namespace Program;
class Menu
{
    public static void MenuStart()
    {
        Program program = new();
        bool QuitProgram = false;
        TimeSpan startTime = new TimeSpan(8, 30, 0);
        TimeSpan endTime = new TimeSpan(17, 30, 0);
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        while (QuitProgram == false && currentTime >= startTime && currentTime <= endTime)
        {
            Program.World.WriteLine("Dit zijn de eerst komende 5 rondleidingen: ");
            Tours.ShowAvailableTours(2, 0);
            Program.World.WriteLine("Scan de streepjescode op uw entreebewijs.");
            string? FirstCustomerCode = Program.World.ReadLine().ToLower();
            if (FirstCustomerCode == "abcd")
            {
                Guide.CheckEmployeeID();
            }
            else if (Customer.CheckIfCustomerInList(FirstCustomerCode) == false && Tours.CheckIfCanCancel(FirstCustomerCode) == false)
            {
                Program.World.WriteLine($"Uw code klopt niet. Dit was de code die u invulde: {FirstCustomerCode}");
                Program.Main();
            }
            Program.World.WriteLine("");
            Program.World.WriteLine("Toets het nummer in van de actie die u wilt uitvoeren:");
            Program.World.WriteLine($"[1] Rondleiding reserveren \n[2] Rondleiding annuleren \n[3] Info rondleidingen \n");
            string answer = Program.World.ReadLine().ToLower();
            switch (answer)
            {
                case "1":
                    Tours.InputMoreCustomercodes(FirstCustomerCode);
                    break;
                case "2":
                    Program.World.WriteLine("Scan de streepjescode op uw entreebewijs om een inschrijving te annuleren of toets 'q' om terug te gaan naar het begin:");
                    string customerCodeToCancel = Program.World.ReadLine();
                    if (customerCodeToCancel == "q")
                    {
                        break;
                    }
                    else
                    {
                        Cancel.CancelAppointment(customerCodeToCancel);
                        break;
                    }
                case "3":
                    Tours.ShowAvailableTours(1, 0);
                    break;
                case "abcd":
                    Guide.CheckEmployeeID();
                    break;
                case "498901":
                    QuitProgram = true;
                    break;
                default:
                    Program.World.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met [1](Rondleiding reserveren), [2](Rondleiding annuleren), [3](Info Rondleidingen) of [4](Inloggen werknemers)).");
                    break;
            }
        }
    }
}