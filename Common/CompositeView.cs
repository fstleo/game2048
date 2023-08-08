namespace Game2048.Common;

public class CompositeView : IView
{
    private readonly IView[] _views;
    
    public CompositeView(params IView [] views)
    {
        _views = views;
    }

    public void Show()
    {
        Console.Clear();
        foreach (var consoleView in _views)
        {
            consoleView.Show();   
            Console.WriteLine();
        }
    }
}