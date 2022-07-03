using FluentValidation;

namespace Log_API.Entities;

public class LogEntry
{
    public int ApplicationId { get; set; }
    public int TraceId { get; set; }
    public Severity Severity { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
    public string Message { get; set; }
    public string? ComponentName { get; set; }
    public int? RequestId { get; set; }
}

public class LogEntryValidator : AbstractValidator<LogEntry>
{
    public LogEntryValidator()
    {
        RuleFor(le => le.ApplicationId)
            .NotEmpty();
        RuleFor(le => le.TraceId)
            .NotEmpty();
        RuleFor(le => le.Severity)
            .IsInEnum()
            .NotEmpty();
        RuleFor(le => le.Message)
            .NotEmpty();
        RuleFor(le => le.TimeStamp)
            .NotEmpty();
    }
}