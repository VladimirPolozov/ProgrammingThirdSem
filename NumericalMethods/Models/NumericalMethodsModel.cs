using System;
using System.Collections.Generic;
using System.Windows;
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
        public static double FindPointOfIntersectionNewtonMethod(string functionExpression, double parametrB)
        {
            var expression = ConvertExpressionToFunctionFromString(functionExpression);
            return parametrB - (SolveFunc(expression, parametrB) / GetDerivative(expression, parametrB));
        }

        // Поиск экстремума (точка минимума или максимума) методом Ньютона
        public static double FindExtremeNewtonMethod(string functionExpression, double parametrB)
        {
            var argument = new Argument($"x = {parametrB}");
            var firstDerivative = new Expression($"der(({functionExpression}), x)", argument);
            var secondDerivative = new Expression($"der(der({functionExpression}, x), x)", argument);
            var firstDerivativeValue = firstDerivative.calculate();
            var secondDerivativeValue = secondDerivative.calculate();
            return parametrB - (firstDerivativeValue / secondDerivativeValue);
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