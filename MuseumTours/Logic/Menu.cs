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
                    string? customerCodeToCancel = Console.ReadLine();
                    if (customerCodeToCancel == "q")
                    {
                        MainProgram();
                    }
                    if (Tours.CheckIfCanCancel(customerCodeToCancel) is true)
                    {
                        Console.WriteLine($"Weet u zeker dat u niet meer mee wilt met deze rondleiding: \n{Tours.CheckWhatTour(customerCodeToCancel)} \n[1] Ja ik wil niet meer mee. \n[2] Ik wil toch wel nog mee.");
                        string? JaOfNee = Console.ReadLine();
                        if (JaOfNee == "1")
                        {
                            Cancel.CancelAppointment(customerCodeToCancel);
                        }
                        else
                        {
                            MainProgram();
                        }
                    }
                    break;
                case "3":
                    Valid_Answer = true;
                    Tours.ShowAvailableTours(1, 0);
                    break;
                case "4":
                    Valid_Answer = true;
                    Guide.CheckEmployeeID();
                    break;
                case "2468":
                    Valid_Answer = true;
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met '1'(Rondleiding reserveren), '2'(Rondleiding annuleren), '3'(Info Rondleidingen) of '4'(Inloggen werknemers)).");
                    break;
            }
        }
    }
}

