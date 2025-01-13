using System;

namespace ProgrammingThirdSem.Ales.Models
{
    public abstract class AlesModel
    {
        public abstract class GaussMethod
        {
            public static double[] Solve(double[,] coefficients, double[] constants)
            {
                int n = constants.Length;
                double[,] L = new double[n, n];
                double[,] U = new double[n, n];

                // LU-разложение
                for (int i = 0; i < n; i++)
                {
                    for (int j = i; j < n; j++)
                    {
                        U[i, j] = coefficients[i, j];
                        for (int k = 0; k < i; k++)
                        {
                            U[i, j] -= L[i, k] * U[k, j];
                        }
                    }
                    for (int j = i + 1; j < n; j++)
                    {
                        L[j, i] = coefficients[j, i];
                        for (int k = 0; k < i; k++)
                        {
                            L[j, i] -= L[j, k] * U[k, i];
                        }
                        L[j, i] /= U[i, i];
                    }
                    L[i, i] = 1; // диагональные элементы L равны 1
                }

                // Решение Ly = b
                double[] y = new double[n];
                for (int i = 0; i < n; i++)
                {
                    y[i] = constants[i];
                    for (int j = 0; j < i; j++)
                    {
                        y[i] -= L[i, j] * y[j];
                    }
                }

                // Решение Ux = y
                double[] x = new double[n];
                for (int i = n - 1; i >= 0; i--)
                {
                    x[i] = y[i];
                    for (int j = i + 1; j < n; j++)
                    {
                        x[i] -= U[i, j] * x[j];
                    }
                    x[i] /= U[i, i];
                }

                return x;
            }
        }

        public abstract class JordanGaussMethod
        {public static double[] Solve(double[,] coefficients, double[] constants)
            {
                var numberOfVariables = constants.Length;

                // Расширение матрицы
                for (var rowIndex = 0; rowIndex < numberOfVariables; ++rowIndex)
                {
                    // Проверка на нуль перед дальнейшим ходом
                    if (Math.Abs(coefficients[rowIndex, rowIndex]) < 1e-10)
                    {
                        throw new InvalidOperationException($"Ведущий элемент в строке {rowIndex + 1} равен нулю. Система либо несогласованная, либо имеет бесконечно много решений.");
                    }

                    // Прямой ход для нормализации и исключения
                    for (var pivotIndex = 0; pivotIndex < numberOfVariables; ++pivotIndex)
                    {
                        if (rowIndex != pivotIndex)
                        {
                            var factor = coefficients[pivotIndex, rowIndex] / coefficients[rowIndex, rowIndex];
                            for (var colIndex = 0; colIndex < numberOfVariables; colIndex++)
                            {
                                coefficients[pivotIndex, colIndex] -= factor * coefficients[rowIndex, colIndex];
                            }
                            constants[pivotIndex] -= factor * constants[rowIndex];
                        }
                    }
                }

                // Нормализация матрицы
                for (var rowIndex = 0; rowIndex < numberOfVariables; ++rowIndex)
                {
                    var normalizingFactor = coefficients[rowIndex, rowIndex];

                    // Проверка на нуль перед делением
                    if (Math.Abs(normalizingFactor) < 1e-10)
                    {
                        throw new InvalidOperationException($"Деление на ноль при нормализации. Ведущий элемент в строке {rowIndex + 1} равен нулю.");
                    }

                    for (var colIndex = 0; colIndex < numberOfVariables; colIndex++)
                    {
                        coefficients[rowIndex, colIndex] /= normalizingFactor;
                    }
                    constants[rowIndex] /= normalizingFactor;
                }

                return constants;
            }
        }

        public abstract class KramerMethod
        {
            // Метод для вычисления определителя матрицы с использованием метода Гаусса
    private static double CalculateDeterminant(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        double[,] tempMatrix = (double[,])matrix.Clone();
        double determinant = 1;

        for (int i = 0; i < n; i++)
        {
            // Поиск максимального элемента в столбце
            double maxElement = Math.Abs(tempMatrix[i, i]);
            int maxRow = i;
            for (int k = i + 1; k < n; k++)
            {
                if (Math.Abs(tempMatrix[k, i]) > maxElement)
                {
                    maxElement = Math.Abs(tempMatrix[k, i]);
                    maxRow = k;
                }
            }

            // Меняем местами текущую строку и строку с максимальным элементом
            if (maxRow != i)
            {
                for (int k = i; k < n; k++)
                {
                    double temp = tempMatrix[maxRow, k];
                    tempMatrix[maxRow, k] = tempMatrix[i, k];
                    tempMatrix[i, k] = temp;
                }
                determinant *= -1; // Изменение знака определителя
            }

            // Приведение к верхнетреугольному виду
            for (int k = i + 1; k < n; k++)
            {
                double factor = tempMatrix[k, i] / tempMatrix[i, i];
                for (int j = i; j < n; j++)
                {
                    tempMatrix[k, j] -= factor * tempMatrix[i, j];
                }
            }
            determinant *= tempMatrix[i, i]; // Умножаем на диагональный элемент
        }

        return determinant;
    }

    // Метод для решения системы линейных уравнений
    public static double[] Solve(double[,] coefficientMatrix, double[] constantTerms)
    {
        var size = coefficientMatrix.GetLength(0); // Получаем размерность матрицы
        var determinant = CalculateDeterminant(coefficientMatrix);
        var solution = new double[size];

        // Проверка на нуль определителя
        if (Math.Abs(determinant) < 1e-10)
        {
            throw new InvalidOperationException("Определитель матрицы равен нулю. Система либо несогласованная, либо имеет бесконечно много решений.");
        }

        // Решение для каждой переменной
        for (var column = 0; column < size; ++column)
        {
            var tempMatrix = (double[,])coefficientMatrix.Clone(); // Клонируем матрицу коэффициентов
            for (var row = 0; row < size; ++row)
            {
                tempMatrix[row, column] = constantTerms[row]; // Заменяем столбец на свободные члены
            }
            solution[column] = CalculateDeterminant(tempMatrix) / determinant; // Вычисляем значение переменной
        }

        return solution;
    }
        }
    }
}