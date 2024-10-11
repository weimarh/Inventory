using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public partial record Price
{
    private const string Pattern = @"^\d+(\.\d{1,2})?$";

    private Price(string value) => Value = value;

    public static Price? Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !PriceRegex().IsMatch(value))
        {
            return null;
        }

        return new Price(value);
    }

    public string Value { get; init; }

    [GeneratedRegex(Pattern)]
    private static partial Regex PriceRegex();
}
