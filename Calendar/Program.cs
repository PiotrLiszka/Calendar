using Lib;

// FileCheck.CheckDBFile(@"../DB/calendar.db");
// using (var filecheck = new FileCheck())
// {
// filecheck.CheckDBFile(@"../DB/calendar.db");
// }

Console.Title = "Calendar";

DateTime dateNow = DateTime.Now;
DateTime viewerMonth = new(2024, 03, 15, 13, 22, 11);
viewerMonth = viewerMonth.AddDays(-viewerMonth.Day + 1);    // pierwszy dzień aktualnego miesiąca


//  DBControl.DBControlClass.Test();

DrawMain calendarDraw = new(viewerMonth);   //  DONE NIE USUWAĆ
KeyControls keyControls = new();

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
calendarDraw.CalendarDrawn += keyControls.OnCalendarDrawn;
keyControls.KeyPressed += calendarDraw.OnKeyPressed;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

calendarDraw.Draw();
