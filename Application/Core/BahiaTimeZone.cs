namespace Application.Core;

public class BahiaTimeZone
{
    private readonly TimeZoneInfo _bahiaTimeZone;

    public BahiaTimeZone()
    {
        _bahiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Bahia");
    }

    public DateTime Now()
    {
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _bahiaTimeZone);
    }

    public DateTime ConvertToBahiaTime(DateTime dateTime)
    {
        DateTime utcDateTime = dateTime.Kind == DateTimeKind.Utc
            ? dateTime
            : dateTime.ToUniversalTime();

        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, _bahiaTimeZone);
    }

    public DateOnly GetFirstDayOfCurrentMonth()
    {
        var nowInBahia = Now();
        return new DateOnly(nowInBahia.Year, nowInBahia.Month, 1);
    }
}
