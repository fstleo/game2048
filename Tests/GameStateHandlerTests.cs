using Game2048.Gameplay;

namespace Tests;

public class GameStateHandlerTests
{
    private GameStateHandler _gameState;
        
    [SetUp]
    public void Setup()
    {
        _gameState = new GameStateHandler();
    }

    [Test]
    public void Win_switches_state_to_win()
    {
        _gameState.Win();
        
        Assert.AreEqual(GameState.Win, _gameState.State);
    }
    
    [Test]
    public void Game_over_switches_state_to_game_over()
    {
        _gameState.GameOver();
        
        Assert.AreEqual(GameState.GameOver, _gameState.State);
    }
    
    [Test]
    public void Restart_switches_state_to_restarting()
    {
        _gameState.Restart();
        
        Assert.AreEqual(GameState.Restarting, _gameState.State);
    }
    
    [Test]
    public void Return_switches_state_to_playing()
    {
        _gameState.ReturnToGame();
        
        Assert.AreEqual(GameState.Playing, _gameState.State);
    }
    
    [Test]
    public void Quit_switches_state_to_quitting()
    {
        _gameState.Quit();
        
        Assert.AreEqual(GameState.Quitting, _gameState.State);
    }
}
