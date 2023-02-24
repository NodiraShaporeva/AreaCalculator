using AreaCalculator;

namespace AreaCalculatorTest;

public class AreaCalculatorTests
{
    [Fact]
    public void Unit_circle_area()
    {
        var eps = 1e-16;
        var circle = new Circle(1);
        var area = circle.GetArea();
        
        Assert.InRange(area, Math.PI - eps, Math.PI + eps);
    }
    
    [Fact]
    public void Invalid_circle_not_allowed()
    {
        Assert.Throws<ArgumentException>(() =>  new Circle(0));
        Assert.Throws<ArgumentException>(() =>  new Circle(-3));
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

























