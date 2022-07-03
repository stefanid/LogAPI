using System;
using Log_API;
using Log_API.Entities;
using Log_API.Helpers;
using NUnit.Framework;

namespace UnitTests;

public class Tests
{
    
    [Test]
    [TestCase(1, "Test message", 1, null, "")]
    [TestCase(1, "Test message", 1, 1, "Test component")]
    public void When_WriteToFile_ThenSuccessfullyExecute(int applicationId, string message,
        int traceId, int? requestId, string componentName)
    {
        LogEntry entry = new LogEntry()
        {
            ApplicationId = applicationId,
            TraceId = traceId,
            Message = message,
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
            RequestId = requestId,
            ComponentName = componentName
        };
        
        Assert.That(() => LogWriter.WriteLog(entry), Throws.Nothing);
    }

}