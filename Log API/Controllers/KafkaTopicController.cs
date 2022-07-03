using Log_API.Entities;
using Log_API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace Log_API.Controllers;

[ApiController]
[Route("[controller]")]
public class KafkaTopicController: ControllerBase
{
    private LoggerHelper.DelegateLogEntryData _logEntryToKafkaTopic;
    private LoggerHelper.KafkaTopic _kafkaTopic;
    public KafkaTopicController()
    {
        _kafkaTopic = new LoggerHelper.KafkaTopic();
        _logEntryToKafkaTopic = new LoggerHelper.DelegateLogEntryData(_kafkaTopic.LogEntryDataToKafkaTopic);
    }
    
    [HttpPost("log")]
    public async Task<IActionResult> Log(LogEntry entry)
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