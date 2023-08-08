namespace Game2048.Gameplay.Scores;

public class ScoreView : IView
{
    private readonly IScoreProvider _scoreProvider;

    public ScoreView(IScoreProvider scoreProvider)
    {
        _scoreProvider = scoreProvider;

    }
    public void Show()
    {
        Console.WriteLine($"Current score {_scoreProvider.Score}");
        Console.WriteLine($"Max score {_scoreProvider.MaxScore}");
    }

}