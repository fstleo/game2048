namespace Game2048.Gameplay;

public class GameStateHandler : IGameStateHandler, IGameStateProvider
{
    private GameState _state;


    public GameState State
    {
        get => _state;
        private set
        {
            if (_state == value)
            {
                return;
            }
            _state = value;
        }
    }
    
    public void ReturnToGame()
    {
        State = GameState.Playing;
    }

    public void GameOver()
    {
        State = GameState.GameOver;
    }

    public void Restart()
    {
        State = GameState.Restarting;
    }

    public void Quit()
    {
        State = GameState.Quitting;
    }

    public void Win()
    {
        State = GameState.Win;
    }
}