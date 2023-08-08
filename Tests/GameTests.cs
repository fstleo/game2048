using Game2048.Gameplay;
using Game2048.Gameplay.Field;
using Game2048.Gameplay.Scores;
using NSubstitute;

namespace Tests;

public class GameTests
{
    private IGameStateHandler _gameState;
    private IGrid _grid;
    private IScoreGainer _score;
    private Game _game;
    
    [SetUp]
    public void Setup()
    {
        _gameState = Substitute.For<IGameStateHandler>();
        
        _grid = Substitute.For<IGrid>();
        _grid.Size.Returns(4);
        _grid.TargetNumber.Returns(2048);

        _score = Substitute.For<IScoreGainer>();
        
        _game = new Game(_gameState, _grid, _score);
    }

    [Test]
    public void Create_new_game_clears_field_score_and_put_two_numbers()
    {
        _game.StartNew();
        
        _grid.Received().Clear();
        _grid.Received(2).PlaceRandomly();
        _score.Received().Reset();
        _gameState.Received().ReturnToGame();
    }
    
    [Test]
    public void Return_to_game_changes_state()
    {
        _game.ReturnToGame();
        
        _gameState.Received().ReturnToGame();
    }


    [Test]
    public void Game_up_input_passed_to_grid()
    {
        _game.ProcessInput(GameInput.Up);

        _grid.Received().Up();
    }

    [Test]
    public void Game_down_input_passed_to_grid()
    {
        _game.ProcessInput(GameInput.Down);

        _grid.Received().Down();
    }
    
    [Test]
    public void Game_left_input_passed_to_grid()
    {
        _game.ProcessInput(GameInput.Left);

        _grid.Received().Left();
    }
    
    [Test]
    public void Game_right_input_passed_to_grid()
    {
        _game.ProcessInput(GameInput.Right);

        _grid.Received().Right();
    }
    
    [Test]
    public void Do_not_place_next_if_no_move()
    {
        _grid.Up().Returns(-1);
        
        _game.ProcessInput(GameInput.Up);

        _score.DidNotReceive().AddScore(Arg.Any<int>());
    }
    
    [Test]
    public void If_score_more_than_0_score_increases()
    {
        _grid.Up().Returns(4);
        
        _game.ProcessInput(GameInput.Up);

        _score.Received().AddScore(4);
    }
    
    [Test]
    public void Restart_changes_state_to_restart()
    {
        _game.ProcessInput(GameInput.Restart);

        _gameState.Received().Restart();
    }
    
    [Test]
    public void Exit_changes_state_to_quitting()
    {
        _game.ProcessInput(GameInput.Exit);

        _gameState.Received().Quit();
    }
    
    [Test]
    public void If_win_condition_met_state_changes_to_win()
    {
        _grid.WinConditionMet.Returns(true);

        _grid.Up().Returns(2048);
        
        _game.ProcessInput(GameInput.Up);
        
        _gameState.Received().Win();
    }
}
