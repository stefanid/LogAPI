using Log_API.Entities;

namespace Log_API.Helpers;

public class LoggerHelper
{
    public delegate Task DelegateLogEntryData(LogEntry entry);

    public class FlatFile
    {
        public static async Task LogEntryDataToFlatFile(LogEntry entry)
        {
            LogWriter.WriteLog(entry);
        }
    }

    public class KafkaTopic
    {
        public async Task LogEntryDataToKafkaTopic(LogEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}