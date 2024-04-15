using System; 
namespace Lib;

public class KeyControlsEventArgs : EventArgs
{
    internal KeyControls? PressedKey { get; set; }
}

public class KeyControls
{
    public DateTime ViewerDate { get; set; }
    internal ConsoleKeyInfo key;

    public event EventHandler<KeyControlsEventArgs>? KeyPressed;
    public void OnCalendarDrawn(object source, DrawEventArgs e)
    {

        Console.TreatControlCAsInput = true;
        // System.Console.WriteLine("Czekam na przycisk...");
        do
        {
            key = Console.ReadKey(true);
            // System.Console.WriteLine($"{key.Modifiers} + {key.Key}");
        }
        while (!e.KeyEvents.ListOfKeys.Contains(key.Key));

        OnKeyPressed();
    }

    protected virtual void OnKeyPressed()
    {
        KeyPressed?.Invoke(this, new KeyControlsEventArgs() { PressedKey = this });
    }

}