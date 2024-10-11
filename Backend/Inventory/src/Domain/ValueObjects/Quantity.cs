using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public partial record Quantity
{
    private const string Pattern = @"^\d+(\.\d{1,2})?$";
    public Quantity(string amount, string unit)
    {
        Amount = amount;
        Unit = unit;
    }

    public string Amount { get; init; }
    public string Unit { get; init; }

    public static Quantity? Create(string amount, string unit)
    {
        if (string.IsNullOrWhiteSpace(amount) || string.IsNullOrEmpty(unit)
            || !AmountRegex().IsMatch(amount))
        {
            return null;
        }

        return new Quantity(amount, unit);
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex AmountRegex();
}
