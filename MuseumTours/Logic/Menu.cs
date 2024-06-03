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
            Console.WriteLine("Scan de streepjescode op uw entreebewijs.");
            string? FirstCustomerCode = Console.ReadLine().ToLower();
            if (FirstCustomerCode == "abcd")
            {
                Guide.CheckEmployeeID();
            }
            else if (Customer.CheckIfCustomerInList(FirstCustomerCode) == false && Tours.CheckIfCanCancel(FirstCustomerCode) == false)
            {
                Console.WriteLine($"Uw code klopt niet. Dit was de code die u invulde: {FirstCustomerCode}");
                Program.Main();
            }
            Console.WriteLine("\nWat wilt u doen?");
            Console.WriteLine($"[1] Rondleiding reserveren \n[2] Rondleiding annuleren \n[3] Info rondleidingen\n");
            string answer = Console.ReadLine().ToLower();
            if (FirstCustomerCode == "abcd")
            {
                Guide.CheckEmployeeID();
                break;
            }
            switch (answer)
            {
                case "1":
                    Valid_Answer = true;
                    Tours.InputMoreCustomercodes(FirstCustomerCode);
                    break;
                case "2":
                    Valid_Answer = true;
                    // Console.WriteLine("Scan de code op uw entreebewijs om een inschrijving te annuleren of toets [q] om terug te gaan naar het begin:");
                    // string? customerCodeToCancel = Console.ReadLine();
                    if (FirstCustomerCode == "q")
                    {
                        Program.Main();
                    }
                    if (Tours.CheckIfCanCancel(FirstCustomerCode) is true)
                    {
                        Console.WriteLine($"\nDit is de rondleiding die u tot nu toe heeft gereserveerd: \n{Tours.CheckWhatTour(FirstCustomerCode)} \nWeet u zeker dat u deze wilt annuleren? \n[1] Ik wil deze rondleiding annuleren. \n[2] Ik wil toch nog wel mee.");
                        string? JaOfNee = Console.ReadLine();
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
                        Console.WriteLine("U heeft nog geen rondleiding om te annuleren.");
                    }
                    break;
                case "3":
                    Valid_Answer = true;
                    string ShowTourOption3 = Tours.CheckWhatTour(FirstCustomerCode);
                    Console.WriteLine($"U heeft gereserveerd voor de rondleiding: \n{ShowTourOption3}");
                    Console.WriteLine("Als u het gelezen heeft toets dan [q] om terug te gaan naar het begin.");
                    string choice = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met [1] Rondleiding reserveren, [2] Rondleiding annuleren, [3] Info Rondleidingen of [4] Inloggen werknemers).");
                    break;
            }
        }
    }
}

