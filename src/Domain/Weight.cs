// ReSharper disable UnusedMember.Local

using System.Text.Json;
using System.Text.Json.Serialization;

namespace BenchPressPlus20Kg.Domain;

[JsonConverter(typeof(WeightSerializer))]
public record Weight
{
    public enum Unit
    {
        Kg,
        Lb,
    }

    public const decimal LbToKg = 0.45359237m;
    public const decimal KgToLb = 1 / LbToKg;

    private readonly decimal _lb;
    
    private Weight(decimal lb) => _lb = lb;

    public decimal InKg => Math.Round(_lb * LbToKg / 2.5m) * 2.5m;
    public decimal InLb => Math.Round(_lb / 5m) * 5m;

    public virtual bool Equals(Weight? other)
    {
        if (ReferenceEquals(objA: null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return InLb == other.InLb;
    }

    public override int GetHashCode() => InLb.GetHashCode();

    public static Weight FromKg(decimal kg) => new(kg * KgToLb);
    public static Weight FromLb(decimal lb) => new(lb);
    
    public static Weight FromUnit(decimal value, Unit unit) => unit switch
    {
        Unit.Kg => FromKg(value),
        Unit.Lb => FromLb(value),
        _ => throw new InvalidOperationException(),
    };

    public string ToString(Unit unit) => unit switch
    {
        Unit.Kg => $"{InKg:#.###}",
        Unit.Lb => $"{InLb:#.###}",
        _ => throw new InvalidOperationException(),
    };

    public Weight IncrementStep() => new(_lb + 5m);
    public Weight DecrementStep() => new(_lb - 5m);
}

public class WeightSerializer : JsonConverter<Weight>
{
    public override Weight? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return Weight.FromLb(reader.GetDecimal());
    }

    public override void Write(Utf8JsonWriter writer, Weight value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.InLb);
    }
}
