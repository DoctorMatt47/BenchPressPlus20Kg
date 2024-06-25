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

    private Weight(decimal kg) => InKg = kg;

    public decimal InKg { get; }

    public decimal InLb => InKg * (decimal) KgToLb;

    public static Weight FromUnit(decimal weight, Unit unit) => unit switch
    {
        Unit.Kg => new Weight(weight),
        Unit.Lb => new Weight(weight * (decimal) LbToKg),
        _ => throw new InvalidOperationException(),
    };

    public static Weight operator *(Weight weight, decimal multiplier) => new(weight.InKg * multiplier);

    public string ToString(Unit unit) => unit switch
    {
        Unit.Kg => $"{InKg} kg",
        Unit.Lb => $"{InLb} lb",
        _ => throw new InvalidOperationException(),
    };
}
