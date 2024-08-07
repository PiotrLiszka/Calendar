using System;
using Lib.ConsoleDraw;

namespace Lib;

public static class KeyControls
{
    public static ConsoleKeyInfo PressedKey { get; set; }

    public static void OnCalendarDrawn(CalendarDraw source)
    {
        // Console.WriteLine("Czekam na przycisk...");
        do
        {
            PressedKey = Console.ReadKey(true);
        }
        while (!source.ListOfKeys.Contains(PressedKey.Key) && !source.BaseKeys.Contains(PressedKey.Key));

        source.OnKeyPressed();
    }

}