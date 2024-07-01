using System.Globalization;
using BenchPressPlus20Kg.Core;
using CsvHelper;
using CsvHelper.Configuration;

namespace BenchPressPlus20Kg.SheetParser;

internal record SheetSet(int Count, char Reps);

internal record SheetWorkout(SheetSet A, SheetSet B, SheetSet C);

public class Sheet
{
    private const string Path = "WorkoutSheet.csv";

    private Dictionary<Weight, IEnumerable<Workout>>? _sheet;

    public IEnumerable<Workout> GetWorkouts(Weight weight)
    {
        if (_sheet is null)
        {
            LoadFromCsv();
        }
        
        return _sheet![weight];
    }

    private void LoadFromCsv()
    {
        // ReSharper disable PossibleMultipleEnumeration
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };

        using var reader = new StreamReader(Path);
        using var csv = new CsvReader(reader, config);

        var records = csv.GetRecords<object>().Cast<IDictionary<string, object>>();

        var counts = records.Skip(3)
            .First()
            .Skip(1)
            .Select(x => int.Parse((string) x.Value))
            .Chunk(3);

        var reps = records.First()
            .Skip(1)
            .Select(x => ((string) x.Value)[0])
            .Chunk(3);

        var workouts = counts.Zip(
            reps,
            (cs, rs) => new SheetWorkout(
                new SheetSet(cs[0], rs[0]),
                new SheetSet(cs[1], rs[1]),
                new SheetSet(cs[2], rs[2])
            )
        );

        var results = records
            .Select(
                row => (
                    Orm: Weight.FromLb(int.Parse((string) row.First().Value)),
                    Weights: row
                        .Skip(1)
                        .Select(x => int.Parse((string) x.Value))
                        .Chunk(3)
                        .ToList())
            )
            .ToDictionary(
                tuple => tuple.Orm,
                tuple => workouts
                    .Zip(
                        tuple.Weights,
                        ZipWorkoutAndWeight
                    )
                    .Select(
                        (sets, index) => new Workout
                        {
                            Ordinal = index + 1,
                            Sets = sets.ToList(),
                            Orm = tuple.Orm,
                        }
                    )
            );

        _sheet = results;
        // ReSharper restore PossibleMultipleEnumeration
    }

    private static IEnumerable<Set> ZipWorkoutAndWeight(SheetWorkout workout, int[] weights)
    {
        return new List<SheetSet> {workout.A, workout.B, workout.C}
            .Select(
                (sets, index) => Enumerable.Range(start: 1, sets.Count)
                    .Select(
                        ordinal => new Set
                        {
                            Ordinal = ordinal,
                            IsFailureTest = sets.Reps == 'F',
                            IsNegative = sets.Reps == 'N',
                            Reps = int.TryParse(sets.Reps.ToString(), out var reps) ? reps : null,
                            Weight = Weight.FromLb(weights[index]),
                        }
                    )
            )
            .SelectMany(x => x);
    }
}
