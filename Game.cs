namespace Game2048;


public class Game
{
    private List<(int x, int y)> _freeCells;
    private readonly int[,] _grid;
    private readonly int _size;
    private Random _random = new();

    public Game(int gridSize)
    {
        _size = gridSize;
        _grid = new int[_size, _size];
        
        _freeCells = new List<(int, int)>(_grid.Length);
        for (int i = 0; i < _size; i++)
        for (int j = 0; j < _size; j++)
        {
            _freeCells.Add((i,j));
        }
        
        for (int i = 0; i < 2; i++)
        {
            PlaceRandomly();
        }
    }

    public bool PlaceRandomly()
    {
        if (_freeCells.Count == 0)
        {
            return false;
        }
        var freeCellIndex = _random.Next(0, _freeCells.Count);
        var freeCell = _freeCells[freeCellIndex];
        _grid[freeCell.x, freeCell.y] = _random.Next(0, 10) == 0 ? 4 : 2;
        _freeCells.RemoveAt(freeCellIndex);
        return true;
    }

    public void Print()
    {
        for (int i = 0; i < _size; i++)
        {
            for (int j = 0; j < _size; j++)
            {
                Console.Write(_grid[i, j].ToString("0000").Replace('0', '_') + ' ');    
            }
            Console.WriteLine();
        }
    }

    private bool InBounds(int x, int y)
    {
        return 0 <= x && x < _size && 0 <= y && y < _size;
    }

    private bool Swipe(int xStart, int xEnd, int yStart, int yEnd, int xStep, int yStep)
    {
        bool moved = false;
        for (int i = xStart; i != xEnd; i += Math.Sign(xEnd - xStart))
        for (int j = yStart; j != yEnd; j += Math.Sign(yEnd - yStart))
        {
            int toMergeX = i;
            int toMergeY = j;
            while (InBounds(toMergeX, toMergeY) && _grid[toMergeX, toMergeY] == 0)
            {
                toMergeX += xStep;
                toMergeY += yStep;
            }

            if (!InBounds(toMergeX, toMergeY))
            {
                //line is empty
                continue;
            }

            if (_grid[i, j] != _grid[toMergeX, toMergeY])
            {
                _grid[i, j] = _grid[toMergeX, toMergeY];
                _grid[toMergeX, toMergeY] = 0;
                _freeCells.Remove((i, j));
                _freeCells.Add((toMergeX, toMergeY));
                moved = true;
            }
            
            do
            {
                toMergeX += xStep;
                toMergeY += yStep;
            } while (InBounds(toMergeX, toMergeY) && _grid[toMergeX, toMergeY] == 0);

            if (!InBounds(toMergeX, toMergeY))
            {
                //nothing to merge
                continue;
            }
            if (_grid[i, j] == _grid[toMergeX, toMergeY])
            {
                _grid[i, j] *= 2;
                _grid[toMergeX, toMergeY] = 0;
                _freeCells.Add((toMergeX, toMergeY));
                moved = true;
            }
        }
        return moved;
    }
    
    public void Up()
    {
        Swipe(0, _size - 1, 0, _size, 1, 0);
    }
    
    public void Down()
    {
        Swipe( _size - 1, 1, 0, _size, -1, 0);
    }
    
    public void Right()
    {
        Swipe(0, _size - 1, _size - 1, 0, 0, -1);
    }
    
    public void Left()
    {
        Swipe(0, _size, 0, _size - 1, 0, 1);
    }
}
