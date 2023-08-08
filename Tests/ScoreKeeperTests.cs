using Game2048.Gameplay.Scores;
using NSubstitute;

namespace Tests;

public class ScoreKeeperTests
{
    
    private ScoreKeeper _scoreKeeper;
    private IStorage<int> _storage;
    
    [SetUp]
    public void Setup()
    {
        _storage = Substitute.For<IStorage<int>>();
        _scoreKeeper = new ScoreKeeper(_storage);
    }

    [Test]
    public void Max_score_is_from_storage()
    {
        var value = 50;
        _storage.Load().Returns(value);
        
        Assert.AreEqual(value, _scoreKeeper.MaxScore);
    }
    
    [Test]
    public void If_score_is_more_than_max_score_max_score_updated_and_saved()
    {
        
        _storage.Load().Returns(50);
        
        _scoreKeeper.AddScore(70);

        _storage.Received().Save(70);
        Assert.AreEqual(70, _scoreKeeper.MaxScore);
    }

    [Test]
    public void Add_score_updates_score()
    {
        _scoreKeeper.AddScore(20);
        _scoreKeeper.AddScore(70);

        Assert.AreEqual(90, _scoreKeeper.Score);
    }

    [Test]
    public void Reset_makes_score_0()
    {
        _scoreKeeper.AddScore(70);
        
        _scoreKeeper.Reset();

        Assert.AreEqual(0, _scoreKeeper.Score);
    }
}
