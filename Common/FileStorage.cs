using Game2048.Gameplay.Scores;

namespace Game2048.Common;

public class FileStorage : IStorage<int>
{
    private readonly string _fileName;

    public FileStorage(string fileName)
    {
        _fileName = fileName;
    }

    public int Load()
    {
        return File.Exists(_fileName) ? 
            BitConverter.ToInt32(File.ReadAllBytes(_fileName)) :
            0;
    }

    public void Save(int value)
    {
        File.WriteAllBytes(_fileName, BitConverter.GetBytes(value));
    }
}
