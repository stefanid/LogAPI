using System;
using System.Threading.Tasks;
using Log_API;
using Log_API.Controllers;
using Log_API.Entities;
using Log_API.Helpers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace UnitTests;

public class KafkaTopicControllerUnitTests
{
    private LoggerHelper.DelegateLogEntryData _logEntryToKafkaTopic;
    private LoggerHelper.KafkaTopic _kafkaTopic;

    public KafkaTopicControllerUnitTests()
    {
        _kafkaTopic = new LoggerHelper.KafkaTopic();
        _logEntryToKafkaTopic = new LoggerHelper.DelegateLogEntryData(_kafkaTopic.LogEntryDataToKafkaTopic);
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
        
        KafkaTopicController controller = new KafkaTopicController();
        
        //Act
        Task<IActionResult> result = controller.Log(entry);
        ObjectResult? resultValue = result.Result as ObjectResult;
        
        // Assert
        Assert.True(result.Result is ObjectResult);
        Assert.True(resultValue.StatusCode.Equals(500));
        Assert.True(resultValue.Value.Equals("The method or operation is not implemented."));
    }
    
}