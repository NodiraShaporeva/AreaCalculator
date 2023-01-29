namespace AreaCalculator;

public class AreaCalculator
{
    // что делать с некорректными входными данными?
    // по образцу стандартной библиотеки Math будем возвращать NaN
    // было бы лучше выбрасывать Exception?
    // считать ли нулевую площадь ошибкой?
    // поскольку в требованиях это не оговорено, сделаем как удобнее и будем считать нулевую площадь корректной
    public static double CircleArea(double radius)
    {
        if (radius < 0) return Double.NaN;
        return radius * radius * Math.PI;
    }

    public static double TriangleArea(double a, double b, double c)
    {
        if (a < 0 || b < 0 || c < 0) return Double.NaN;
        // вычисление по формуле Герона происходит единообразно для всех видов треугольников
        // проверка на то, прямоугольный ли треугольник, не требуется
        double p = (a + b + c) / 2;
        // для несуществующего треугольника (c > a + b, etc.) выражение под корнем будет отрицательным
        // Sqrt вернет NaN. Поэтому отдельная проверка существования треугольника не требуется
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }
    
    //  что значит "без знания типа фигуры в compile-time"?
    // обобщенный метод, например, может быть таким
    public static double Area(double[] sides)
    {
        return sides.Length switch
        {
            1 => CircleArea(sides[0]), //круг
            3 => TriangleArea(sides[0], sides[1], sides[2]), //треугольник
            _ => Double.NaN // не знаем, что это такое, действуем по соглашению об ошибках
        };
    }
}