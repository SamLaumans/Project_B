public interface IWorld
{
    DateTime Now { get; }

    void WriteLine(string line);

    void Write(string line);

    string ReadLine();

    void Clear();

    string ReadAllText(string path);

    void WriteAllText(string path, string contents);
}