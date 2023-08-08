namespace Game2048.Gameplay;

public interface IScoreGainer
{
    void AddScore(int score);
    void Reset();
}
