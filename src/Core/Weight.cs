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

    public static Weight FromKg(decimal kg) => new(kg);

    public static Weight FromLb(decimal lb) => new(lb * (decimal) LbToKg);

    public static Weight FromUnit(decimal n, Unit unit) => unit switch
    {
        Unit.Kg => FromKg(n),
        Unit.Lb => FromLb(n),
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
