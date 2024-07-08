namespace BenchPressPlus20Kg.Domain;

public record Workout
{
    public required int Ordinal { get; init; }
    public required List<Set> Sets { get; init; }
    public required Weight Orm { get; init; }

    public bool HasFailureTest => Sets.Any(set => set.IsFailureTest);
    
    public string ToString(Weight.Unit unit)
    {
        var sets = string.Join(
            Environment.NewLine,
            Sets.Select(s => s.ToString(unit))
        );
        
        return $"{Ordinal}. {Orm.ToString(unit)}{Environment.NewLine}{sets}";
    }
}
