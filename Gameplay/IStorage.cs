namespace Game2048.Gameplay.Scores;

public interface IStorage<T>
{
    T Load();
    void Save(T value);
}
