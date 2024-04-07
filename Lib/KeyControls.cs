namespace Lib;

public class KeyControls
{
    public DateTime ViewerDate { get; set; }

    public event EventHandler? KeyPressed;
    public void OnCalendarDrawn(object source, EventArgs e)
    {
        System.Console.WriteLine("Czekam na przycisk...");
        ConsoleKeyInfo key = Console.ReadKey(true);
        System.Console.WriteLine($"{key.Modifiers} + {key.Key}");
        
    }

    protected virtual void OnKeyPressed()
    {
        KeyPressed?.Invoke(this, EventArgs.Empty);
    }

}