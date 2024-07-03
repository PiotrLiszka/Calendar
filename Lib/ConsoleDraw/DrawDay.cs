using System;
using DBControl;
using System.Data;
using static System.Console;

namespace Lib.ConsoleDraw;

public class DrawDay : CalendarDraw
{

    // TODO: zmienić z drawday na drawevents
    //  w zależności od wartości przekazanej w konstruktorze, wypisywać eventy dla dnia albo całego miesiąca

    public DrawDay(DateTime requestedDate)
    {
        RequestedDate = requestedDate;
        controlKeys = [ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.A, ConsoleKey.P];
        ListOfKeys.AddRange(controlKeys);
        state = State.MainView;
    }
    public override void OnKeyPressed()
    {
        if (state == State.MainView)
        {
            switch (KeyControls.PressedKey.Key)
            {
                case ConsoleKey.LeftArrow:
                    RequestedDate = RequestedDate.AddDays(-1);
                    Draw();
                    break;
                case ConsoleKey.RightArrow:
                    RequestedDate = RequestedDate.AddDays(1);
                    Draw();
                    break;
                // case ConsoleKey.A:
                //     //  TODO (dodawanie zdarzenia)
                //     break;
                case ConsoleKey.Q:
                    state = State.ControlView;
                    DrawControls();
                    break;
                case ConsoleKey.Escape:
                    state = State.MainView;
                    return;
                default:
                    Draw();
                    break;
            }
        }
        else
        {
            switch (KeyControls.PressedKey.Key)
            {
                case ConsoleKey.Escape:
                    state = State.MainView;
                    Draw();
                    break;
                default:
                    DrawControls();
                    break;
            }
        }

    }

    protected override void DrawHeader()    // OK
    {
        WriteLine($"{"",-37}{RequestedDate:dd}  {RequestedDate:MMMM}  {RequestedDate:yyyy}");
        WriteLine(floor + "\n");
    }

    protected override void DrawControls()
    {
        Console.Clear();
        DrawControls controls = new DrawControls(ListOfKeys, this);
        controls.Draw();
        WriteLine("\n\n\n\n\n\n[ESC]\tWróć do kalendarza");
        OnCalendarDrawn(this);
    }

    protected void WriteEvents()    // TODO : poprawa ułożenia w UI
    {
        var sqlite_conn = DBQueries.CreateConnection();
        DataTable SelectResults = DBQueries.SelectForDay(RequestedDate, sqlite_conn);

        if (SelectResults.Rows.Count > 0)
        {
            for (int row = 0; row < SelectResults.Rows.Count; row++)
            {
                DataRow results = SelectResults.Rows[row];
                TimeOnly time = TimeOnly.FromDateTime(DateTime.Parse(results.ItemArray[2].ToString()));    // zmiana wartości z SQLite (string) na TimeOnly
                Write($"{time}\t{results.ItemArray[3]}\t{results.ItemArray[4]}");               // wypisywanie danych : czas, tekst wydarzenia[3] i priorytet[4]

                WriteLine();
            }
        }
        else
        {
            WriteLine("\nW wybranym dniu nie ma żadnych wydarzeń.");
        }

        sqlite_conn.Close();
    }

    public override void Draw()
    {
        Console.Clear();
        DrawHeader();
        WriteEvents();
        Write("\n\n\n");
        DrawBasicControls();

        base.OnCalendarDrawn(this);
        //ReadLine();
    }

}
