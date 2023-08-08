namespace Game2048.Gameplay;

public interface IGameStateProvider
{
    GameState State { get; }
}