public interface IWorld
{
    DateTime Now { get; }

    void WriteLine(string line);
    void WriteLine(int line);
    void WriteLine(bool line);

    string ReadLine();

    string ReadAllText(string path);

    void WriteAllText(string path, string contents);
}