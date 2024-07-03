using System;
using System.Text;
using System.Collections.Generic;

using static System.Console;
using static System.Threading.Thread;

using DBControl;

namespace Lib.ConsoleDraw;

public class DrawMain : CalendarDraw
{
    public bool appRunning;

    public DrawMain(DateTime requestedDate)   // TODO: Implementacja dodawania i wyświetlania zdarzeń
    {
        controlKeys = [ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.A, ConsoleKey.P, ConsoleKey.D];
        RequestedDate = requestedDate;
        ListOfKeys.AddRange(controlKeys);
        state = State.MainView;
        appRunning = true;
    }

    public override void OnKeyPressed()
    {
        if (state == State.MainView)
        {
            switch (KeyControls.PressedKey.Key)
            {
                case ConsoleKey.LeftArrow:
                    RequestedDate = RequestedDate.AddMonths(-1);
                    Draw();
                    return;
                case ConsoleKey.RightArrow:
                    RequestedDate = RequestedDate.AddMonths(1);
                    Draw();
                    return;
                case ConsoleKey.Escape:
                    appRunning = false;
                    return;
                case ConsoleKey.Q:
                    state = State.ControlView;
                    DrawControls();
                    return;
                case ConsoleKey.A:
                // break;
                case ConsoleKey.D:
                    state = State.OtherViews;
                    // DayChoice();
                    DrawDay drawDay = new(RequestedDate.AddDays(DayChoice() - 1));
                    drawDay.Draw();
                    state = State.MainView;
                    break;
                default:
                    // Draw();
                    break;
            }
        }
        else if (state == State.ControlView || state == State.OtherViews)
        {
            switch (KeyControls.PressedKey.Key)
            {
                case ConsoleKey.Escape:
                    state = State.MainView;
                    // Draw();
                    return;
                default:
                    // base.OnCalendarDrawn(this);
                    break;
            }
        }
    }

    protected override void DrawBasicControls()
    {
        System.Console.Write("[Q] Skróty klawiszowe\t [ESC] Wyjście");
    }
    protected override void DrawControls()
    {
        Console.Clear();
        DrawControls controls = new DrawControls(ListOfKeys, this);
        controls.Draw();
        System.Console.WriteLine("\n\n\n\n\n\n[ESC]\tWróć do kalendarza");
        base.OnCalendarDrawn(this);
    }
    /// <summary>
    /// Rysuje puste pole w kalendarzu
    /// </summary>
    private static void DrawEmpty()
    {
        string empty = $"|{"",-13}";
        Console.ForegroundColor = ConsoleColor.White;
        (int Left, int Top) = Console.GetCursorPosition();
        Write(empty);
        Console.SetCursorPosition(Left, Top + 1);
        Write(empty);
        Console.SetCursorPosition(Left, Top + 2);
        Write(empty);
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
        Dictionary<int, int> monthData = DataFetch.DataForMonth(RequestedDate);   // słownik z danymi dla miesiąca
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

        base.OnCalendarDrawn(this);
    }

    /// <summary>
    /// Metoda pozwalająca użytkownikowi wybór dnia do wyświetlenia
    /// </summary>
    /// <returns>Wybrany dzień do wyświetlenia</returns>
    public int DayChoice()
    {
        DateTime dateTime = DateTime.Now;
        int daysInMyMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        StringBuilder dayString = new();
        ConsoleKeyInfo keyCheck;
        bool input = false;
        int day;

        Console.Clear();
        DrawHeader();

        do
        {
            WriteLine($"Wybierz dzień z tego miesiąca do wyświetlenia (1-{daysInMyMonth}): ");
            do
            {
                keyCheck = ReadKey(false);
                dayString.Append(keyCheck.KeyChar);
            }
            while (keyCheck.Key != ConsoleKey.Enter);

            WriteLine(dayString.ToString());
            if (int.TryParse(dayString.ToString(), out day))
            {
                if (day > 0 && day <= daysInMyMonth)
                {
                    input = true;
                }
                else
                {
                    WriteLine("\nDzień spoza zakresu!");
                    dayString.Clear();
                }
            }
            else
            {
                WriteLine("\nWprowadzono błędne dane!");
                dayString.Clear();
            }
        }
        while (!input);

        return day; // do sprawdzenia
    }
}