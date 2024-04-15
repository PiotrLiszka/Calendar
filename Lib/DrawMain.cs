using System;
using System.Collections.Generic;
using static System.Console;
using static System.Threading.Thread;
namespace Lib;

public class DrawMain : CalendarDraw
{
    private State state = new State();

    public DrawMain(DateTime requestedDate)   
    {
        controlKeys = [ConsoleKey.Q, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.A, ConsoleKey.Escape];
        RequestedDate = requestedDate;
        ListOfKeys.AddRange(controlKeys);
        state = State.MainCalendar;
    }

    private enum State
    {
        MainCalendar,
        ControlView
    }

    public override void OnKeyPressed(object source, KeyControlsEventArgs e)
    {
        if (state == State.MainCalendar)
        {
            switch (e.PressedKey.key.Key)
            {
                case ConsoleKey.LeftArrow:
                    RequestedDate = RequestedDate.AddMonths(-1);
                    Draw();
                    break;
                case ConsoleKey.RightArrow:
                    RequestedDate = RequestedDate.AddMonths(1);
                    Draw();
                    break;
                case ConsoleKey.A:
                    break;
                case ConsoleKey.Escape:
                    break;
                case ConsoleKey.Q:
                    state = State.ControlView;
                    DrawControls();
                    break;
                default:
                    break;
            }
        }
        else if (state == State.ControlView)
        {
            switch (e.PressedKey.key.Key)
            {
                case ConsoleKey.Escape:
                    state = State.MainCalendar;
                    Draw();
                    break;
                default:
                    OnCalendarDrawn(this);
                    break;
            }
        }
    }

    private void DrawControls()
    {
        Console.Clear();
        System.Console.WriteLine("[<-]\t\tPoprzedni miesiąc\n\n[->]\t\tNastępny miesiąc\n\n[A]\t\tDodaj wydarzenie\n\n[P]\t\tPokaż wydarzenia");
        System.Console.WriteLine("\n\n\n\n\n\n[ESC]\tWróć do kalendarza");
        OnCalendarDrawn(this);
    }
    /// <summary>
    /// Rysuje puste pole w kalendarzu
    /// </summary>
    private static void DrawEmpty()
    {
        Console.ForegroundColor = ConsoleColor.White;
        (int Left, int Top) = Console.GetCursorPosition();
        Write($"|{"",-13}");
        Console.SetCursorPosition(Left, Top + 1);
        Write($"|{"",-13}");
        Console.SetCursorPosition(Left, Top + 2);
        Write($"|{"",-13}");
        Console.SetCursorPosition(Left + 14, Top);
    }
    
    /// <summary>
    /// Rysuje prawą krawędź kalendarza i "podłogę"
    /// </summary>
    /// <param name="floor">Pozioma linia dzieląca tygodnie</param>
    private void DrawEndOfWeek()
    {
        Console.ForegroundColor = ConsoleColor.White;
        (int Left, int Top) = Console.GetCursorPosition();
        Write("|");
        Console.SetCursorPosition(Left, Top + 1);
        Write("|");
        Console.SetCursorPosition(Left, Top + 2);
        Write("|");
        Console.SetCursorPosition(0, Top + 3);
        WriteLine(floor);
    }

    protected override void DrawHeader()
    {
        WriteLine($"{"",-42}{RequestedDate:MMMM}  {RequestedDate:yyyy}");
        WriteLine(floor);
    }
    /// <summary>
    /// Główna klasa rysująca konkretną opcję kalendarza
    /// </summary>
    public override void Draw()     // jest dramatyczna, ale działa dobrze...
    {
        Dictionary<int,int> monthData = DataForMonth(RequestedDate);   // słownik z danymi dla miesiąca
        Sleep(10);
        string[] days = ["Pon", "Wto", "Śro", "Czw", "Pią", "Sob", "Ndz"];
        Console.Clear();

        Sleep(2);
        DrawHeader();

        Write("|");

        foreach (string d in days)
        {
            Write($"{"",-5}{d}{"",5}|");    //  szerokość kolumny - 13 znaków + |
        }
        WriteLine();                        //  koniec 1 wiersza
        WriteLine(floor);
        
        byte dayNr = 1;
        byte dayFields = 35;
        byte firstDay = (byte)RequestedDate.DayOfWeek;

        if (firstDay == 0) firstDay = 7;           //   zamiana enum niedzieli z 0 na 7
        // sprawdzam ile jest potrzebnych pól do narysowania kalendarza (domyślnie 35)
        if (dayFields < (firstDay + (byte)DateTime.DaysInMonth(year: RequestedDate.Year, month: RequestedDate.Month) - 1))
            dayFields = 35 + 7;

        for (int day = 1; day <= dayFields; day++)
        {
            //  wypisywanie numerów dni w "komórkach" odpowiadającym ich dniu tygodnia
            if (day >= firstDay && day < (firstDay + (int)DateTime.DaysInMonth(year: RequestedDate.Year, month: RequestedDate.Month)))
            {
                (int Left, int Top) = Console.GetCursorPosition();

                Console.ForegroundColor = ConsoleColor.White;
                Write($"|  ");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Write($"{dayNr,-11}");

                Console.SetCursorPosition(Left, Top + 1);
                Console.ForegroundColor = ConsoleColor.White;
                Write($"|{"",-13}");

                Console.SetCursorPosition(Left, Top + 2);

                if (monthData.TryGetValue(dayNr, out int zdarzenia))    // wypisywanie ilości zdarzeń pod odpowednim dniem
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Write($"| ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Write($"Zdarzeń: {zdarzenia}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Write($"|{"",-13}");
                }
                
                Console.SetCursorPosition(Left + 14, Top);
                dayNr++;
            }
            else
            {
                DrawEmpty();
            }

            if (day % 7 == 0)
            {
                DrawEndOfWeek();
            }
        }

        DrawBasicControls();

        
        OnCalendarDrawn(this);
    }
    protected override void OnCalendarDrawn(CalendarDraw calendar)
    {
        base.OnCalendarDrawn(this);
    }
}