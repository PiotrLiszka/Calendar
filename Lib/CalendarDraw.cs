using System; 
using System.Collections.Generic;

namespace Lib;

public class DrawEventArgs : EventArgs
{
    public CalendarDraw? KeyEvents { get; set; }
}

public abstract class CalendarDraw : DrawDataFetch
{
    protected DateTime RequestedDate { get; set; }
    protected ConsoleKey[]? controlKeys;
    protected readonly string floor = "__________________________________________________________________________________________________";

    internal List<ConsoleKey> ListOfKeys { get; set; } = new List<ConsoleKey>();

    //  Eventy odpowiadające za przekazanie odpowiednich klawiszy do klasy KeyControls
    public delegate void DrawEventHandler(object sender, DrawEventArgs e);
    public event DrawEventHandler? CalendarDrawn;

    public abstract void OnKeyPressed(object source, KeyControlsEventArgs e);

    public abstract void Draw();
    protected abstract void DrawHeader();
    protected virtual void DrawBasicControls()
    {
        System.Console.Write("[Q] Skróty klawiszowe\t [ESC] Wyjdź/Powrót");
    }
    
    protected virtual void OnCalendarDrawn(CalendarDraw calendar)
    {
        CalendarDrawn?.Invoke(this, new DrawEventArgs(){KeyEvents = calendar});
    }
}