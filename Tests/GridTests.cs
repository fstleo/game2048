using Game2048;
using Game2048.Gameplay.Field;

namespace Tests;

public class GridTests
{
    private Grid _grid;
    
    [SetUp]
    public void Setup()
    {
        _grid = new Grid(4, 2048);
    }

    [Test]
    public void On_left_numbers_move_left()
    {
        _grid.Map[3, 1] = 2;
        _grid.Left();
        Assert.AreEqual(2, _grid.Map[0, 1]);
    }
    
    [Test]
    public void On_right_numbers_move_right()
    {
        _grid.Map[0, 0] = 2;
        _grid.Right();
        Assert.AreEqual(2, _grid.Map[3, 0]);
    }
    
    [Test]
    public void On_up_numbers_move_up()
    {
        _grid.Map[1, 1] = 2;
        _grid.Up();
        Assert.AreEqual(2, _grid.Map[1, 0]);
    }
    
    [Test]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(8)]
    [TestCase(16)]
    [TestCase(32)]
    [TestCase(64)]
    [TestCase(128)]
    [TestCase(256)]
    [TestCase(512)]
    [TestCase(1024)]
    public void On_merge_get_score_equal_to_merged_sum(int tile)
    {
        var expected = tile * 2;
        _grid.Map[1, 1] = tile;
        _grid.Map[1, 2] = tile;
        var score = _grid.Up();
        Assert.AreEqual(expected, score);
    }
    
    [Test]
    public void On_merge_get_score()
    {
        _grid.Map[1, 1] = 2;
        _grid.Map[1, 2] = 2;
        var score = _grid.Up();
        Assert.AreEqual(4, score);
    }
    
    [Test]
    public void Score_is_negative_if_cant_move()
    {
        _grid.Map[1, 0] = 2;
        var score = _grid.Up();
        Assert.AreEqual(-1, score);
    }
    
    [Test]
    public void On_down_numbers_move_down()
    {
        _grid.Map[0, 0] = 2;
        _grid.Down();
        Assert.AreEqual(2, _grid.Map[0, 3]);
    }
    
        
    [Test]
    public void On_left_numbers_merge_from_closest_to_the_left()
    {
        _grid.Map[0, 1] = 2;
        _grid.Map[1, 1] = 2;
        _grid.Map[2, 1] = 2;
        _grid.Left();
        Assert.AreEqual(4, _grid.Map[0, 1]);
    }
    
    [Test]
    public void On_left_numbers_merge()
    {
        _grid.Map[0, 1] = 2;
        _grid.Map[1, 1] = 2;
        _grid.Map[2, 1] = 2;
        _grid.Map[3, 1] = 2;
        _grid.Left();
        Assert.AreEqual(4, _grid.Map[0, 1]);
        Assert.AreEqual(4, _grid.Map[1, 1]);
        _grid.Left();
        Assert.AreEqual(8, _grid.Map[0, 1]);
    }
    
        
    [Test]
    public void On_right_numbers_merge()
    {
        _grid.Map[0, 1] = 2;
        _grid.Map[1, 1] = 2;
        _grid.Map[2, 1] = 2;
        _grid.Map[3, 1] = 2;
        _grid.Right();
        Assert.AreEqual(4, _grid.Map[2, 1]);
        Assert.AreEqual(4, _grid.Map[3, 1]);
        _grid.Right();
        Assert.AreEqual(8, _grid.Map[3, 1]);
    }
    
    [Test]
    public void On_up_numbers_merge()
    {
        _grid.Map[1, 0] = 2;
        _grid.Map[1, 1] = 2;
        _grid.Map[1, 2] = 2;
        _grid.Map[1, 3] = 2;
        _grid.Up();
        Assert.AreEqual(4, _grid.Map[1, 0]);
        Assert.AreEqual(4, _grid.Map[1, 1]);
        _grid.Up();
        Assert.AreEqual(8, _grid.Map[1, 0]);
    }

    [Test]
    public void On_down_numbers_merge()
    {
        _grid.Map[1, 0] = 2;
        _grid.Map[1, 1] = 2;
        _grid.Map[1, 2] = 2;
        _grid.Map[1, 3] = 2;
        _grid.Down();
        Assert.AreEqual(4, _grid.Map[1, 2]);
        Assert.AreEqual(4, _grid.Map[1, 3]);
        _grid.Down();
        Assert.AreEqual(8, _grid.Map[1, 3]);
    }


    [Test]
    public void If_merge_two_1024_has_2048_is_true()
    {
        _grid.Map[1, 0] = _grid.TargetNumber/2;
        _grid.Map[1, 1] = _grid.TargetNumber/2;
        _grid.Down();
        Assert.IsTrue(_grid.WinConditionMet);
    }

    
    [Test]
    public void Get_number_returns_number_from_the_map()
    {
        _grid.Map[1, 0] = 16;
        _grid.Map[1, 1] = 32;
        
        Assert.AreEqual(16, _grid.GetNumberAt(1,0));
        Assert.AreEqual(32, _grid.GetNumberAt(1,1));
    }
    
    [Test]
    public void Cant_swipe_if_non_of_close_numbers_are_equal()
    {
        FillNonswipableNumbers();
        
        Assert.IsFalse(_grid.CanSwipe());
    }

    private void FillNonswipableNumbers()
    {
        for (int i = 0; i < _grid.Size; i++)
        for (int j = 0; j < _grid.Size; j++)
        {
            _grid.Map[i, j] = (int)Math.Pow(2, (j + 1) * (i + 1) );
        }

    }
    
    [Test]
    public void Can_swipe_if_grid_is_full_but_has_close_equal_numbers()
    {
        FillNonswipableNumbers();

        _grid.Map[2, 2] = _grid.Map[2, 1];
        
        Assert.IsTrue(_grid.CanSwipe());
    }
    
    [Test]
    public void Place_randomly_put_one_number()
    {
        _grid.PlaceRandomly();
        
        int numbersCount = 0;
        for (int i = 0; i < _grid.Size; i++)
        for (int j = 0; j < _grid.Size; j++)
        {
            if (_grid.Map[i, j] != 0)
            {
                numbersCount++;
            }
        }
        Assert.AreEqual(1, numbersCount);
    }
    
    [Test]
    public void Can_swipe_if_has_an_empty_space()
    {
        FillNonswipableNumbers();

        _grid.Map[2, 2] = 0;
        
        Assert.IsTrue(_grid.CanSwipe());
    }
}
