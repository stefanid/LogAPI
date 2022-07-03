using System;
using System.Threading.Tasks;
using Log_API;
using Log_API.Entities;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace UnitTests;

public class FlatFileController
{
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

        Log_API.Controllers.FlatFileController controller = new Log_API.Controllers.FlatFileController();
        
        //Act
        Task<IActionResult> result = controller.Log(entry);
        
        //Assert
        Assert.That(result.Result is OkResult);
    }
}