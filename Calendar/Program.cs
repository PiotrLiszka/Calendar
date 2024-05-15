using Lib;

// TODO:
// FileCheck.CheckDBFile(@"../DB/calendar.db");
// using (var filecheck = new FileCheck())
// {
// filecheck.CheckDBFile(@"../DB/calendar.db");
// }

Console.Title = "Calendar";

DateTime dateNow = DateTime.Now;
DateTime viewerMonth = new(2024, 03, 15, 13, 22, 11);
viewerMonth = viewerMonth.AddDays(-viewerMonth.Day + 1);    // pierwszy dzień aktualnego miesiąca

// !!!! TODO: Przekazywanie tablicy/listy/słownika controlsów w argumentach klasy, zamiast wypisywać na sztywno w klasie!

//  DBControl.DBControlClass.Test();

DrawMain calendarDraw = new(viewerMonth);
//  DrawDay drawDay = new();        // do implementacji! + dodania nasłuchiwania eventów
KeyControls keyControls = new();

calendarDraw.CalendarDrawn += keyControls.OnCalendarDrawn;
keyControls.KeyPressed += calendarDraw.OnKeyPressed;

calendarDraw.Draw();
