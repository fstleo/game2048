namespace Game2048.Gameplay.View;

public class GameStateView : IView
{
    private readonly IGameStateProvider _gameStateProvider;
    private readonly Game _game;
    private readonly IInputReader _inputReader;

    public GameStateView(IGameStateProvider gameStateProvider, Game game, IInputReader inputReader)
    {
        _gameStateProvider = gameStateProvider;
        _game = game;
        _inputReader = inputReader;
    }
    
    private void ConfirmRestart()
    {
        Console.WriteLine("Do you want to restart? y/n"); 
        if (_inputReader.ReadConfirmation())
        {
            _game.StartNew();
        }
        else
        {
            _game.ReturnToGame();
        }
    }

    private void ProcessGameEnd()
    {
        if (_inputReader.ReadConfirmation())
        {
            _game.StartNew();
        }
        else
        {
            Environment.Exit(0);
        }
    }

    private void ShowGameOver()
    {
        Console.WriteLine("Game over. Do you want to restart? y/n");
        ProcessGameEnd();
    }

    private void ShowWin()
    {
        Console.WriteLine("You won. Do you want to restart? y/n");
        ProcessGameEnd();
    }

    
    private void ProcessQuit()
    {
        Console.WriteLine("Do you want to quit? y/n");
        if (_inputReader.ReadConfirmation())
        {
            Environment.Exit(0);
        }
        else
        {
            _game.ReturnToGame();
        }
    }
    
    
    public void Show()
    {
        switch (_gameStateProvider.State)
        {
            case GameState.Playing:
                _game.ProcessInput(_inputReader.ReadGameInput());
                break;
            case GameState.Restarting:
                ConfirmRestart();
                break;
            case GameState.GameOver:
                ShowGameOver();
                break;
            case GameState.Quitting:
                ProcessQuit();
                break;
            case GameState.Win:
                ShowWin();
                break;
        }
    }
}
