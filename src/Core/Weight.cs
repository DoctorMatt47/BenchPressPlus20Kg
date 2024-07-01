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

    private readonly decimal _lb;

    private Weight(decimal lb) => _lb = lb;

    public decimal InKg => Math.Round(_lb * LbToKg / 2.5m) * 2.5m;
    public decimal InLb => Math.Round(_lb / 5m) * 5m;

    public static Weight FromKg(decimal kg) => new(kg * KgToLb);
    public static Weight FromLb(decimal lb) => new(lb);

    public string ToString(Unit unit) => unit switch
    {
        Unit.Kg => $"{InKg:#.###}",
        Unit.Lb => $"{InLb:#.###}",
        _ => throw new InvalidOperationException(),
    };

    public Weight IncrementStep() => new(_lb + 5m);
    public Weight DecrementStep() => new(_lb - 5m);
}
