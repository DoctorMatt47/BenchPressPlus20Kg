using BenchPressPlus20Kg.Core;
using Spectre.Console;

namespace BenchPressPlus20Kg.ConsoleUi;

public class WorkoutSheetUi(Plan plan)
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
        
        foreach (var workout in plan.Workouts)
        {
            var sets = workout.Sets.Select(set => set.ToString()).ToList();
            
            table.AddRow(
                    sets
                    .Prepend(workout.Orm.ToString())
                    .Prepend(workout.Ordinal.ToString())
                    .ToArray()
            );
        }

        AnsiConsole.Write(table);
    }
}
