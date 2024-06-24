using System.Data;
using System;
using System.Collections.Generic;

namespace DBControl;

public static class DataFetch
{
    /// <summary>
    /// Zbiera informacje z całego miesiąca i przekształca jest w Dictionary, które zawiera info, ile jest przypisanych wydarzeń w danym dniu.
    /// </summary>
    /// <param name="dateTime">Miesiąc, dla którego dane mają być sprawdzone (numer dnia nieistotny)</param>
    /// <returns>Dictionary z ilościami wydarzeń</returns>
   public static Dictionary <int,int> DataForMonth(DateTime dateTime)
   {
        var sqlite_conn = DBQueries.CreateConnection();

        DataTable? SelectResults = DBQueries.SelectForMonth(dateTime, sqlite_conn);

        Dictionary<int, int> ShortResult = new();

        if (SelectResults.Rows.Count != 0)
        {
            for (int rows = 0; rows < SelectResults.Rows.Count; rows++)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string? dayNr = SelectResults.Rows[rows].ItemArray[1].ToString()[..2];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                try
                {         
                    ShortResult.Add(int.Parse(dayNr),1);
                }
                catch
                {
                    // Dodaje do value +1, jeśli dzień znajduje się już w Dictionary - zwiększa ilość zdarzeń z danego dnia
                    ShortResult[int.Parse(dayNr)] += 1;
                }
            }
        }

        sqlite_conn.Close();

        return ShortResult;
   }

}