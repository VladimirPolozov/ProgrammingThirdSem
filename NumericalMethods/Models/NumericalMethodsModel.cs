using System;
using org.mariuszgromada.math.mxparser;
using Expression = org.mariuszgromada.math.mxparser.Expression;

namespace ProgrammingThirdSem.NumericalMethods.Models
{
    public static class NumericalMethodsModel
    {
        private static readonly double Phi = (1 + Math.Sqrt(5)) / 2;
        private const double DefaultDelta = 0.0000001F;

        //  Поиск точки пересечения графика функции с осью абсцисс методом дихотомии
        public static double FindPointOfIntersectionDihotomyMethod(string functionExpression, double parametrA, double parametrB, double epsilon)
        {
            Function expression = ConvertExpressionToFunctionFromString(functionExpression);
            double parametrAValue = SolveFunc(expression, parametrA);
            double parametrBValue = SolveFunc(expression, parametrB);
            double middleOfSegment = 0;
            double middleOfSegmentValue;
            double xMin = Math.Round(FindMinimumByGoldenSection(functionExpression, parametrA, parametrB, epsilon), 2, MidpointRounding.AwayFromZero);
            double xMinValue = SolveFunc(expression, xMin);

            if (parametrAValue * xMinValue > 0 && parametrBValue * xMinValue > 0)
            {
                throw new ArgumentException("Функция имеет более не имеет точек пересечения с осью абсцисс на заданном интервале, либо их количество чётно");
            }

            while (parametrB - parametrA > epsilon) {
                middleOfSegment = (parametrA + parametrB) / 2;
                middleOfSegmentValue = SolveFunc(expression, middleOfSegment);

                if (middleOfSegmentValue == 0) {
                    break;
                } else if (parametrAValue == 0) {
                    return parametrA;
                } else if (parametrBValue == 0)
                {
                    return parametrB;
                } else if (parametrAValue * middleOfSegmentValue < 0)
                {
                    parametrB = middleOfSegment;
                } else {
                    parametrA = middleOfSegment;
                    parametrAValue = middleOfSegmentValue;
                }
            }

            return middleOfSegment;
        }

        //  Поиск точки минимума методом золотого сечения 
        public static double FindMinimumByGoldenSection(string functionExpression, double parametrA, double parametrB, double epsilon)
        {
            Function expression = ConvertExpressionToFunctionFromString(functionExpression);
            do
            {
                double firstDot = parametrB - (parametrB - parametrA) / Phi;
                double secondDot = parametrA + (parametrB - parametrA) / Phi;
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
            Function expression = ConvertExpressionToFunctionFromString(functionExpression);
            do
            {
                double firstDot = parametrB - (parametrB - parametrA) / Phi;
                double secondDot = parametrA + (parametrB - parametrA) / Phi;
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
            Function expression = ConvertExpressionToFunctionFromString(functionExpression);
            return parametrB - (SolveFunc(expression, parametrB) / GetDerivative(expression, parametrB));
        }

        // Поиск экстремума (точка минимума или максимума) методом Ньютона
        public static double FindExtremeNewtonMethod(string functionExpression, double parametrB)
        {
            Argument argument = new Argument($"x = {parametrB}");
            Expression firstDerivative = new Expression($"der(({functionExpression}), x)", argument);
            Expression secondDerivative = new Expression($"der(der({functionExpression}, x), x)", argument);
            double firstDerivativeValue = firstDerivative.calculate();
            double secondDerivativeValue = secondDerivative.calculate();
            return parametrB - (firstDerivativeValue / secondDerivativeValue);
        }

        public static double GetDerivative(Function function, double point)
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