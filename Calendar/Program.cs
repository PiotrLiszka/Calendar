using Lib;
using Lib.ConsoleDraw;

// TODO:
// FileCheck.CheckDBFile(@"../DB/calendar.db");
// using (var filecheck = new FileCheck())
// {
// filecheck.CheckDBFile(@"../DB/calendar.db");
// }

Console.Title = "Calendar";

DateTime dateNow = DateTime.Now;
DateTime viewerMonth = dateNow.AddDays(-dateNow.Day + 1);   // pierwszy dzień aktualnego miesiąca
// DateTime viewerMonth = new(2024, 03, 15, 13, 22, 11);
// viewerMonth = viewerMonth.AddDays(-viewerMonth.Day + 1);    // pierwszy dzień testowego miesiąca

// !!!! TODO: Przekazywanie tablicy/listy/słownika controlsów w argumentach klasy, zamiast wypisywać na sztywno w klasie!

//  DBControl.DBControlClass.Test();

DrawMain drawCalendar = new(viewerMonth);
DrawDay drawDay = new(viewerMonth);

letsGo(drawCalendar);

void letsGo(CalendarDraw yes)
{
    yes.Draw();
}

