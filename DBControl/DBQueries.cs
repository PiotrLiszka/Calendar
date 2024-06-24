using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using static System.Console;
namespace DBControl;

public class DBQueries
{

   public static void Test () // wypełnianie miesiąca eventami
   {
      var sqlite_conn = CreateConnection();

      for (int i = 1; i < 28; i++)
      {
         DBQueries.AddToDB(new DateTime(2024, 05, i, 13, 22, 11), $"Test event nr {i}", 1,sqlite_conn);
      }
   }

   public static SQLiteConnection CreateConnection()
   {
      // var sqlite_conn = new SQLiteConnection("Data Source=../../../../DB/calendar.db;");
      char sep = Path.DirectorySeparatorChar;
      var sqlite_conn = new SQLiteConnection($"Data Source =" + 
         Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $"VS Projects{sep}Calendar{sep}DB{sep}calendar.db"));
      try
      {
         sqlite_conn.Open();  // jeśli nie ma pliku bazy to tworzy ją automatycznie
      }
      catch (Exception ex)
      {
         WriteLine(ex.Message.ToString());
      }

      string createCalendarTableQuery = @"
               CREATE TABLE IF NOT EXISTS calendar(
                  id INTEGER PRIMARY KEY AUTOINCREMENT,
                  date DATE NOT NULL,
                  time TIME NOT NULL,
                  text TINYTEXT NOT NULL,
                  priority TINYINT NOT NULL
               );";

      SQLiteCommand command = new SQLiteCommand(createCalendarTableQuery, sqlite_conn);
      try
      {
         command.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
         WriteLine(ex.Message);
      }

      return sqlite_conn;
   }

   public static void AddToDB(DateTime dt, string txt, int priority, SQLiteConnection sqlite_conn)
   {
      string stringAddQuery = "INSERT INTO calendar (date,time,text,priority) " +
                              "VALUES (@inputDate,@inputTime,@inputText,@inputPriority);";

      try
      {
         SQLiteCommand addCommand = new(stringAddQuery, sqlite_conn);

         addCommand.Parameters.Add("@inputDate", DbType.Date);    // DATETIME dziwnie działa w SQLite
         addCommand.Parameters.Add("@inputTime", DbType.Time);    // zapisuje dane jako string...

         addCommand.Parameters["@inputDate"].Value = $"{dt:yyyy-MM-dd}";
         addCommand.Parameters["@inputTime"].Value = $"{dt:HH:mm:ss}";

         addCommand.Parameters.AddWithValue("@inputText", txt);
         addCommand.Parameters.AddWithValue("@inputPriority", priority);

         addCommand.ExecuteNonQuery();
      }

      catch (Exception ex)
      {
         WriteLine(ex.Message);
      }
   }

   public static void RemoveRecord(int id, SQLiteConnection sqlite_conn)
   {
      string removeQuery = "DELETE FROM calendar WHERE ID = @ID;";
      try
      {
         SQLiteCommand removeCommand = new(removeQuery, sqlite_conn);

         removeCommand.Parameters["@ID"].Value = id;
         removeCommand.Parameters.AddWithValue("@ID", SqlDbType.UniqueIdentifier);

         removeCommand.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
         WriteLine(ex.Message);
      }
   }

   /// <summary>
   /// Metoda pobierająca z bazy danych zdarzenia z zażądanego przez program/użytkownika miesiąca
   /// </summary>
   /// <param name="dt">DateTime z pierwszym dniem danego miesiąca</param>
   /// <param name="sqlite_conn">Połączenie z bazą danych</param>
   /// <returns>Tablicę z wszystkimi zdarzeniami danego miesiąca</returns>
   public static DataTable SelectForMonth(DateTime dt, SQLiteConnection sqlite_conn)
    {
        string selectQuery = $"SELECT * FROM CALENDAR WHERE date LIKE '{dt:yyyy-MM-}%'";

        return GetSelectResults(sqlite_conn, selectQuery);

    }
      /// <summary>
      /// Metoda pobierająca z bazy danych zdarzenia z zażądanego przez program/użytkownika dnia
      /// </summary>
      /// <param name="dt">Data z wybranym dniem miesiąca</param>
      /// <param name="sqlite_conn">Połączenie z bazą danych</param>
      /// <returns>Tablicę ze zdarzeniami w konkretnym dniu</returns>

       public static DataTable SelectForDay(DateTime dt, SQLiteConnection sqlite_conn)
    {
        string selectQuery = $"SELECT * FROM CALENDAR WHERE date LIKE '{dt:yyyy-MM-dd}%'";

        return GetSelectResults(sqlite_conn, selectQuery);

    }
   /// <summary>
   /// Zbiera rezultaty kwerend SELECT w tablicy
   /// </summary>
   /// <param name="sqlite_conn">Połączenie sqlite</param>
   /// <param name="selectQuery">Kwerenda do wykonania i przechowania</param>
   /// <returns>Tablica wyników kwerendy selectQuery</returns>
    private static DataTable GetSelectResults(SQLiteConnection sqlite_conn, string selectQuery)
    {
        DataTable results = new();
        try
        {
            SQLiteCommand selectCommand = new(selectQuery, sqlite_conn);

            SQLiteDataReader reader = selectCommand.ExecuteReader();

            if (reader.HasRows)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    results.Columns.Add(new DataColumn(reader.GetName(i)));
                }

                int j = 0;
                while (reader.Read())
                {
                    DataRow row = results.NewRow();
                    results.Rows.Add(row);

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        results.Rows[j][i] = reader.GetValue(i);
                    }

                    j++;
                }

            }
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
        }

        return results;
    }
}
      




