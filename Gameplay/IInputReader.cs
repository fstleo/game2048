namespace Game2048.Gameplay;

public interface IInputReader
{
    bool ReadConfirmation();
    GameInput ReadGameInput();
}
