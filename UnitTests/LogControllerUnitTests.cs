using System;
using System.Threading.Tasks;
using Log_API;
using Log_API.Entities;
using Log_API.Helpers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace UnitTests;

public class LogControllerUnitTests
{
    private Log_API.Controllers.LogController _controller;
    private LoggerHelper.DelegateLogEntryData _logEntryToKafkaTopic;
    private LoggerHelper.KafkaTopic _kafkaTopic;
    
    public LogControllerUnitTests()
    {
        _controller = new Log_API.Controllers.LogController();
        _kafkaTopic = new LoggerHelper.KafkaTopic();
        _logEntryToKafkaTopic = new LoggerHelper.DelegateLogEntryData(_kafkaTopic.LogEntryDataToKafkaTopic);
    }
    
    [Test]
    public async Task When_WriteToFlatFile_ThenSuccessfullyDelegateFunction()
    {
        //Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
        };

       
        
        //Act
        Task<IActionResult> result = _controller.FlatFile(entry);
        
        //Assert
        Assert.That(result.Result is OkResult);
    }
    
    [Test]
    public async Task When_WriteToKafkaTopic_ThenThrowNotImplementedYetInternalServerError()
    {
        //Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
        };
        
        
        //Act
        Task<IActionResult> result = _controller.Kafka(entry);
        ObjectResult? resultValue = result.Result as ObjectResult;
        
        // Assert
        Assert.True(result.Result is ObjectResult);
        Assert.True(resultValue.StatusCode.Equals(500));
        Assert.True(resultValue.Value.Equals("The method or operation is not implemented."));
    }
}