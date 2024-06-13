namespace Program;
class Menu
{
    public static void MenuStart()
    {
        Program program = new();
        bool QuitProgram = false;
        TimeSpan currentTime = Program.World.Now.TimeOfDay;
        while (QuitProgram == false)
        {
            Program.World.WriteLine("Dit zijn de eerst komende 5 rondleidingen: ");
            Tours.ShowAvailableTours(2, 0);
            Program.World.WriteLine("Scan de streepjescode op uw entreebewijs.");
            string? FirstCustomerCode = Program.World.ReadLine().ToLower();
            if (FirstCustomerCode == "gids")
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
                    // Program.World.WriteLine("Scan de code op uw entreebewijs om een inschrijving te annuleren of toets [q] om terug te gaan naar het begin:");
                    // string? customerCodeToCancel = Program.World.ReadLine();
                    if (FirstCustomerCode == "q")
                    {
                        Program.Main();
                    }
                    if (Tours.CheckIfCanCancel(FirstCustomerCode) is true)
                    {
                        Program.World.WriteLine($"\nDit is de rondleiding die u tot nu toe heeft gereserveerd: \n{Tours.CheckWhatTour(FirstCustomerCode)} \nWeet u zeker dat u deze wilt annuleren? \n[1] Ik wil deze rondleiding annuleren. \n[2] Ik wil toch nog wel mee.");
                        string? JaOfNee = Program.World.ReadLine();
                        if (JaOfNee == "1")
                        {
                            Cancel.CancelAppointment(FirstCustomerCode);
                        }
                        else
                        {
                            Program.Main();
                        }
                    }
                    else
                    {
                        Program.World.WriteLine("U heeft nog geen rondleiding om te annuleren.");
                    }
                    break;
                case "3":
                    Tours.ShowAvailableTours(1, 0);
                    if (Tours.CheckIfCanCancel(FirstCustomerCode) == true)
                    {
                        Program.World.WriteLine($"U heeft voor deze rondleiding gereserveerd: \n{Tours.CheckWhatTour(FirstCustomerCode)}");
                    }
                    Program.World.WriteLine("Druk op [Enter] om terug te gaan.");
                    Program.World.ReadLine();
                    break;
                case "498901":
                    QuitProgram = true;
                    break;
                default:
                    Program.World.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met [1] Rondleiding reserveren, [2] Rondleiding annuleren, [3] Info Rondleidingen.");
                    break;
            }
        }
    }
}