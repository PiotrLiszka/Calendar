using DBControl;
using System.Data;

namespace Lib;

public class DrawDataFetch
{
   protected static Dictionary <int,int> DataForMonth(DateTime dateTime)
   {
        var sqlite_conn = DBControlClass.CreateConnection();

        DataTable? SelectResults = DBControlClass.SelectForMonth(dateTime, sqlite_conn);

        Dictionary<int, int> ShortResult = new Dictionary<int, int>();

        if (SelectResults.Rows.Count != 0)
        {
            for (int rows = 0; rows < SelectResults.Rows.Count; rows++)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string? dayNr = SelectResults.Rows[rows].ItemArray[1].ToString().Substring(0,2);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                try
                {         
                    ShortResult.Add(int.Parse(dayNr),1);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    ShortResult[int.Parse(dayNr)] += 1;
                }
            }
        }

        // if (SelectResults.Rows.Count != 0)
        // {
        //     for (int rows = 0; rows < SelectResults.Rows.Count; rows++)
        //     {
        //         for (int column = 0; column < SelectResults.Columns.Count; column++)
        //         {
        //             Write($"{SelectResults.Rows[rows].ItemArray[column]}  ");
        //         }
        //         WriteLine();
        //     }
        // }

        sqlite_conn.Close();

        return ShortResult;
   }
}