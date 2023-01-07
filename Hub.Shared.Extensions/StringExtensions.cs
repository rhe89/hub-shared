using System;
using JetBrains.Annotations;

namespace Hub.Shared.Extensions;

[UsedImplicitly]
public static class StringExtensions
{
    [UsedImplicitly]
    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => null,
            "" => "",
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };
}