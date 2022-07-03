using System.Reflection;
using Log_API.Entities;

namespace Log_API.Helpers;

public static class LogWriter
{
    private static string path = @"logData.txt";

    public static void WriteLog(LogEntry entry)
    {
        try
        {
            using (StreamWriter w = File.AppendText(path))
            {
                Log(entry, w);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private static void Log(LogEntry entry, TextWriter txtWriter)
    {
        try
        {
            txtWriter.Write("\r\nLog Entry : ");
            txtWriter.WriteLine("{0}", entry.TimeStamp.ToLocalTime());
            txtWriter.Write("\r\nApplication ID : ");
            txtWriter.WriteLine("{0}", entry.ApplicationId);
            txtWriter.Write("\r\nTrace ID : ");
            txtWriter.WriteLine("{0}", entry.TraceId);
            txtWriter.Write("\r\nSeverity : ");
            txtWriter.WriteLine("{0}", entry.Severity);
            txtWriter.Write("\r\nMessage : ");
            txtWriter.WriteLine("{0}", entry.Message);
            
            if (entry.RequestId.HasValue)
            {
                txtWriter.Write("\r\nRequest Id : ");
                txtWriter.WriteLine("{0}", entry.RequestId);
            }
            if (!string.IsNullOrEmpty(entry.ComponentName))
            {
                txtWriter.Write("\r\nComponent name : ");
                txtWriter.WriteLine("{0}", entry.ComponentName);
            }
            txtWriter.WriteLine("-------------------------------");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}