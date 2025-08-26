using System;
using System.IO;
/// <summary>
/// Developed by Praveen 31636
/// </summary>
/// 
public class TrvApiLogger
{
    private string logFilePath;
   

    public TrvApiLogger()
    {
        logFilePath = @"D:\\WebLogs\\TrvApiError.log";
        EnsureLogDirectoryExists();
    }
   
    public TrvApiLogger(string logFilePath)
    {
        this.logFilePath = logFilePath;
        EnsureLogDirectoryExists();
    }

    // write a log entry
    public void WriteLog(string msg)
    {
        try
        {
            using (var fs = File.Open(logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
            using (var sw = new StreamWriter(fs))
            {
                sw.AutoFlush = true;
                sw.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));
                sw.Write(" : ");
                sw.WriteLine(msg);
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    // ensure the log directory exists
    private void EnsureLogDirectoryExists()
    {
        var logDirectory = Path.GetDirectoryName(logFilePath);
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
    }

}