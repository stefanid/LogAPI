using System;
using Log_API;
using Log_API.Entities;
using Log_API.Helpers;
using Xunit;
using Assert = Xunit.Assert;

namespace APIUnitTests;

public class LogWriterUnitTests
{
    [Fact]
    public void When_WriteToFile_ThenSuccessfullyWrite()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            Message = "Failed operation",
            Severity = Severity.High,
            TraceId = 1,
            TimeStamp = DateTimeOffset.Now.ToLocalTime()
        };
        
        //Act
        Action act = () => LogWriter.WriteLog(entry);
        
        // Assert
        Assert.Null(act);
    }
    
    [Fact]
    public void When_WriteToFileAndExecutionFails_ThenThrowException()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            Message = "Failed operation",
            Severity = Severity.High,
            TraceId = 1,
            TimeStamp = DateTimeOffset.Now.ToLocalTime()
        };
        
        //Act
        Action act = () => LogWriter.WriteLog(entry);

        // Assert
        Exception exception = Assert.Throws<Exception>(act);
        Assert.NotNull(exception);
    }
}