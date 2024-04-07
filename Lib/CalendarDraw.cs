using static System.Console;

namespace Lib;


public class CalendarDraw : DrawDataFetch
{
    public DateTime RequestedDate { get; set; }
    public event EventHandler? CalendarDrawn;
    public CalendarDraw(DateTime date)
    {
        RequestedDate = date;
    }

    public void OnKeyPressed(object source, EventArgs e)
    {

    }
 

    /// <summary>
    /// Metoda "rysująca" główny widok kalendarza w konsoli
    /// </summary>
    /// <param name="RequestedDate">Wczytanie aktualnego dnia/miesiąca</param>
    public void DrawMain()     // jest dramatyczna, ale działa dobrze...
    {
        Dictionary<int,int> monthData = DataForMonth(RequestedDate);   // słownik z danymi dla miesiąca

        string floor = "__________________________________________________________________________________________________";
        string[] days = ["Pon", "Wto", "Śro", "Czw", "Pią", "Sob", "Ndz"];
        Console.Clear();
        WriteLine($"{"",-42}{RequestedDate:MMMM}  {RequestedDate:yyyy}");
        WriteLine(floor);
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
        if (dayFields < (firstDay + (byte)DateTime.DaysInMonth(year: RequestedDate.Year, month: RequestedDate.Month)))
            dayFields = 35 + 7;


        for (int day = 1; day <= dayFields; day++)
        {
            //  wypisywanie numerów dni w "komórkach" odpowiadającym ich dniu tygodnia
            if (day >= firstDay && day < (firstDay + (int)DateTime.DaysInMonth(year: RequestedDate.Year, month: RequestedDate.Month)))
            {
                (int Left, int Top) CurPos = Console.GetCursorPosition();

                Console.ForegroundColor = ConsoleColor.White;
                Write($"|  ");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Write($"{dayNr,-11}");

                Console.SetCursorPosition(CurPos.Left, CurPos.Top + 1);
                Console.ForegroundColor = ConsoleColor.White;
                Write($"|{"",-13}");

                Console.SetCursorPosition(CurPos.Left, CurPos.Top + 2);

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
                
                Console.SetCursorPosition(CurPos.Left + 14, CurPos.Top);
                dayNr++;
            }
            else        // jeśli konkretny dzień tygodnia należy do poprzedniego/następnego miesiąca - rysuje pustą komórkę
            {
                Console.ForegroundColor = ConsoleColor.White;
                (int Left, int Top) CurPos = Console.GetCursorPosition();
                Write($"|{"",-13}");
                Console.SetCursorPosition(CurPos.Left, CurPos.Top + 1);
                Write($"|{"",-13}");
                Console.SetCursorPosition(CurPos.Left, CurPos.Top + 2);
                Write($"|{"",-13}");
                Console.SetCursorPosition(CurPos.Left + 14, CurPos.Top);
            }

            if (day % 7 == 0)       // rysowanie krawędzi i rozpoczęcie kolejnego wiersza kalendarza
            {
                Console.ForegroundColor = ConsoleColor.White;
                (int Left, int Top) CurPos = Console.GetCursorPosition();
                Write("|");
                Console.SetCursorPosition(CurPos.Left, CurPos.Top + 1);
                Write("|");
                Console.SetCursorPosition(CurPos.Left, CurPos.Top + 2);
                Write("|");
                Console.SetCursorPosition(0, CurPos.Top + 3);
                WriteLine(floor);
            }
        }

        Write(" <- Poprzedni miesiąc -> Następny miesiąc \n");
        
        OnCalendarDrawn();

        ReadLine();
    }
    // TODO: Metoda wypisująca wydarzenia z dnia
    public void DrawDay()
    {

    }
    protected virtual void OnCalendarDrawn()
    {
        CalendarDrawn?.Invoke(this, EventArgs.Empty);
    }
}