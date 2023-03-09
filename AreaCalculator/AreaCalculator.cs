namespace AreaCalculator;

public interface IFigure // абстракция - расширяемая библиотека (новую фигуру можно добавить, не меняя базовый код библиотеки)
{
    public double GetArea();
    public string ToString();
    public double[] GetDimensions();
}

public class Circle : IFigure // тип фигуры определяется явно
{
    private readonly double _radius;
    public double Radius => _radius;
    public Circle( double radius )
    {
        if (double.IsNaN(radius) || double.IsInfinity(radius) || radius <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be greater than zero.");
        }
        _radius = radius;
    }

    public double GetArea()
    {
        return _radius * _radius * Math.PI;
    }

    public double[] GetDimensions()
    {
        return new [] {_radius};
    }
    
    public override string ToString() => $"Type: {GetType().Name}, Radius = {_radius}";
}

public class Triangle : IFigure // тип фигуры определяется явно
{
    public const string TriangleWithGivenSidesDoesntExist = "Triangle with given sides doesn't exist!";
    public double SideA { get; }
    public double SideB { get; }
    public double SideC { get; }
    
    // private readonly double _a, _b, _c;
    
    public Triangle(double sideA, double sideB, double sideC)
    {
        if (sideA is <= 0 or double.NaN || double.IsInfinity(sideA)) 
            throw new ArgumentOutOfRangeException(nameof(sideA));
        if (sideB is <= 0 or double.NaN || double.IsInfinity(sideB)) 
            throw new ArgumentOutOfRangeException(nameof(sideB));
        if (sideC is <= 0 or double.NaN || double.IsInfinity(sideC)) 
            throw new ArgumentOutOfRangeException(nameof(sideC));
        
        if (!Exists(sideA, sideB, sideC))
        {
            throw new ArgumentException(TriangleWithGivenSidesDoesntExist);
        }

        SideA = sideA;
        SideB = sideB;
        SideC = sideC;
    }

    public static bool Exists(double a, double b, double c)
    {
        return a + b > c && a + c > b && b + c > a;
    }
 
    public bool IsRight() // Проверка на то, является ли треугольник прямоугольным
    {
        double[] sides = { SideA, SideB, SideC };
        Array.Sort(sides);
        return Math.Abs(sides[2] * sides[2] - (sides[0] * sides[0] + sides[1] * sides[1])) < 0.0001;
    }

    public double GetArea()
    {
        // вычисление по формуле Герона происходит единообразно для всех видов треугольников
        // проверка на то, прямоугольный ли треугольник, не требуется
        double s = (SideA + SideB + SideC) / 2;
        return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));    
    }

    public double[] GetDimensions()
    {
        return new[] { SideA, SideB, SideC };
    }
    
    public override string ToString() => $"Type: {GetType().Name}, Sides = {SideA}, {SideB}, {SideC}";
}