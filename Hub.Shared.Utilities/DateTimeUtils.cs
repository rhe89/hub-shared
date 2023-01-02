using System;
using JetBrains.Annotations;

namespace Hub.Shared.Utilities;

[UsedImplicitly]
public static class DateTimeUtils
{
    [UsedImplicitly]
    public static DateTime Today => DateTime.Now.Date;

    [UsedImplicitly]
    public static DateTime FirstDayOfMonth(int? year, int? month)
    {
        year ??= Today.Year;
        month ??= Today.Month;
        
        return new DateTime(year.Value, month.Value, 1);
    }
    
    [UsedImplicitly]
    public static DateTime FirstDayOfMonth(DateTime date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }

    [UsedImplicitly]
    public static DateTime FirstDayOfMonth()
    {
        return FirstDayOfMonth(Today);
    }
    
    [UsedImplicitly]
    public static DateTime LastDayOfMonth(int? year, int? month)
    {
        year ??= Today.Year;
        month ??= Today.Month;
        
        return new DateTime(year.Value, month.Value, DateTime.DaysInMonth(year.Value, month.Value));
    }
    
    [UsedImplicitly]
    public static DateTime LastDayOfMonth(DateTime date)
    {
        return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
    }
    
    [UsedImplicitly]
    public static DateTime LastDayOfMonth()
    {
        return LastDayOfMonth(Today);
    }
}