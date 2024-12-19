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
            } else if (SolveFunc(function, pointB) == 0)
            {
                return new List<(double, double, double)>() {(0, 0, pointB)};
            }
            
            var valuesHistory = new List<(double, double, double)>();

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

        //  Поиск точки минимума методом золотого сечения 
        public static double FindMinimumByGoldenSection(string functionExpression, double parametrA, double parametrB, double epsilon)
        {
            var expression = ConvertExpressionToFunctionFromString(functionExpression);
            do
            {
                var firstDot = parametrB - (parametrB - parametrA) / Phi;
                var secondDot = parametrA + (parametrB - parametrA) / Phi;
                if (SolveFunc(expression, firstDot) >= SolveFunc(expression, secondDot))
                {
                    parametrA = firstDot;
                } else
                {
                    parametrB = secondDot;
                }
            } while (Math.Abs(parametrB - parametrA) > epsilon);
            return (parametrA + parametrB) / 2;
        }


        //  Поиск точки максимума методом золотого сечения 
        public static double FindMaximumByGoldenSection(string functionExpression, double parametrA, double parametrB, double epsilon)
        {
            var expression = ConvertExpressionToFunctionFromString(functionExpression);
            do
            {
                var firstDot = parametrB - (parametrB - parametrA) / Phi;
                var secondDot = parametrA + (parametrB - parametrA) / Phi;
                if (SolveFunc(expression, firstDot) <= SolveFunc(expression, secondDot))
                {
                    parametrA = firstDot;
                } else
                {
                    parametrB = secondDot;
                }
            } while (Math.Abs(parametrB - parametrA) > epsilon);
            return (parametrA + parametrB) / 2;
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

        // конвертирует выражение из типа строка в тип Function
        public static Function ConvertExpressionToFunctionFromString(string functionExpression)
        {
            return new Function("f(x) = " + functionExpression);
        }
    }
}