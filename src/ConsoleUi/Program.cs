// See https://aka.ms/new-console-template for more information

using BenchPressPlus20Kg.Core;

var orm = Weight.FromUnit(weight: 107.5m, Weight.Unit.Kg);

var workoutSheet = new WorkoutSheet();

var str = workoutSheet.Workouts
    .Select(workout => string.Join(Environment.NewLine, workout.Select(set => set.ToString(orm))))
    .Select((str, i) => $"Workout {i + 1}:{Environment.NewLine}" + str);

Console.WriteLine(string.Join($"{Environment.NewLine}{Environment.NewLine}", str));
