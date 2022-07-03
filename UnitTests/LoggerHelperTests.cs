using System;
using Log_API;
using Log_API.Entities;
using Log_API.Helpers;
using NUnit.Framework;

namespace UnitTests;

public class LoggerHelperTests
{
    private LoggerHelper.DelegateLogEntryData logEntryToFlatFile;
    private LoggerHelper.DelegateLogEntryData _logEntryToKafkaTopic;
    private LoggerHelper.KafkaTopic _kafkaTopic;
    
    public LoggerHelperTests()
    {
        logEntryToFlatFile = LoggerHelper.FlatFile.LogEntryDataToFlatFile;
        _kafkaTopic = new LoggerHelper.KafkaTopic();
        _logEntryToKafkaTopic = new LoggerHelper.DelegateLogEntryData(_kafkaTopic.LogEntryDataToKafkaTopic);
    }
    
    
    [Test]
    public void When_WriteToFlatFile_ThenSuccessfullyDelegateFunction()
    {
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
        };
        
        Assert.That(() => logEntryToFlatFile(entry), Throws.Nothing);
    }
    
    [Test]
    public void When_WriteToKafkaTopic_ThenThrowNotImplementedYet()
    {
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
        };
        
        Assert.That(() => _logEntryToKafkaTopic(entry), Throws.TypeOf<NotImplementedException>());
    }
}