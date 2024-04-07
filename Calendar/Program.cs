using static System.Console;

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


// var sqlite_conn = DBControlClass.CreateConnection();
// WriteLine($"Connection to {sqlite_conn.DataSource} database is {sqlite_conn.State} !");  //  przenoszone do drawdatafetch

// DBControlClass.AddToDB(new DateTime(2024, 03, 15, 13, 22, 11), "Mama miała placek", 3,sqlite_conn);        // TESTING
// DBControlClass.AddToDB(new DateTime(2024, 03, 17, 14, 22, 11), "Mama miała placek2", 1,sqlite_conn); 
// DBControlClass.AddToDB(new DateTime(2024, 03, 02, 15, 22, 11), "Mama miała placek3", 2,sqlite_conn);
// DBControlClass.AddToDB(new DateTime(2024, 03, 02, 15, 22, 11), "Mama miała placek3", 2,sqlite_conn);
// DBControlClass.AddToDB(new DateTime(2024, 03, 07, 15, 22, 11), "Mama miała placek3", 2,sqlite_conn);

CalendarDraw calendarDraw = new(viewerMonth);   //  DONE NIE USUWAĆ
KeyControls keyControls = new();

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
calendarDraw.CalendarDrawn += keyControls.OnCalendarDrawn;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
calendarDraw.DrawMain();





