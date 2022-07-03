using Log_API.Entities;
using Log_API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Log_API.Controllers;

[ApiController]
[Route("[controller]")]
public class FlatFileController: ControllerBase
{
    private LoggerHelper.DelegateLogEntryData logEntryToFlatFile;
    public FlatFileController()
    {
        
        logEntryToFlatFile = LoggerHelper.FlatFile.LogEntryDataToFlatFile;
    }
    
    [HttpPost("log")]
    public async Task<IActionResult> Log(LogEntry entry)
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
}