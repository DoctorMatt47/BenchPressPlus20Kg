using BenchPressPlus20Kg.Core;
using Bogus;
using FluentAssertions;

namespace BenchPressPlus20Kg.UnitTests.Core;

public class SetTests
{
    private readonly Faker _faker = new();

    [Fact]
    public void FromReps_SetRelative_ReturnsPercentAndReps()
    {
        // Arrange
        var percent = _faker.Random.Decimal();
        var reps = _faker.Random.Int();

        // Act
        var result = new SetRelative
        {
            Reps = reps,
            Percent = percent,
        };

        // Assert
        result.Percent.Should().Be(percent);
        result.Reps.Should().Be(reps);
    }

    [Fact]
    public void Negative_SetRelative_ReturnsPercentAndIsNegative()
    {
        // Arrange
        var percent = _faker.Random.Decimal();

        // Act
        var result = new SetRelative
        {
            IsNegative = true,
            Percent = percent,
        };

        // Assert
        result.Percent.Should().Be(percent);
        result.IsNegative.Should().BeTrue();
    }

    [Fact]
    public void FailureTest_SetRelative_ReturnsPercentAndIsFailureTest()
    {
        // Arrange
        var percent = _faker.Random.Decimal();

        // Act
        var result = new SetRelative
        {
            IsFailureTest = true,
            Percent = percent,
        };

        // Assert
        result.Percent.Should().Be(percent);
        result.IsFailureTest.Should().BeTrue();
    }

    [Fact]
    public void ToString_SetAbsolute_ReturnsWeightAndRepsString()
    {
        // Arrange
        var weight = Weight.FromUnit(_faker.Random.Decimal(), Weight.Unit.Kg);
        var reps = _faker.Random.Int();
        
        var set = new Set
        {
            Reps = reps,
            Weight = weight,
        };

        // Act
        var result = set.ToString();

        // Assert
        result.Should().Be($"{weight.Round()} x {reps}");
    }
}
