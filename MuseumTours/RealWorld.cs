namespace Program;

public class RealWorld : IWorld
{
    public DateTime Now
    {
        get => DateTime.Now;
    }

    public void WriteLine(string line)
    {
        Program.World.WriteLine(line);
    }

    public string ReadLine()
    {
        return Program.World.ReadLine();
    }

    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }

    public void WriteAllText(string path, string contents)
    {
        File.WriteAllText(path, contents);
    }
}