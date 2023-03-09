using AreaCalculator;
using static FluentAssertions.FluentActions;

namespace AreaCalculatorTest;

public class AreaCalculatorTests
{
    [Fact]
    public void Triangle_and_circle_are_shapes()
    {
        // Arrange
        var triangle = new Triangle(sideA: 3, sideB: 4, sideC: 5);
        var cirlce = new Circle(1);

        // Act and Assert
        triangle.Should().BeAssignableTo<IFigure>();
        cirlce.Should().BeAssignableTo<IFigure>();
    }
    
    [Fact]
    public void Circle_created()
    {
        // Arrange
        var radius = 1d;

        // Act
        var circle = new Circle(radius: radius);

        // Assert
        circle.Radius.Should().Be(radius);
    }
    
    [Theory]
    [InlineData(double.NaN)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(double.NegativeInfinity)]
    [InlineData(0)]
    [InlineData(-1)]
    public void Creating_invalid_circle_is_impossible(double radius)
    {
        FluentActions.Invoking(() => new Circle(radius))
            .Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName("radius", "because radius is invalid");
    }

    [Theory]
    [InlineData(1, Math.PI)]
    [InlineData(5.5, 95.033177771091246)]
    public void Area_calculation_is_correct(double radius, double expectedArea)
    {
        var circle = new Circle(radius);
        circle.GetArea().Should()
            .BeApproximately(expectedArea, 0.000000001);
    }
    
    [Fact]
    public void Unit_circle_area()
    {
        var eps = 1e-16;
        var circle = new Circle(1);
        var area = circle.GetArea();
        
        Assert.InRange(area, Math.PI - eps, Math.PI + eps);
    }
    
    
    [Theory]
    [InlineData(3, 4, 5)]
    [InlineData(5, 12, 13)]
    [InlineData(8, 15, 17)]
    public void Triangle_created(double a, double b, double c)
    {
        var triangle = new Triangle(sideA: a, sideB: b, sideC: c);
        triangle.SideA.Should().Be(a);
        triangle.SideB.Should().Be(b);
        triangle.SideC.Should().Be(c);
    }

    [Fact]
    public void Invalid_triangle_creating_is_impossible()
    {
        var act = () => new Triangle(sideA: 1, sideB: 2, sideC: 3);
        act.Should()
            .ThrowExactly<ArgumentException>("because triangle with given sides doesnt exist")
            .WithMessage(Triangle.TriangleWithGivenSidesDoesntExist);
    }

    [Theory]
    [InlineData(double.NaN, 4, 5)]
    [InlineData(double.PositiveInfinity, 4, 5)]
    [InlineData(1, double.NaN, 5)]
    [InlineData(1, 2, double.NaN)]
    [InlineData(0, 4, 5)]
    [InlineData(-1, 4, 5)]
    public void Triangle_with_negative_size_creating_is_impossible(
        double sideA, double sideB, double sideC)
    {
        Invoking(() => new Triangle(sideA, sideB, sideC))
            .Should()
            .Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(3, 4, 5, 6)]
    [InlineData(5, 12, 13, 30)]
    [InlineData(8, 15, 17, 60)]
    public void Triangle_are_calculation_is_correct(
        double sideA, double sideB, double sideC, double expectedArea)
    {
        var triangle = new Triangle(sideA, sideB, sideC);
        triangle.GetArea().Should().BeApproximately(expectedArea, 0.00000001);
    }

    [Theory]
    [InlineData(3, 4, 5, true)]
    [InlineData(5, 12, 13, true)]
    [InlineData(8, 15, 17, true)]
    [InlineData(3, 4, 6, false)]
    [InlineData(5, 12, 14, false)]
    [InlineData(8, 15, 18, false)]
    public void Triangle_rightness_checking_is_correct(
        double sideA, double sideB, double sideC, bool expected)
    {
        var triangle = new Triangle(sideA, sideB, sideC);
        triangle.IsRight().Should().Be(expected);
    }
    
    
    [Fact]
    public void Canonical_triangle_area()
    {
        var triangle = new Triangle(3, 4, 5);
        var area = triangle.GetArea();
        
        Assert.Equal(6, area);
    }

    [Fact]
    public void Equilateral_triangle()
    {
        var eps = 1e-16;
        var triangle = new Triangle(1, 1, 1);
        var area = triangle.GetArea();
        var expectedArea = Math.Sqrt(3.0 / 16);
        
        Assert.InRange(area, expectedArea - eps, expectedArea + eps);
    }

    [Fact]
    public void Invalid_triangle_not_allowed()
    {
        Assert.Throws<ArgumentException>(() =>  new Triangle(1, 2, 4));
        Assert.Throws<ArgumentException>(() =>  new Triangle(4, 2, 1));
        Assert.Throws<ArgumentException>(() =>  new Triangle(2, 4, 1));
    }
}

























