namespace BenchPressPlus20Kg.Core;

public class Weight
{
    private const double LbToKg = 0.45359237;
    private const double KgToLb = 1 / LbToKg;

    public double InKg { get; }

    public double InLb => InKg * KgToLb;
    
    private Weight(double kg) => InKg = kg;

    public static Weight FromKg(int kg) => new(kg);
    
    public static Weight FromLb(int lb) => new(lb * LbToKg);
}
