// See https://aka.ms/new-console-template for more information

using BenchPressPlus20Kg.Domain;
using BenchPressPlus20Kg.Infrastructure;

var plan = new Plan(Weight.FromKg(107.5m), new Sheet().GetWorkouts, () => 1);

// var ui = new WorkoutSheetUi(plan, Weight.Unit.Kg);
//
// ui.Print();
//
// foreach (var _ in Enumerable.Range(0, 14))
// {
//     plan.NextWorkout();
//     ui.Print();
// }

Console.WriteLine(plan.Workouts.Aggregate(
    "",
    (acc, w) => acc + Environment.NewLine + Environment.NewLine + w.ToString(Weight.Unit.Kg)));
