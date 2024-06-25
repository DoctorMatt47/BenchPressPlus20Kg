// ReSharper disable ArgumentsStyleLiteral

namespace BenchPressPlus20Kg.Core;

public class WorkoutSheet
{
    public IEnumerable<IEnumerable<WorkoutSet>> Workouts { get; } =
        new List<List<IEnumerable<WorkoutSet>>>
            {
                new() {Sets(0.75m, 1, 6), Sets(0.8m, 2, 5), Sets(0.8m, 2, 4)},
                new() {Sets(0.85m, 2, 3), Sets(0.9m, 2, 2), Sets(1m, 1, isNegative: true)},
                new() {Sets(0.75m, 1, 6), Sets(0.8m, 2, 5), Sets(0.85m, 2, 4)},
                new() {Sets(0.9m, 2, 3), Sets(0.95m, 2, 2), Sets(1.05m, 1, isNegative: true)},
                new() {Sets(0.8m, 1, 6), Sets(0.85m, 2, 5), Sets(0.9m, 1, isFailureTest: true)},
                new() {Sets(0.9m, 2, 3), Sets(1m, 2, 2), Sets(1.1m, 1, isNegative: true)},
                new() {Sets(0.85m, 2, 5), Sets(0.95m, 2, 3), Sets(0.95m, 1, isFailureTest: true)},
                new() {Sets(0.95m, 2, 3), Sets(1.05m, 2, 1), Sets(1.15m, 1, isNegative: true)},
                new() {Sets(0.9m, 2, 5), Sets(1m, 2, 3), Sets(1m, 1, isFailureTest: true)},
                new() {Sets(1m, 2, 3), Sets(1.1m, 2, 2), Sets(1.1m, 1, 1)},
                new() {Sets(0.95m, 2, 5), Sets(1m, 2, 3), Sets(1.05m, 1, isFailureTest: true)},
                new() {Sets(1.0m, 2, 3), Sets(1.1m, 2, 2), Sets(1.15m, 1, 1)},
                new() {Sets(1m, 1, 5), Sets(1.1m, 2, 3), Sets(1.15m, 2, 2)},
                new() {Sets(1.05m, 1, 3), Sets(1.15m, 1, 2), Sets(1.20m, 1, 1)},
            }
            .Select(x => x.SelectMany(y => y));

    private static IEnumerable<WorkoutSet> Sets(
        decimal percent,
        int count,
        int? reps = null,
        bool isNegative = false,
        bool isFailureTest = false)
    {
        for (var i = 0; i < count; i++)
        {
            yield return (isNegative, isUntilFailure: isFailureTest) switch
            {
                (false, false) => WorkoutSet.FromReps(percent, reps!.Value),
                (true, false) => WorkoutSet.Negative(percent),
                (false, true) => WorkoutSet.FailureTest(percent),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
