using System;
using System.Collections.Generic;
using System.Linq;
using org.mariuszgromada.math.mxparser;
using Expression = org.mariuszgromada.math.mxparser.Expression;

namespace ProgrammingThirdSem.NumericalMethods.Models
{
    public static class NumericalMethodsModel
    {
        private static readonly double Phi = (1 + Math.Sqrt(5)) / 2;
        private const double DefaultDelta = 0.0000001F;

        public static List<(double, double, double)> DichotomyMethod(Function function, double pointA, double pointB, double epsilon)
        {
            var pointAValue = SolveFunc(function, pointA);

            if (pointAValue == 0)
            {
                return new List<(double, double, double)>() {(0, 0, pointA)};
            }
            if (SolveFunc(function, pointB) == 0)
            {
                return new List<(double, double, double)>() {(0, 0, pointB)};
            }

            var valuesHistory = new List<(double, double, double)>() {};

            while (pointB - pointA > epsilon)
            {
                var middlePoint = (pointA + pointB) / 2;
                var middlePointValue = SolveFunc(function, middlePoint);
                valuesHistory.Add((pointA, pointB, middlePoint));

                if (middlePointValue == 0)
                {
                    return valuesHistory;
                }

                if (pointAValue * middlePointValue < 0)
                {
                    pointB = middlePoint;
                }
                else
                {
                    pointA = middlePoint;
                    pointAValue = middlePointValue;
                }
            }
            
            valuesHistory.Add((pointA, pointB, (pointA + pointB) / 2));
            return valuesHistory;
        }
        
        public static List<(double, double, double)> GoldenRatioMinMethod(Function function, double pointA, double pointB, double epsilon)
        {
            var valuesHistory = new List<(double, double, double)>();

            while (Math.Abs(pointB - pointA) > epsilon)
            {
                var firstPoint = pointB - (pointB - pointA) / Phi;
                var secondPoint = pointA + (pointB - pointA) / Phi;

                valuesHistory.Add((pointA, pointB, (firstPoint + secondPoint) / 2)); // Сохраняем границы и среднюю точку

                if (SolveFunc(function, firstPoint) >= SolveFunc(function, secondPoint))
                {
                    pointA = firstPoint;
                }
                else
                {
                    pointB = secondPoint;
                }
            }

            // Добавляем финальную среднюю точку
            valuesHistory.Add((pointA, pointB, (pointA + pointB) / 2));
            return valuesHistory;
        }
        
        public static List<(double, double, double)> GoldenRatioMaxMethod(Function function, double pointA, double pointB, double epsilon)
        {
            var valuesHistory = new List<(double, double, double)>();

            while (Math.Abs(pointB - pointA) > epsilon)
            {
                var firstPoint = pointB - (pointB - pointA) / Phi;
                var secondPoint = pointA + (pointB - pointA) / Phi;

                valuesHistory.Add((pointA, pointB, (firstPoint + secondPoint) / 2)); // Сохраняем границы и среднюю точку

                if (SolveFunc(function, firstPoint) <= SolveFunc(function, secondPoint))
                {
                    pointA = firstPoint;
                }
                else
                {
                    pointB = secondPoint;
                }
            }

            // Добавляем финальную среднюю точку
            valuesHistory.Add((pointA, pointB, (pointA + pointB) / 2));
            return valuesHistory;
        }
        
        // Поиск точки пересечения (нуль функции) методом Ньютона
        public static List<double> NewtonNullMethod(Function function, double pointB, double epsilon)
        {
            var valuesHistory = new List<double>() {pointB};

            while (true)
            {
                var functionValue = SolveFunc(function, pointB);
                var derivativeValue = GetDerivative(function, pointB);
                
                if (Math.Abs(functionValue) < epsilon)
                {
                    break; // Достигли нуля функции
                }

                pointB -= functionValue / derivativeValue; // Обновляем точку
                valuesHistory.Add(pointB); // Сохраняем текущее значение точки, значение функции и значение производной
            }

            return valuesHistory;
        }

        // Поиск экстремума (точка минимума или максимума) методом Ньютона
        public static List<double> NewtonExtremeMethod(Function function, double pointB, double epsilon)
        {
            var valuesHistory = new List<double>() {pointB};

            while (true)
            {
                var derivativeValue = GetDerivative(function, pointB);
                var secondDerivativeValue = GetDerivative(function, pointB + DefaultDelta); // Для вычисления второй производной

                // Проверяем, если производная слишком мала, чтобы избежать деления на ноль
                if (Math.Abs(derivativeValue) < epsilon)
                {
                    break; // Достигли точки экстремума
                }

                // Обновляем точку
                pointB -= derivativeValue / secondDerivativeValue; // Используем вторую производную для обновления точки
                
                // Сохраняем текущее значение точки, значение производной и вторую производную
                valuesHistory.Add(pointB);
            }

            return valuesHistory;
        }
        
        // Поиск экстремума метод координатного спуска
        public static (double, double) CoordinateDescentMethod(string functionExpression, double pointA, double pointB, double epsilon, bool isMinimumSearched)
        {
            var middlePoint = (pointA + pointB) / 2;
            double pointX, pointY;
            var nextPointX = middlePoint;
            var nextPointY = middlePoint;

            do
            {
                pointX = nextPointX;
                pointY = nextPointY;

                var functionAccordingX = ConvertExpressionToFunctionFromString(
                    functionExpression.Replace("y", pointY.ToString()));
                nextPointX = isMinimumSearched ?
                    GoldenRatioMinMethod(functionAccordingX, pointA, pointB, epsilon).Last().Item3 :
                    GoldenRatioMaxMethod(functionAccordingX, pointA, pointB, epsilon).Last().Item3;

                var functionAccordingY = ConvertExpressionToFunctionFromString(
                    functionExpression.Replace("x", pointX.ToString()));
                nextPointY = isMinimumSearched ?
                    GoldenRatioMinMethod(functionAccordingY, pointA, pointB, epsilon).Last().Item3 :
                    GoldenRatioMaxMethod(functionAccordingY, pointA, pointB, epsilon).Last().Item3;
            } while (Math.Abs(nextPointX - pointX) > epsilon || Math.Abs(nextPointY - pointY) > epsilon);

            return (nextPointX, nextPointY);
        }
        
        // поиск интеграла методом прямоугольников (средние прямольники)
        public static List<(int, double)> RectangleMethod(Function function, double pointA, double pointB, double epsilon)
        {
            var numberOfRectangles = 1; // Начальное количество прямоугольников
            var valuesHistory = new List<(int, double)>();

            // Инициализируем предыдущую площадь значением, отличным от текущей
            var previousArea = double.MaxValue; // Устанавливаем в максимально возможное значение

            do
            {
                var width = (pointB - pointA) / numberOfRectangles; // Ширина каждого прямоугольника
                double area = 0; // Текущая площадь

                for (var i = 0; i < numberOfRectangles; ++i)
                {
                    // Находим среднюю точку для текущего прямоугольника
                    var x = pointA + (i + 0.5) * width; // Средняя точка
                    var height = SolveFunc(function, x); // Высота прямоугольника
                    area += height * width; // Добавляем площадь текущего прямоугольника
                }

                // Сохраняем количество разбиений и соответствующую площадь
                valuesHistory.Add((numberOfRectangles, area));

                // Проверяем, достигнута ли нужная точность
                if (Math.Abs(area - previousArea) <= epsilon)
                {
                    break; // Выходим из цикла, если достигнута нужная точность
                }

                // Увеличиваем количество прямоугольников для следующей итерации
                previousArea = area; // Обновляем предыдущую площадь
                numberOfRectangles *= 2; // Увеличиваем количество разбиений

                // Предотвращаем слишком большое количество разбиений
                if (numberOfRectangles > 10000) // Например, ограничим до 10,000
                {
                    throw new InvalidOperationException("Слишком большое количество разбиений. Проверьте параметры.");
                }

            } while (true); // Бесконечный цикл, но с условием выхода внутри

            return valuesHistory;
        }
        
        // поиск интеграла методом прямоугольников (левые прямоугольники)
        public static List<(int, double)> LeftRectangleMethod(Function function, double pointA, double pointB, double epsilon)
        {
            var numberOfRectangles = 1; // Начальное количество прямоугольников
            var valuesHistory = new List<(int, double)>();

            // Инициализируем предыдущую площадь значением, отличным от текущей
            var previousArea = double.MaxValue; // Устанавливаем в максимально возможное значение

            do
            {
                var width = (pointB - pointA) / numberOfRectangles; // Ширина каждого прямоугольника
                double area = 0; // Текущая площадь

                for (var i = 0; i < numberOfRectangles; ++i)
                {
                    // Находим левую точку для текущего прямоугольника
                    var x = pointA + i * width; // Левая точка
                    var height = SolveFunc(function, x); // Высота прямоугольника
                    area += height * width; // Добавляем площадь текущего прямоугольника
                }

                // Сохраняем количество разбиений и соответствующую площадь
                valuesHistory.Add((numberOfRectangles, area));

                // Проверяем, достигнута ли нужная точность
                if (Math.Abs(area - previousArea) <= epsilon)
                {
                    break; // Выходим из цикла, если достигнута нужная точность
                }

                // Увеличиваем количество прямоугольников для следующей итерации
                previousArea = area; // Обновляем предыдущую площадь
                numberOfRectangles *= 2; // Увеличиваем количество разбиений

                // Предотвращаем слишком большое количество разбиений
                if (numberOfRectangles > 10000) // Например, ограничим до 10,000
                {
                    throw new InvalidOperationException("Слишком большое количество разбиений. Проверьте параметры.");
                }

            } while (true); // Бесконечный цикл, но с условием выхода внутри

            return valuesHistory;
        }
        
        // поиск интеграла методом прямоугольников (правые прямоугольники)
        public static List<(int, double)> RightRectangleMethod(Function function, double pointA, double pointB, double epsilon)
        {
            var numberOfRectangles = 1; // Начальное количество прямоугольников
            var valuesHistory = new List<(int, double)>();

            // Инициализируем предыдущую площадь значением, отличным от текущей
            var previousArea = double.MaxValue; // Устанавливаем в максимально возможное значение

            do
            {
                var width = (pointB - pointA) / numberOfRectangles; // Ширина каждого прямоугольника
                double area = 0; // Текущая площадь

                for (var i = 0; i < numberOfRectangles; ++i)
                {
                    // Находим правую точку для текущего прямоугольника
                    var x = pointA + (i + 1) * width; // Правая точка
                    var height = SolveFunc(function, x); // Высота прямоугольника
                    area += height * width; // Добавляем площадь текущего прямоугольника
                }

                // Сохраняем количество разбиений и соответствующую площадь
                valuesHistory.Add((numberOfRectangles, area));

                // Проверяем, достигнута ли нужная точность
                if (Math.Abs(area - previousArea) <= epsilon)
                {
                    break; // Выходим из цикла, если достигнута нужная точность
                }

                // Увеличиваем количество прямоугольников для следующей итерации
                previousArea = area; // Обновляем предыдущую площадь
                numberOfRectangles *= 2; // Увеличиваем количество разбиений

                // Предотвращаем слишком большое количество разбиений
                if (numberOfRectangles > 10000) // Например, ограничим до 10,000
                {
                    throw new InvalidOperationException("Слишком большое количество разбиений. Проверьте параметры.");
                }

            } while (true); // Бесконечный цикл, но с условием выхода внутри

            return valuesHistory;
        }

        public static List<(int, double)> TrapezoidMethod(Function function, double pointA, double pointB, double epsilon)
        {
            var numberOfTrapezoids = 1; // Начальное количество трапеций
            var valuesHistory = new List<(int, double)>();

            // Инициализируем предыдущую площадь значением, отличным от текущей
            var previousArea = double.MaxValue; // Устанавливаем в максимально возможное значение

            do
            {
                var width = (pointB - pointA) / numberOfTrapezoids; // Ширина каждой трапеции
                double area = 0; // Текущая площадь

                // Вычисляем площадь трапеций
                for (var i = 0; i < numberOfTrapezoids; ++i)
                {
                    var x0 = pointA + i * width; // Левый конец трапеции
                    var x1 = pointA + (i + 1) * width; // Правый конец трапеции
                    var height0 = SolveFunc(function, x0); // Высота левой стороны
                    var height1 = SolveFunc(function, x1); // Высота правой стороны
                    area += (height0 + height1) * width / 2; // Площадь трапеции
                }

                // Сохраняем количество разбиений и соответствующую площадь
                valuesHistory.Add((numberOfTrapezoids, area));

                // Проверяем, достигнута ли нужная точность
                if (Math.Abs(area - previousArea) <= epsilon)
                {
                    break; // Выходим из цикла, если достигнута нужная точность
                }

                // Увеличиваем количество трапеций для следующей итерации
                previousArea = area; // Обновляем предыдущую площадь
                numberOfTrapezoids *= 2; // Увеличиваем количество разбиений

                // Предотвращаем слишком большое количество разбиений
                if (numberOfTrapezoids > 10000) // Например, ограничим до 10,000
                {
                    throw new InvalidOperationException("Слишком большое количество разбиений. Проверьте параметры.");
                }

            } while (true); // Бесконечный цикл, но с условием выхода внутри

            return valuesHistory;
        }
        
        public static List<(int, double)> ParabolaMethod(Function function, double pointA, double pointB, double epsilon)
        {
            var numberOfIntervals = 2; // Начальное количество интервалов (должно быть четным)
            var valuesHistory = new List<(int, double)>();

            // Инициализируем предыдущую площадь значением, отличным от текущей
            var previousArea = double.MaxValue; // Устанавливаем в максимально возможное значение

            do
            {
                var width = (pointB - pointA) / numberOfIntervals; // Ширина каждого интервала
                double area = 0; // Текущая площадь

                // Вычисляем площадь с использованием метода Симпсона
                for (var i = 0; i < numberOfIntervals; i += 2)
                {
                    var x0 = pointA + i * width; // Левый конец интервала
                    var x1 = pointA + (i + 1) * width; // Средняя точка интервала
                    var x2 = pointA + (i + 2) * width; // Правый конец интервала

                    var height0 = SolveFunc(function, x0); // Высота левой стороны
                    var height1 = SolveFunc(function, x1); // Высота средней точки
                    var height2 = SolveFunc(function, x2); // Высота правой стороны

                    // Площадь параболы
                    area += (height0 + 4 * height1 + height2) * width / 3;
                }

                // Сохраняем количество интервалов и соответствующую площадь
                valuesHistory.Add((numberOfIntervals, area));

                // Проверяем, достигнута ли нужная точность
                if (Math.Abs(area - previousArea) <= epsilon)
                {
                    break; // Выходим из цикла, если достигнута нужная точность
                }

                // Увеличиваем количество интервалов для следующей итерации
                previousArea = area; // Обновляем предыдущую площадь
                numberOfIntervals *= 2; // Увеличиваем количество интервалов

                // Предотвращаем слишком большое количество разбиений
                if (numberOfIntervals > 10000) // Например, ограничим до 10,000
                {
                    throw new InvalidOperationException("Слишком большое количество интервалов. Проверьте параметры.");
                }

            } while (true); // Бесконечный цикл, но с условием выхода внутри

            return valuesHistory;
        }

        private static double GetDerivative(Function function, double point)
        {
            return (SolveFunc(function, point + DefaultDelta) - SolveFunc(function, point - DefaultDelta)) / (2 * DefaultDelta);
        }

        //  Метод для вычисления значения функции в точке x
        public static double SolveFunc(Function function, double x)
        {
            return new Expression($"f({x.ToString().Replace(",", ".")})", function).calculate();
        }
        
        public static double SolveFunc(string functionString, double x)
        {
            var function = ConvertExpressionToFunctionFromString(functionString);
            return new Expression($"f({x.ToString().Replace(",", ".")})", function).calculate();
        }

        // конвертирует выражение из типа строка в тип Function
        private static Function ConvertExpressionToFunctionFromString(string functionExpression)
        {
            return new Function("f(x) = " + functionExpression);
        }
    }
}