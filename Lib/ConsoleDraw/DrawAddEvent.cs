using System;
using static System.Console;

namespace Lib.ConsoleDraw;

public class DrawAddEvent
{
    private readonly DateTime addDate;
    
    DrawAddEvent(DateTime requestedDate)
    {
        addDate = requestedDate;
    }

    private void DrawHeader()
    {
        WriteLine($"{"",-37}{addDate:dd}  {addDate:MMMM}  {addDate:yyyy}");
        
    }
}
