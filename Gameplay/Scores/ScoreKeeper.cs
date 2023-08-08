namespace Game2048.Gameplay.Scores;

public class ScoreKeeper : IScoreProvider, IScoreGainer
{
    private readonly IStorage<int> _storage;
    private int _maxScore;
    public int Score { get; private set; }

    public int MaxScore
    {
        get
        {
            if (_maxScore < 0)
            {
                _maxScore = _storage.Load();
            }
            return _maxScore;
        }
        private set
        {            
            _maxScore = value;
            _storage.Save(value);
        }
    }

    public ScoreKeeper(IStorage<int> storage)
    {
        _storage = storage;
        _maxScore = -1;
    }

    public void AddScore(int value)
    {
        Score += value;
        if (Score > MaxScore)
        {
            MaxScore = Score;
        }
    }

    public void Reset()
    {
        Score = 0;
    }
}
