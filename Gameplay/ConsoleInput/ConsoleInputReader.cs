namespace Game2048.Gameplay.ConsoleInput;

public class ConsoleInputReader : IInputReader
{
    public bool ReadConfirmation()
    {
        while (true)
        {
            var button = Console.ReadKey();
            switch (button.Key)
            {
                case ConsoleKey.Y:
                    return true;
                case ConsoleKey.N:
                    return false;
                    break;
            }
        }
    }

    public GameInput ReadGameInput()
    {
        while (true)
        {
            var button = Console.ReadKey();
            switch (button.Key)
            {
                case ConsoleKey.UpArrow:
                    return GameInput.Up;
                case ConsoleKey.DownArrow:
                    return GameInput.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    return GameInput.Left;
                    break;
                case ConsoleKey.RightArrow:
                    return GameInput.Right;
                    break;
                case ConsoleKey.R:
                    return GameInput.Restart;
                    break;
                case ConsoleKey.Q:
                    return GameInput.Exit;
                    break;
                default:
                    return GameInput.Nop;
                    break;
            }
        }
    }
}
