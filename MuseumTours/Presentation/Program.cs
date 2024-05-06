namespace Program;

using System;

public class Program
{
  public static void Main()
  {
    TimeSpan startTime = new TimeSpan(10, 30, 0);
    TimeSpan endTime = new TimeSpan(17, 30, 0);
    TimeSpan currentTime = DateTime.Now.TimeOfDay;
    while (currentTime >= startTime && currentTime <= endTime)
    {
      Thread.Sleep(2500);
      Console.WriteLine("\n=========================================================================");
      Menu.MainProgram();
    }
    Console.WriteLine("Het museum is gesloten.");
  }
}


