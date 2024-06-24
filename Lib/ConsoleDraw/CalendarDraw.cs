using System;
using System.Collections.Generic;

namespace Lib.ConsoleDraw;

public abstract class CalendarDraw
{
    protected DateTime RequestedDate { get; set; }
    protected ConsoleKey[]? controlKeys;
    protected readonly string floor = "__________________________________________________________________________________________________";

    internal List<ConsoleKey> BaseKeys = [ConsoleKey.Q, ConsoleKey.Escape];
    internal List<ConsoleKey> ListOfKeys = new List<ConsoleKey>();
    protected State state = new();
    protected enum State
    {
        MainView,
        ControlView,
        EventsView 
    }
    

    public abstract void OnKeyPressed();
    public abstract void Draw();
    protected abstract void DrawHeader();
    protected abstract void DrawControls();
    protected virtual void DrawBasicControls()
    {
        System.Console.Write("[Q] Skróty klawiszowe\t [ESC] Powrót");
    }

    // TODO: oprócz DrawDay stworzyć klasę wypisującą wszystkie zdarzenia z miesiąca bez potrzeby wyboru dnia... 
    // TODO: klasa/metoda umożliwiająca edycje wydarzeń   
    protected virtual void OnCalendarDrawn(CalendarDraw calendar)
    {
        KeyControls.OnCalendarDrawn(calendar);
    }
}