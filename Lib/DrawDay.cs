using System;
using static System.Console;

namespace Lib;

public class DrawDay : CalendarDraw
{

    public DrawDay()
    {
        controlKeys = [ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.A, ConsoleKey.Escape];
        ListOfKeys.AddRange(controlKeys);
    }
    public override void OnKeyPressed(object source, KeyControlsEventArgs e)
    {
        switch (e.PressedKey.key.Key)
        {
            case ConsoleKey.LeftArrow:
                RequestedDate = RequestedDate.AddDays(-1);
                Draw();
                break;
            case ConsoleKey.RightArrow:
                RequestedDate = RequestedDate.AddDays(1);
                Draw();
                break;
            default:
                break;
        }
    }

    protected override void DrawHeader()
    {
        WriteLine($"{"",-37}{RequestedDate:dd}  {RequestedDate:MMMM}  {RequestedDate:yyyy}");
        WriteLine(floor);
    }

    public override void Draw()
    {
        throw new NotImplementedException();
    }

}
