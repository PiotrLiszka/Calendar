using System;
using static System.Console;

namespace Lib;

public class DrawDay : CalendarDraw
{

    // TODO: CAŁA KLASA!!

    public DrawDay(DateTime requestedDate)
    {
        RequestedDate = requestedDate;
        controlKeys = [ConsoleKey.A, ConsoleKey.Escape];
        ListOfKeys.AddRange(controlKeys);
    }
    public override void OnKeyPressed(object source, KeyControlsEventArgs e)
    {

        switch (e.PressedKey.key.Key)
        {
            case ConsoleKey.A:
                //  TODO (dodawanie zdarzenia)
                break;
            case ConsoleKey.Escape:
                // TODO (powrót do kalendarza)
                break;
            default:
                break;
        }

    }

    protected override void DrawHeader()    // OK
    {
        WriteLine($"{"",-37}{RequestedDate:dd}  {RequestedDate:MMMM}  {RequestedDate:yyyy}");
        WriteLine(floor);
    }

    public override void Draw()
    {
        throw new NotImplementedException();
    }

    protected override void OnCalendarDrawn(CalendarDraw calendar)
    {
        base.OnCalendarDrawn(this);
    }

}
