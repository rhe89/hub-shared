using System;
using JetBrains.Annotations;

namespace Hub.Shared.Extensions;

[UsedImplicitly]
public static class DateTimeExtensions
{
    [UsedImplicitly]
    public static string ToNorwegianDateString(this DateTime dateTime)
    {
        return dateTime.ToString("dd.MM");
    }
}