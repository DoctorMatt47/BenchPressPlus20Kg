namespace BenchPressPlus20Kg.Core;

public record Weight
{
    public enum Unit
    {
        Kg,
        Lb,
    }

    private const double LbToKg = 0.45359237;
    private const double KgToLb = 1 / LbToKg;

    private Weight(decimal kg, Unit unit)
    {
        InKg = kg;
        InUnit = unit;
    }

    public Unit InUnit { get; set; }
    public decimal InKg { get; }
    public decimal InLb => InKg * (decimal) KgToLb;

    public static Weight FromUnit(decimal weight, Unit unit) => unit switch
    {
        Unit.Kg => new Weight(weight, unit),
        Unit.Lb => new Weight(weight * (decimal) LbToKg, unit),
        _ => throw new InvalidOperationException(),
    };

    public static Weight operator *(Weight weight, decimal multiplier)
    {
        return new Weight(weight.InKg * multiplier, weight.InUnit);
    }

    public override string ToString() => InUnit switch
    {
        Unit.Kg => $"{InKg:#.###}",
        Unit.Lb => $"{InLb:#.###}",
        _ => throw new InvalidOperationException(),
    };
    
    public Weight Round() => InUnit switch
    {
        Unit.Kg => new Weight(Math.Round(InKg / 2.5m) * 2.5m, Unit.Kg),
        Unit.Lb => new Weight(Math.Round(InLb), Unit.Lb),
        _ => throw new InvalidOperationException(),
    };
}
