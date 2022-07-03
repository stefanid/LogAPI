using System;
using FluentValidation.TestHelper;
using Log_API;
using Log_API.Entities;
using NUnit.Framework;

namespace UnitTests;

public class LogEntryValidationUnitTests
{
    private LogEntryValidator _validator;
    
    public LogEntryValidationUnitTests()
    {
        _validator = new LogEntryValidator();
    }
    
    [Test]
    public void When_LogEntryHasApplicationIdMissing_ThenShouldHaveValidationError()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            TraceId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
        };
        
        // Act
        TestValidationResult<LogEntry>? result = _validator.TestValidate(entry);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ApplicationId);
    }
    
    [Test]
    public void When_LogEntryHasTraceIdMissing_ThenShouldHaveValidationError()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
        };
        
        // Act
        TestValidationResult<LogEntry>? result = _validator.TestValidate(entry);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TraceId);
    }
    
    [Test]
    public void When_LogEntryMessageMissing_ThenShouldHaveValidationError()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
        };
        
        // Act
        TestValidationResult<LogEntry>? result = _validator.TestValidate(entry);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Message);
    }
    
    [Test]
    public void When_LogEntryMessageIsEmptyString_ThenShouldHaveValidationError()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = String.Empty,
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
        };
        
        // Act
        TestValidationResult<LogEntry>? result = _validator.TestValidate(entry);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Message);
    }
    
    [Test]
    public void When_LogEntryTimeStampMissing_ThenShouldHaveValidationError()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = "Test",
            Severity = Severity.High,
        };
        
        // Act
        TestValidationResult<LogEntry>? result = _validator.TestValidate(entry);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.TimeStamp);
    }
    
    [Test]
    public void When_LogEntrySeverityMissing_ThenShouldHaveValidationError()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime()
        };
        
        // Act
        TestValidationResult<LogEntry>? result = _validator.TestValidate(entry);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Severity);
    }
    
    [Test]
    public void When_LogEntryBodyValid_ThenShouldNotHaveValidationError()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High
        };
        
        // Act
        TestValidationResult<LogEntry>? result = _validator.TestValidate(entry);
        
        // Assert
        Assert.That(result.IsValid);
    }
    
    [Test]
    public void When_LogEntryBodyValidWithOptionalParams_ThenShouldNotHaveValidationError()
    {
        // Arrange
        LogEntry entry = new LogEntry()
        {
            ApplicationId = 1,
            TraceId = 1,
            Message = "Test",
            TimeStamp = DateTimeOffset.Now.ToLocalTime(),
            Severity = Severity.High,
            ComponentName = "Test component",
            RequestId = 1
        };
        
        // Act
        TestValidationResult<LogEntry>? result = _validator.TestValidate(entry);
        
        // Assert
        Assert.That(result.IsValid);
    }
    
}