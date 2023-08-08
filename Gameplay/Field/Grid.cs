namespace Game2048.Gameplay.Field;

public interface IGrid
{
    int TargetNumber { get; }
    bool WinConditionMet { get; }
    int Size { get; }
    
    int GetNumberAt(int x, int y);
    void Clear();
    void PlaceRandomly();
    bool CanSwipe();
    int Left();
    int Right();
    int Down();
    int Up();
}

public class Grid : IGrid
{
    public int TargetNumber { get; }
    private readonly List<(int x,int y)> _freeCells;
    public int[,] Map { get; }
    public int Size { get; }
    public bool WinConditionMet { get; private set; }
    private readonly Random _random = new();

    public Grid(int size, int targetNumber)
    {
        TargetNumber = targetNumber;
        Size = size;
        Map = new int[Size, Size];
        
        _freeCells = new List<(int,int)>(Map.Length);
        Clear();
    }

    public int GetNumberAt(int x, int y)
    {
        return Map[x, y];
    }

    public void Clear()
    {
        WinConditionMet = false;
        _freeCells.Clear();
        for (var x = 0; x < Size; x++)
        for (var y = 0; y < Size; y++)
        {
            Map[x, y] = 0;
            _freeCells.Add((x,y));
        }
    }

    private bool IsEmptyAt(int x, int y)
    {
        return Map[x, y] == 0;
    }

    public void PlaceRandomly()
    {
        var freeCellIndex = _random.Next(0, _freeCells.Count);
        var freeCell = _freeCells[freeCellIndex];
        Map[freeCell.x, freeCell.y] = _random.Next(0, 10) == 0 ? 4 : 2;
        _freeCells.RemoveAt(freeCellIndex);
    }

    private bool InBounds(int x, int y)
    {
        return 0 <= x && x < Size && 0 <= y && y < Size;
    }

    public bool CanSwipe()
    {
        return CanSwipe(0, Size - 1, 0, Size, 1, 0) ||
               CanSwipe( Size - 1, 0, 0, Size, -1, 0) ||
               CanSwipe(0, Size, Size - 1, 0, 0, -1) ||
               CanSwipe(0, Size, 0, Size - 1, 0, 1);
    }

    private bool CanSwipe(int xStart, int xEnd, int yStart, int yEnd, int xStep, int yStep)
    {
        var xInc = Math.Sign(xEnd - xStart);
        var yInc = Math.Sign(yEnd - yStart);
        for (var x = xStart; x != xEnd; x += xInc)
        for (var y = yStart; y != yEnd; y += yInc)
        {
            if (CanMove(xStep, yStep, x, y, out var moveToX, out var moveToY))
            {
                return true;
            }
            
            if (CanMerge(xStep, yStep, ref moveToX, ref moveToY, x, y))
            {
                return true;
            }
        }
        return false;
    }

    private int Swipe(int xStart, int xEnd, int yStart, int yEnd, int xStep, int yStep)
    {
        var score = -1;
        var xInc = Math.Sign(xEnd - xStart);
        var yInc = Math.Sign(yEnd - yStart);
        for (var x = xStart; x != xEnd; x += xInc)
        for (var y = yStart; y != yEnd; y += yInc)
        {
            if (CanMove(xStep, yStep, x, y, out var moveToX, out var moveToY))
            {
                Move(x, y, moveToX, moveToY);
                score = Math.Max(0, score);
            }

            if (CanMerge(xStep, yStep, ref moveToX, ref moveToY, x, y))
            {
                var scoreFromCell = Merge(x, y, moveToX, moveToY);
                if (scoreFromCell == TargetNumber)
                {
                    WinConditionMet = true;
                }
                score = Math.Max(0, score) + scoreFromCell;
            }
        }
        return score;
    }

    private void Move(int x, int y, int moveToX, int moveToY)
    {
        Map[x, y] = Map[moveToX, moveToY];
        Map[moveToX, moveToY] = 0;
        _freeCells.Remove((x, y));
        _freeCells.Add((moveToX, moveToY));
    }

    private int Merge(int x, int y, int toMergeX, int toMergeY)
    {
        Map[x, y] *= 2;
        Map[toMergeX, toMergeY] = 0;
        _freeCells.Add((toMergeX, toMergeY));
        return Map[x, y];
    }

    private bool CanMerge(int xStep, int yStep, ref int toMergeX, ref int toMergeY, int x, int y)
    {
        do
        {
            toMergeX += xStep;
            toMergeY += yStep;
        } 
        while (InBounds(toMergeX, toMergeY) && IsEmptyAt(toMergeX, toMergeY));

        return InBounds(toMergeX, toMergeY) && 
               Map[x, y] == Map[toMergeX, toMergeY];
    }

    private bool CanMove(int xStep, int yStep, int x, int y, out int moveToX, out int moveToY)
    {
        moveToX = x;
        moveToY = y;
        while (InBounds(moveToX, moveToY) && IsEmptyAt(moveToX, moveToY))
        {
            moveToX += xStep;
            moveToY += yStep;
        }

        return InBounds(moveToX, moveToY) &&
               IsEmptyAt(x, y) && 
               !IsEmptyAt(moveToX, moveToY);
    }

    public int Left()
    {
        return Swipe(0, Size - 1, 0, Size, 1, 0);
    }
    
    public int Right()
    {
        return Swipe( Size - 1, 0, 0, Size, -1, 0);
    }
    
    public int Down()
    {
        return Swipe(0, Size, Size - 1, 0, 0, -1);
    }
    
    public int Up()
    {
        return Swipe(0, Size, 0, Size - 1, 0, 1);
    }
}
