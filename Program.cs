using Game2048;

while (true)
{
    var game = new Game(4);

    while (true)
    {
        game.Print();
        var button = Console.ReadKey();
        if (button.Key == ConsoleKey.UpArrow)
        {
            game.Up();
        }
        if (button.Key == ConsoleKey.DownArrow)
        {
            game.Down();
        }
        if (button.Key == ConsoleKey.LeftArrow)
        {
            game.Left();
        }
        if (button.Key == ConsoleKey.RightArrow)
        {
            game.Right();
        }
        if (button.Key == ConsoleKey.Q)
        {
            break;
        }
        if (!game.PlaceRandomly())
        {
            Console.Clear();
            Console.WriteLine("Game over. Restart? y/n");
            var response = Console.ReadKey();
            if (response.Key == ConsoleKey.Y)
            {
                Console.Clear();
                break;
            }
            if (response.Key == ConsoleKey.N)
            {
                Console.Clear();
                return;
            }

        }
        Console.Clear();
    }
}
    