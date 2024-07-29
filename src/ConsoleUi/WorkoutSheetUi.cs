using BenchPressPlus20Kg.Domain;
using Spectre.Console;

namespace BenchPressPlus20Kg.ConsoleUi;

public class WorkoutSheetUi(IPlanRepository planRepository)
{
    public async Task Print(Weight.Unit unit)
    {
        var table = new Table();
        var plan = await planRepository.GetPlan();

        table.Title($"Workout Sheet orm {plan.CurrentOrm}");
        table.AddColumn("Day");
        table.AddColumn("Orm");

        table.AddColumns(
            Enumerable.Range(start: 1, count: 5)
                .Select(i => new TableColumn($"Set {i}"))
                .ToArray()
        );

        table.AddColumn("Is Done");

        foreach (var workout in plan.Workouts)
        {
            table.AddRow(GetRow(plan, workout, unit));
        }

        AnsiConsole.Write(table);
    }

    private static string[] GetRow(Plan plan, Workout workout, Weight.Unit unit)
    {
        var sets = workout.Sets.Select(set => set.ToString(unit)).ToList();

        var rows = sets
            .Prepend(workout.Orm.ToString(unit))
            .Prepend(workout.Ordinal.ToString())
            .ToList();
        
        if (rows.Count < 7)
        {
            rows.AddRange(Enumerable.Repeat(string.Empty, 7 - rows.Count));
        }

        return rows
            .Append(workout.Ordinal < plan.CurrentIndex + 1 ? "Yes" : "No")
            .ToArray();
    }
}
