namespace Lib;
using System;


public class FileCheck : IDisposable
{
    private bool _disposed;

    // TODO: sprawdzanie istnienie folderu/pliku bazy danych (lokalizacja do zmiany)
    public void CheckDBFile (string path)
   {
      if (!File.Exists(path))
      {
         File.Create(path);
      }
   }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~FileCheck()
    {
      Dispose(false);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                
            }
 
            _disposed = true;
        }
    }
}
