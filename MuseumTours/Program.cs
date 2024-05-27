namespace Program;

using System;

public class Program
{
  public static IWorld World = new RealWorld();
  public static void Main()
  {
    TimeSpan startTime = new TimeSpan(8, 30, 0);
    TimeSpan endTime = new TimeSpan(17, 30, 0);
    TimeSpan currentTime = DateTime.Now.TimeOfDay;
    if (currentTime >= startTime && currentTime <= endTime)
    {
      Menu.menuStart();
    }
    Program.World.WriteLine("Het museum is gesloten.");
  }
}


