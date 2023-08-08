using Game2048.Common;
using Game2048.Gameplay;
using Game2048.Gameplay.ConsoleInput;
using Game2048.Gameplay.Field;
using Game2048.Gameplay.Field.View;
using Game2048.Gameplay.Scores;
using Game2048.Gameplay.View;

var grid = new Grid(4, 2048);
var scores = new ScoreKeeper(new FileStorage("max_scores"));
var gameStateHandler = new GameStateHandler();
var game = new Game(gameStateHandler, grid, scores);
var input = new ConsoleInputReader();

var gameView = new CompositeView(new ScoreView(scores), new GridView(grid), new GameStateView(gameStateHandler, game, input));

game.StartNew();

while (true)
{
    gameView.Show();
}
    