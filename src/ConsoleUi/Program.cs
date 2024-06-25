// See https://aka.ms/new-console-template for more information

using BenchPressPlus20Kg.Core;

var orm = Weight.FromKg(107.5m);

var workoutSheet = new WorkoutSheet();

var str = workoutSheet.Workouts
    .Select(workout => string.Join(Environment.NewLine, workout.Select(set => set.ToString(orm))))
    .Select((str, i) => $"Workout {i}:{Environment.NewLine}" + str);

Console.WriteLine(string.Join($"{Environment.NewLine}{Environment.NewLine}", str));
