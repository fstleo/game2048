namespace Game2048.Gameplay;

public interface IGameStateHandler
{
    void ReturnToGame();
    void GameOver();
    void Restart();
    void Quit();
    void Win();
}