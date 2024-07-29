namespace BenchPressPlus20Kg.Domain;

public class Plan
{
    public required List<Workout> Workouts { get; init; }
    public required Weight CurrentOrm { get; set; }
    public int CurrentIndex { get; set; }
}
