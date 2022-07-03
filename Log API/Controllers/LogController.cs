using Log_API.Entities;
using Log_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Log_API.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController: ControllerBase
{
    private LoggerHelper.DelegateLogEntryData logEntryToFlatFile;
    private LoggerHelper.DelegateLogEntryData _logEntryToKafkaTopic;
    private LoggerHelper.KafkaTopic _kafkaTopic;
    
    public LogController()
    {
        logEntryToFlatFile = LoggerHelper.FlatFile.LogEntryDataToFlatFile;
        _kafkaTopic = new LoggerHelper.KafkaTopic();
        _logEntryToKafkaTopic = new LoggerHelper.DelegateLogEntryData(_kafkaTopic.LogEntryDataToKafkaTopic);
    }
    
    [HttpPost("flat-file")]
    public async Task<IActionResult> FlatFile(LogEntry entry)
    {
        try
        {
           await logEntryToFlatFile(entry);
           return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpPost("kafka-topic")]
    public async Task<IActionResult> Kafka(LogEntry entry)
    {
        try
        {
            await _logEntryToKafkaTopic(entry);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}