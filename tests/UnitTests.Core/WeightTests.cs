using BenchPressPlus20Kg.Core;
using Bogus;

namespace BenchPressPlus20Kg.UnitTests.Core;

public class WeightTests
{
    private const double LbToKg = 0.45359237;
    private const double KgToLb = 1 / LbToKg;

    private readonly Faker _faker = new();

    [Fact]
    public void FromKg_ShouldCreateFromKg()
    {
        // Arrange
        var kg = _faker.Random.Int();

        // Act
        var weight = Weight.FromKg(kg);

        // Assert
        Assert.Equal(kg, weight.InKg);
    }

    [Fact]
    public void FromLb_ShouldCreateFromLb()
    {
        // Arrange
        var lb = _faker.Random.Int();

        // Act
        var weight = Weight.FromLb(lb);

        // Assert
        Assert.Equal(lb, weight.InLb);
    }

    [Fact]
    public void InKg_CreatedFromLb_ConvertToKg()
    {
        // Arrange
        var lb = _faker.Random.Int();
        var weight = Weight.FromLb(lb);

        // Act
        var kg = weight.InKg;

        // Assert
        Assert.Equal(lb * LbToKg, kg);
    }

    [Fact]
    public void InLb_CreatedFromKg_ConvertToLb()
    {
        // Arrange
        var kg = _faker.Random.Int();
        var weight = Weight.FromKg(kg);

        // Act
        var lb = weight.InLb;

        // Assert
        Assert.Equal(kg * KgToLb, lb);
    }
}
