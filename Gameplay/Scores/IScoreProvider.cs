namespace Game2048.Gameplay.Scores;

public interface IScoreProvider
{
    int MaxScore { get; }
    int Score { get; }
}