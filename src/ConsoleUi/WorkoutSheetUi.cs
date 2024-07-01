using BenchPressPlus20Kg.Core;
using Spectre.Console;

namespace BenchPressPlus20Kg.ConsoleUi;

public class WorkoutSheetUi(Plan plan, Weight.Unit unit)
{
    public void Print()
    {
        var table = new Table();

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
            table.AddRow(GetRow(workout));
        }

        AnsiConsole.Write(table);
    }

    private string[] GetRow(Workout workout)
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
