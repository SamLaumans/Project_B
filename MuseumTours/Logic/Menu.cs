namespace Program;
class Menu
{
    public static void MainProgram()
    {
        Program program = new();
        bool Valid_Answer = false;
        while (Valid_Answer == false)
        {
            Console.WriteLine("Dit zijn de eerst komende 5 rondleidingen: ");
            Tours.ShowAvailableTours(2, 0);
            Console.WriteLine("\nWat wilt u doen?");
            Console.WriteLine($"[1] Rondleiding reserveren \n[2] Rondleiding annuleren \n[3] Info rondleidingen\n[4] Inloggen werknemers");
            string answer = Console.ReadLine().ToLower();
            switch (answer)
            {
                case "1":
                    Valid_Answer = true;
                    Tours.InputMoreCustomercodes();
                    break;
                case "2":
                    Valid_Answer = true;
                    Console.WriteLine("Scan de code op uw ticket om een inschrijving te annuleren of toets 'q' om terug te gaan naar het begin:");
                    string customerCodeToCancel = Console.ReadLine();
                    if (customerCodeToCancel == "q")
                    {
                        MainProgram();
                    }
                    Cancel.CancelAppointment(customerCodeToCancel);
                    break;
                case "3":
                    Valid_Answer = true;
                    Tours.ShowAvailableTours(1, 0);
                    break;
                case "4":
                    Valid_Answer = true;
                    Guide.CheckEmployeeID();
                    break;
                default:
                    Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met '1'(Rondleiding reserveren), '2'(Rondleiding annuleren), '3'(Info Rondleidingen) of '4'(Inloggen werknemers)).");
                    break;
            }
        }
    }
}

