using Game2048.Gameplay.Field;

namespace Game2048.Gameplay;

public class Game
{

    private readonly IGameStateHandler _gameState;
    private readonly IScoreGainer _scoreKeeper;

    private readonly IGrid _grid;

    public Game(IGameStateHandler gameState, IGrid grid, IScoreGainer scoreKeeper)
    {
        _gameState = gameState;
        _scoreKeeper = scoreKeeper;
        _grid = grid;
    }
    
    public void StartNew()
    {
        _scoreKeeper.Reset();
        _grid.Clear();
        
        _grid.PlaceRandomly();
        _grid.PlaceRandomly();

        _gameState.ReturnToGame();
    }
    
    public void ProcessInput(GameInput input)
    {
        var scoreInc = -1;
        switch (input)
        {
            case GameInput.Up:
                scoreInc = _grid.Up();
                break;
            case GameInput.Down:
                scoreInc = _grid.Down();
                break;
            case GameInput.Left:
                scoreInc = _grid.Left();
                break;
            case GameInput.Right:
                scoreInc = _grid.Right();
                break;
            case GameInput.Restart:
                _gameState.Restart();
                break;
            case GameInput.Exit:
                _gameState.Quit();
                break;
        }
        if (scoreInc < 0)
        {
            return;
        }
        _scoreKeeper.AddScore(scoreInc);
        if (_grid.WinConditionMet)
        {
            _gameState.Win();
        }
        else
        {
            PlaceNext();    
        }
    }

    private void PlaceNext()
    {
        _grid.PlaceRandomly();
        if (!_grid.CanSwipe())
        {
            _gameState.GameOver();
        }
    }

    public void ReturnToGame()
    {
        _gameState.ReturnToGame();        
    }
}