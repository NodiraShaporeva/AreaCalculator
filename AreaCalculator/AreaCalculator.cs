namespace AreaCalculator;

public interface IFigure // абстракция, расширяемая библиотека: (новую фигуру можно добавить, не меняя базовый код библиотеки)
{
    public double GetArea();
    public string ToString();
    public double[] GetDimensions();
}

public class Circle : IFigure // тип фигуры определяется явно
{
    private readonly double _radius;
    public Circle( double radius )
    {
        if ( radius <= 0.0 )
            throw new ArgumentException( "Provided radius is not a positive double" );
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
    private readonly double _a, _b, _c;
    
    public Triangle( double a, double b, double c )
    {
        if ( a <= 0 || b <= 0 || c <= 0 )
            throw new ArgumentException( "Provided side length is not a positive double" );
        if ( a + b < c || b + c < a || a + c < b )
            throw new ArgumentException( "Provided sides do not form a triangle" );
        _a = a;
        _b = b;
        _c = c;
    }
 
    public bool IsRight( double eps = 1E-6 ) // Проверка на то, является ли треугольник прямоугольным
    {
        double a2 = _a * _a;
        double b2 = _b * _b;
        double c2 = _c * _c;
        return Math.Abs(a2 + b2 - c2) < eps || Math.Abs(b2+c2 - a2) < eps || Math.Abs(a2+c2 - b2) < eps;
    }

    public double GetArea()
    {
        // вычисление по формуле Герона происходит единообразно для всех видов треугольников
        // проверка на то, прямоугольный ли треугольник, не требуется
        double p = (_a + _b + _c) / 2;
        return Math.Sqrt(p * (p - _a) * (p - _b) * (p - _c));    
    }

    public double[] GetDimensions()
    {
        return new[] { _a, _b, _c };
    }
    
    public override string ToString() => $"Type: {GetType().Name}, Sides = {_a}, {_b}, {_c}";
}