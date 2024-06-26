namespace BenchPressPlus20Kg.Core;

public record Weight
{
    public enum Unit
    {
        Kg,
        Lb,
    }

    private const decimal LbToKg = 0.45359237m;
    private const decimal KgToLb = 1 / LbToKg;
    
    private decimal Step => InUnit switch
    {
        Unit.Kg => 2.5m,
        Unit.Lb => 5m,
        _ => throw new InvalidOperationException(),
    };

    private Weight(decimal kg, Unit unit)
    {
        InKg = kg;
        InUnit = unit;
    }

    public Unit InUnit { get; set; }
    public decimal InKg { get; }
    public decimal InLb => InKg * KgToLb;

    private decimal InUnitValue => InUnit switch
    {
        Unit.Kg => InKg,
        Unit.Lb => InLb,
        _ => throw new InvalidOperationException(),
    };

    public static Weight FromUnit(decimal weight, Unit unit) => unit switch
    {
        Unit.Kg => new Weight(weight, unit),
        Unit.Lb => new Weight(weight * LbToKg, unit),
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

    public Weight Round() => FromUnit(Math.Round(InUnitValue / Step) * Step, InUnit);

    public Weight IncrementStep() => FromUnit(InUnitValue + Step, InUnit);
    public Weight DecrementStep() => FromUnit(InUnitValue - Step, InUnit);
}
