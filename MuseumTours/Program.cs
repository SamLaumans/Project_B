namespace Program;

using System;

public class Program
{
  public static IWorld World = new RealWorld();
  public static void Main()
  {
    Guide.Init();
    Menu.MenuStart();
  }
}


