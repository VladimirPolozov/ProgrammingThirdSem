using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ProgrammingThirdSem.NumericalMethods.Models;

namespace ProgrammingThirdSem.NumericalMethods.ViewModels
{
    public sealed class NumericalMethodsViewModel : INotifyPropertyChanged
    {
        private PlotModel _plotModel;  // основной класс в библиотеке OxyPlot, используемый для создания графиков и диаграмм
        private string _functionExpression;
        private double _parameterA;
        private double _parameterB;
        private double _epsilon; 
        private string _resultText;
        private int _widthXAxis;
        private int _widthYAxis;
        private int _countOfSingsAfterComma;
        private int _graphicThickness;
        private double _graphicPointsDelta;

        public string FunctionExpression
        {
            get => _functionExpression;
            set
            {
                _functionExpression = value.ToLower();
                OnPropertyChanged(nameof(FunctionExpression));
            }
        }

        public double ParameterA
        {
            get => _parameterA;
            set
            {
                _parameterA = value;
                OnPropertyChanged(nameof(ParameterA));
            }
        }

        public double ParameterB
        {
            get => _parameterB;
            set
            {
                _parameterB = value;
                OnPropertyChanged(nameof(ParameterB));
            }
        }

        public double Epsilon
        {
            get => _epsilon;
            set
            {
                _epsilon = value;
                OnPropertyChanged(nameof(Epsilon));
            }
        }

        public PlotModel PlotModel
        {
            get => _plotModel;
            private set
            {
                _plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }

        public string ResultText
        {
            get => _resultText;
            set
            {
                _resultText = value;
                OnPropertyChanged(nameof(ResultText));
            }
        }

        public int WidthXAxis
        {
            get => _widthXAxis;
            set
            {
                _widthXAxis = value;
                OnPropertyChanged(nameof(WidthXAxis));
            }
        }

        public int WidthYAxis
        {
            get => _widthYAxis;
            set
            {
                _widthYAxis = value;
                OnPropertyChanged(nameof(WidthYAxis));
            }
        }

        public int CountOfSingsAfterComma
        {
            get => _countOfSingsAfterComma;
            set
            {
                _countOfSingsAfterComma = value;
                OnPropertyChanged(nameof(CountOfSingsAfterComma));
            }
        }

        public double GraphicPointsDelta
        {
            get => _graphicPointsDelta;
            set
            {
                _graphicPointsDelta = value;
                OnPropertyChanged(nameof(GraphicPointsDelta));
            }
        }

        public int GraphicThickness
        {
            get => _graphicThickness;
            set
            {
                _graphicThickness = value;
                OnPropertyChanged(nameof(GraphicThickness));
            }
        }

        // Команда для вызова метода
        public ICommand ConstructPlotCommand { get; }

        public NumericalMethodsViewModel()
        {
            // вставляем в форму данные по умолчанию
            SetDefaultData();

            // Привязываем команды к методу
            ConstructPlotCommand = new RelayCommand(_ => ConstructPlot());

            // Инициализируем пустой график
            PlotModel = new PlotModel { Title = "График функции" };
        }

        private void SetDefaultData()
        {
            FunctionExpression = "x";
            ParameterA = -50;
            ParameterB = 50;
            Epsilon = 0.01;
            WidthXAxis = 50;
            WidthYAxis = 50;
            CountOfSingsAfterComma = 2;
            GraphicPointsDelta = 0.5;
            GraphicThickness = 2;
            _resultText = "";
        }

        private void FindPointOfIntersectionDichotomy()
        {
            try
            {
                var result = NumericalMethodsModel.FindPointOfIntersectionDihotomyMethod(FunctionExpression, ParameterA, ParameterB, Epsilon);
                ResultText = $"Точка пересечения (x): {Math.Round(result, CountOfSingsAfterComma, MidpointRounding.AwayFromZero)}";
                ConstructPlot(result);
            } catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void FindPointOfIntersectionNewton()
        {
            var nextPoint = _parameterB;
            try {
                double currentPoint;
                do
                {
                    currentPoint = nextPoint;
                    nextPoint = NumericalMethodsModel.FindPointOfIntersectionNewtonMethod(FunctionExpression, currentPoint);
                    ConstructPlot(currentPoint);
                    MessageBox.Show($"Промежуточный результат: x_i = {Math.Round(nextPoint, CountOfSingsAfterComma, MidpointRounding.AwayFromZero)}");
                } while (Math.Abs(nextPoint - currentPoint) > _epsilon);
                ResultText = $"Точка пересечения (x): {Math.Round(nextPoint, CountOfSingsAfterComma, MidpointRounding.AwayFromZero)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void FindExtremeNewton()
        {
            var nextPoint = _parameterB;
            try
            {
                double currentPoint;
                do
                {
                    currentPoint = nextPoint;
                    nextPoint = NumericalMethodsModel.FindExtremeNewtonMethod(FunctionExpression, currentPoint);
                    ConstructPlot(nextPoint);
                    MessageBox.Show($"Промежуточный результат: x_i = {Math.Round(nextPoint, CountOfSingsAfterComma, MidpointRounding.AwayFromZero)}");
                } while (Math.Abs(nextPoint - currentPoint) > _epsilon);
                ResultText = $"Точка пересечения (x): {Math.Round(nextPoint, CountOfSingsAfterComma, MidpointRounding.AwayFromZero)}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void FindMinimum()
        {
            try
            {
                var result = NumericalMethodsModel.FindMinimumByGoldenSection(FunctionExpression, ParameterA, ParameterB, Epsilon);
                ResultText = $"Точка минимума (x): {Math.Round(result, CountOfSingsAfterComma, MidpointRounding.AwayFromZero)}";
                ConstructPlot(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void FindMaximum()
        {
            try
            {
                var result = NumericalMethodsModel.FindMaximumByGoldenSection(FunctionExpression, ParameterA, ParameterB, Epsilon);
                ResultText = $"Точка максимума (x): {Math.Round(result, CountOfSingsAfterComma, MidpointRounding.AwayFromZero)}";
                ConstructPlot(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ConstructPlot(double markCoordX)
        {
            // Обновляем график
            PlotModel = new PlotModel { Title = "График функции" };
            var series = new LineSeries { Title = "f(x)", StrokeThickness = GraphicThickness };
            var mark = new LineSeries { Title = "f(x)", StrokeThickness = 1, Color = OxyColors.Blue };

            // Настройка оси X
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom, // Ось X снизу
                Minimum = WidthXAxis / -2,  // Минимум по X
                Maximum = WidthXAxis / 2,   // Максимум по X
                Title = "",  // Подпись оси
                // MajorGridlineStyle = LineStyle.Solid, // Основная сетка
                // MinorGridlineStyle = LineStyle.Dot,   // Второстепенная сетка
                PositionAtZeroCrossing = true // Ось X пересекается с осью Y в 0
            };

            // Настройка оси Y
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left, // Ось Y слева
                Minimum = WidthYAxis / -2,  // Минимум по Y
                Maximum = WidthYAxis / 2,   // Максимум по Y
                Title = "",  // Подпись оси
                // MajorGridlineStyle = LineStyle.Solid, // Основная сетка
                // MinorGridlineStyle = LineStyle.Dot,   // Второстепенная сетка
                PositionAtZeroCrossing = true // Ось Y пересекается с осью X в 0
            };

            // Добавляем оси в модель
            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);

            // Рисуем график
            for (var x = xAxis.Minimum; x <= xAxis.Maximum; x += _graphicPointsDelta)
            {
                var y = NumericalMethodsModel.SolveFunc(NumericalMethodsModel.ConvertExpressionToFunctionFromString(FunctionExpression), x);
                series.Points.Add(new DataPoint(x, y));
            }

            // Рисуем вертикальную линию
            for (var y = yAxis.Minimum; y <= yAxis.Maximum; y += 1)
            {
                mark.Points.Add(new DataPoint(markCoordX, y));
            }

            PlotModel.Series.Add(series);
            PlotModel.Series.Add(mark);
            PlotModel.InvalidatePlot(true);
        }

        private void ConstructPlot()
        {
            // Обновляем график
            PlotModel = new PlotModel { Title = "График функции" };
            var series = new LineSeries { Title = "f(x)", StrokeThickness = GraphicThickness };

            // Настройка оси X
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom, // Ось X снизу
                Minimum = WidthXAxis / -2,  // Минимум по X
                Maximum = WidthXAxis /  2,   // Максимум по X
                Title = "",  // Подпись оси
                // MajorGridlineStyle = LineStyle.Solid, // Основная сетка
                // MinorGridlineStyle = LineStyle.Dot,   // Второстепенная сетка
                PositionAtZeroCrossing = true // Ось X пересекается с осью Y в 0
            };

            // Настройка оси Y
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left, // Ось Y слева
                Minimum = WidthYAxis / -2,  // Минимум по Y
                Maximum = WidthYAxis / 2,   // Максимум по Y
                Title = "",  // Подпись оси
                // MajorGridlineStyle = LineStyle.Solid, // Основная сетка
                // MinorGridlineStyle = LineStyle.Dot,   // Второстепенная сетка
                PositionAtZeroCrossing = true // Ось Y пересекается с осью X в 0
            };

            // Добавляем оси в модель
            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);

            // Рисуем график
            for (var x = xAxis.Minimum; x <= xAxis.Maximum; x += _graphicPointsDelta)
            {
                var y = NumericalMethodsModel.SolveFunc(NumericalMethodsModel.ConvertExpressionToFunctionFromString(FunctionExpression), x);
                series.Points.Add(new DataPoint(x, y));
            }

            PlotModel.Series.Add(series);
            PlotModel.InvalidatePlot(true);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}