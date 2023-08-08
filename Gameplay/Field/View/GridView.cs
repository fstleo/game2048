using System.Text;

namespace Game2048.Gameplay.Field.View;

public class GridView : IView
{
    private readonly Dictionary<int, ConsoleColor> _colors = new();
    private readonly Dictionary<int, string> _numbers = new();
    
    private string _separator;
    
    private readonly IGrid _grid;
    
    public GridView(IGrid grid)
    {
        _grid = grid;
        CreateSeparator();
        InitializeNumbers();
    }
    
    private void CreateSeparator()
    {
        var separatorBuilder = new StringBuilder();
        for (int i = 0; i < _grid.Size; i++)
        {
            separatorBuilder.Append("|----");
        }
        separatorBuilder.Append('|');
        _separator = separatorBuilder.ToString();
    }

    private void InitializeNumbers()
    {
        _numbers[0] = "    ";
        _colors[0] = ConsoleColor.Black;
        
        for (int number = 2; number <= _grid.TargetNumber; number *= 2)
        {
            _numbers.Add(number, number.ToString().PadLeft(4));
            _colors[number] = (ConsoleColor)(Math.Log2(number) % 15 + 1);
        }
    }
    
    private void Separator()
    {
        Console.WriteLine(_separator);
    }
    
    private void PrintNumber(int number)
    {
        Console.Write('|');
        Console.ForegroundColor = _colors[number];
        Console.Write(_numbers[number]);
        Console.ResetColor();
    }

    public void Show()
    {
        for (int i = 0; i < _grid.Size; i++)
        {
            Separator();
            for (int j = 0; j < _grid.Size; j++)
            {
                PrintNumber(_grid.GetNumberAt(j, i));
            }
            Console.WriteLine('|');
        }        
        Separator();
    }


}
