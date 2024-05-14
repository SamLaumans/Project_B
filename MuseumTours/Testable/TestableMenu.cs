namespace Program;
class TestableMenu
{
    public readonly IWorld World;
    public TestableMenu(IWorld world)
    {
    World = world;
    }
    public void MainProgram()
    {
        bool Valid_Answer = false;
        while (Valid_Answer == false)
        {
            World.WriteLine("Dit zijn de eerst komende 5 rondleidingen: ");
            Tours.ShowAvailableTours(2, 0);
            World.WriteLine("\nWat wilt u doen?");
            World.WriteLine($"[1] Rondleiding reserveren \n[2] Rondleiding annuleren \n[3] Info rondleidingen\n[4] Inloggen werknemers");
            string answer = World.ReadLine().ToLower();
            switch (answer)
            {
                case "1":
                    Valid_Answer = true;
                    Tours.InputMoreCustomercodes();
                    break;
                case "2":
                    Valid_Answer = true;
                    World.WriteLine("Scan de code op uw ticket om een inschrijving te annuleren of toets 'q' om terug te gaan naar het begin:");
                    string customerCodeToCancel = World.ReadLine();
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
                    World.WriteLine("We hebben u niet begrepen, Graag enkel antwoorden met '1'(Rondleiding reserveren), '2'(Rondleiding annuleren), '3'(Info Rondleidingen) of '4'(Inloggen werknemers)).");
                    break;
            }
        }
    }
}

