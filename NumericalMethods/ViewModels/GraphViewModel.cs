using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ProgrammingThirdSem.NumericalMethods.Models;

namespace ProgrammingThirdSem.NumericalMethods.ViewModels
{
    public class GraphViewModel<T> : INotifyPropertyChanged
    {
        private readonly Function _function;
        private double _targetDotCoord;
        private List<double> _additionalDotsCoord;
        private double _pointA;
        private double _pointB;
        private int _methodCode;
        
        public double PointA
        {
            get => _pointA;
            set
            {
                _pointA = value;
                OnPropertyChanged(nameof(PointA));
            }
        }
        
        public double PointB
        {
            get => _pointB;
            set
            {
                _pointB = value;
                OnPropertyChanged(nameof(PointB));
            }
        }
        
        public int MethodCode
        {
            get => _methodCode;
            set
            {
                _methodCode = value;
                OnPropertyChanged(nameof(MethodCode));
            }
        }
        
        public double TargetDotCoord
        {
            get => _targetDotCoord;
            set
            {
                _targetDotCoord = value;
                OnPropertyChanged(nameof(TargetDotCoord));
            }
        }
        
        public List<double> AdditionalDotsCoord
        {
            get => _additionalDotsCoord;
            set
            {
                _additionalDotsCoord = value;
                OnPropertyChanged(nameof(AdditionalDotsCoord));
            }
        }
        
        // основной класс в библиотеке OxyPlot, используемый для создания графиков
        private PlotModel _plotModel;
        
        public PlotModel PlotModel
        {
            get => _plotModel;
            set
            {
                _plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }
        
        // параметры графика
        private int _widthXAxis = 100;
        private int _widthYAxis = 100;
        private int _graphicThickness = 2;
        private double _graphicPointsDelta = 0.5;
        
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

        public int GraphicThickness
        {
            get => _graphicThickness;
            set
            {
                _graphicThickness = value;
                OnPropertyChanged(nameof(GraphicThickness));
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
        
        // история вычислений
        private int _iterationsCount;
        private int _currentIterationIndex;
        
        public int IterationsCount
        {
            get => _iterationsCount;
            set
            {
                _iterationsCount = value;
                IterationsInfo = $"Итерация №{CurrentIterationIndex + 1} из {IterationsCount}";
                OnPropertyChanged(nameof(IterationsCount));
            }
        }
        
        public int CurrentIterationIndex
        {
            get => _currentIterationIndex;
            set
            {
                _currentIterationIndex = value;
                IterationsInfo = $"Итерация №{CurrentIterationIndex + 1} из {IterationsCount}";
                OnPropertyChanged(nameof(CurrentIterationIndex));
            }
        }
        
        // Сохраняем историю вычислений
        private List<double> ValuesHistoryDoubleList { get; }
        private List<(double, double, double)> ValuesHistoryTupleList { get; }
        private List<(int, double)> ValuesHistoryIntDouble { get; }
        private string _iterationsInfo;
        private readonly Window _window;

        public string IterationsInfo
        {
            get => _iterationsInfo;
            set
            {
                _iterationsInfo = value;
                OnPropertyChanged(nameof(IterationsInfo));
            }
        }

        private void UpdateGraph()
        {
            // Обновляем график
            PlotModel = new PlotModel { Title = "График функции" };
            var series = new LineSeries { Title = "f(x)", StrokeThickness = GraphicThickness };

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
            for (var x = xAxis.Minimum; x <= xAxis.Maximum; x += GraphicPointsDelta)
            {
                var y = NumericalMethodsModel.SolveFunc(_function, x);
                series.Points.Add(new DataPoint(x, y));
            }
            
            var targetDot = new ScatterSeries { Title = "Точки пересечения", MarkerType = MarkerType.Circle, MarkerSize = 4, MarkerFill = OxyColors.Red };
            var targetDotCoordValue = NumericalMethodsModel.SolveFunc(_function, TargetDotCoord);
            targetDot.Points.Add(new ScatterPoint(TargetDotCoord, targetDotCoordValue));
            
            var additionalDots = new ScatterSeries { Title = "Точки пересечения", MarkerType = MarkerType.Circle, MarkerSize = 2, MarkerFill = OxyColors.Blue };
            foreach (var dot in AdditionalDotsCoord)
            {
                var dotValue = NumericalMethodsModel.SolveFunc(_function, dot);
                additionalDots.Points.Add(new ScatterPoint(dot, dotValue));
            }

            PlotModel.Series.Add(series);
            PlotModel.Series.Add(targetDot);
            PlotModel.Series.Add(additionalDots);
            PlotModel.InvalidatePlot(true);
        }
        
        private void DrawPolygons()
        {
            // Обновляем график
            PlotModel = new PlotModel { Title = "График функции" };
            var series = new LineSeries { Title = "f(x)", StrokeThickness = GraphicThickness };

            // Настройка оси X
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom, // Ось X снизу
                Minimum = WidthXAxis / -2,  // Минимум по X
                Maximum = WidthXAxis / 2,   // Максимум по X
                Title = "",  // Подпись оси
                PositionAtZeroCrossing = true // Ось X пересекается с осью Y в 0
            };

            // Настройка оси Y
            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left, // Ось Y слева
                Minimum = WidthYAxis / -2,  // Минимум по Y
                Maximum = WidthYAxis / 2,   // Максимум по Y
                Title = "",  // Подпись оси
                PositionAtZeroCrossing = true // Ось Y пересекается с осью X в 0
            };

            // Добавляем оси в модель
            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);
            
            // Рисуем график функции
            for (var x = xAxis.Minimum; x <= xAxis.Maximum; x += GraphicPointsDelta)
            {
                var y = NumericalMethodsModel.SolveFunc(_function, x);
                series.Points.Add(new DataPoint(x, y));
            }

            // Создаем серию для трапеций
            var polygonSeries = new LineSeries
            {
                Title = "Трапеции",
                StrokeThickness = 1,
                Color = OxyColors.LightBlue
            };

            var width = (PointB - PointA) / ValuesHistoryIntDouble[CurrentIterationIndex].Item1; // Ширина каждой трапеции
            for (var i = 0; i < ValuesHistoryIntDouble[CurrentIterationIndex].Item1; ++i)
            {
                switch (MethodCode)
                {
                    case 0:
                        // Прямоугольник
                        var x = PointA + (i + 0.5) * width; // Средняя точка
                        var height = NumericalMethodsModel.SolveFunc(_function, x); // Высота прямоугольника
            
                        polygonSeries.Points.Add(new DataPoint(x - width / 2, 0)); // Левый нижний угол
                        polygonSeries.Points.Add(new DataPoint(x - width / 2, height)); // Левый верхний угол
                        polygonSeries.Points.Add(new DataPoint(x + width / 2, height)); // Правый верхний угол
                        polygonSeries.Points.Add(new DataPoint(x + width / 2, 0)); // Правый нижний угол
                        polygonSeries.Points.Add(new DataPoint(x - width / 2, 0)); // Замыкаем прямоугольник
                        break;
                    case 1:
                        // Трапеция
                        var x0 = PointA + i * width; // Левый конец
                        var x1 = PointA + (i + 1) * width; // Правый конец
                        var height0 = NumericalMethodsModel.SolveFunc(_function, x0); // Высота левой стороны
                        var height1 = NumericalMethodsModel.SolveFunc(_function, x1); // Высота правой стороны

                        polygonSeries.Points.Add(new DataPoint(x0, 0)); // Левый нижний угол
                        polygonSeries.Points.Add(new DataPoint(x1, 0)); // Правый нижний угол
                        polygonSeries.Points.Add(new DataPoint(x1, height1)); // Правый верхний угол
                        polygonSeries.Points.Add(new DataPoint(x0, height0)); // Левый верхний угол
                        polygonSeries.Points.Add(new DataPoint(x0, 0)); // Замыкаем трапецию
                        break;
                    case 2:    
                        // Метод Симпсона
                        if (i < ValuesHistoryIntDouble[CurrentIterationIndex].Item1 - 1) // Убедимся, что не выходим за пределы массива
                        {
                            x0 = PointA + i * width; // Левый конец
                            x1 = PointA + (i + 1) * width; // Средняя точка
                            var x2 = PointA + (i + 2) * width; // Правый конец

                            height0 = NumericalMethodsModel.SolveFunc(_function, x0); // Высота левой стороны
                            height1 = NumericalMethodsModel.SolveFunc(_function, x1); // Высота средней точки
                            var height2 = NumericalMethodsModel.SolveFunc(_function, x2); // Высота правой стороны

                            // Добавляем точки для параболы
                            // Используем интерполяцию для создания дополнительных точек
                            for (double t = 0; t <= 1; t += 0.1) // t от 0 до 1 с шагом 0.1
                            {
                                // Уравнение параболы: y = a(x - x0)(x - x2) + height1
                                x = (1 - t) * x0 + t * x2; // Линейная интерполяция между x0 и x2
                                var y = height0 * (1 - t) * (1 - t) + 2 * height1 * (1 - t) * t + height2 * t * t; // Парабола

                                polygonSeries.Points.Add(new DataPoint(x, y)); // Добавляем точку на параболе
                            }

                            // Добавляем нижние углы
                            polygonSeries.Points.Add(new DataPoint(x2, 0)); // Правый нижний угол
                            polygonSeries.Points.Add(new DataPoint(x0, 0)); // Замыкаем параболу
                        }
                        break;
                }
            }

            // Добавляем серии в график
            PlotModel.Series.Add(series);
            PlotModel.Series.Add(polygonSeries);
            PlotModel.InvalidatePlot(true);
        }
        
        public ICommand ConstructGraphCommand { get; }
        public ICommand ShowNextIterationCommand { get; }
        public ICommand ShowPreviousIterationCommand { get; }
        
        public GraphViewModel(List<T> valuesHistory, Function function)
        {
            _function = function;

            if (valuesHistory is List<(double, double, double)> tupleList)
            {
                ValuesHistoryTupleList = tupleList;
                IterationsCount = ValuesHistoryTupleList.Count;
                CurrentIterationIndex = IterationsCount - 1;
                TargetDotCoord = ValuesHistoryTupleList[CurrentIterationIndex].Item3;
                AdditionalDotsCoord = new List<double>() {ValuesHistoryTupleList.Last().Item1, ValuesHistoryTupleList.Last().Item2};
                
                ShowNextIterationCommand = new RelayCommand(_ => NextIterationTupleList());
                ShowPreviousIterationCommand = new RelayCommand(_ => PreviousIterationTupleList());
                ConstructGraphCommand = new RelayCommand(_ => UpdateGraph());
            } else if (valuesHistory is List<double> doubleList)
            {
                ValuesHistoryDoubleList = doubleList;
                IterationsCount = ValuesHistoryDoubleList.Count;
                CurrentIterationIndex = IterationsCount - 1;
                TargetDotCoord = ValuesHistoryDoubleList[CurrentIterationIndex];
                AdditionalDotsCoord = new List<double>();
                
                ShowNextIterationCommand = new RelayCommand(_ => NextIterationDoubleList());
                ShowPreviousIterationCommand = new RelayCommand(_ => PreviousIterationDoubleList());
                ConstructGraphCommand = new RelayCommand(_ => UpdateGraph());
            }
            else
            {
                throw new ArgumentException("Передан неверный тип данных");
            }
            
            UpdateGraph();
        }

        public GraphViewModel(
            List<(int, double)> valuesHistory,
            Function function,
            double parameterA,
            double parameterB,
            int methodCode)
        {
            _function = function;
            PointA = parameterA;
            PointB = parameterB;
            MethodCode = methodCode;
            ValuesHistoryIntDouble = valuesHistory;
            IterationsCount = valuesHistory.Count;
            CurrentIterationIndex = IterationsCount - 1;
            ShowNextIterationCommand = new RelayCommand(_ => NextIterationPolygons());
            ShowPreviousIterationCommand = new RelayCommand(_ => PreviousIterationPolygons());
            ConstructGraphCommand = new RelayCommand(_ => DrawPolygons());
            DrawPolygons();
        }

        private void NextIterationPolygons()
        {
            if (CurrentIterationIndex + 1 == IterationsCount)
            {
                CurrentIterationIndex = -1;
            }
            ++CurrentIterationIndex;
            DrawPolygons();
        }

        private void PreviousIterationPolygons()
        {
            if (CurrentIterationIndex == 0)
            {
                CurrentIterationIndex = IterationsCount;
            }
            --CurrentIterationIndex;
            DrawPolygons();
        }

        private void NextIterationDoubleList()
        {
            if (CurrentIterationIndex + 1 == IterationsCount)
            {
                CurrentIterationIndex = -1;
            }
            ++CurrentIterationIndex;
            TargetDotCoord = ValuesHistoryDoubleList[CurrentIterationIndex];
            UpdateGraph();
        }

        private void PreviousIterationDoubleList()
        {
            if (CurrentIterationIndex == 0)
            {
                CurrentIterationIndex = IterationsCount;
            }
            --CurrentIterationIndex;
            TargetDotCoord = ValuesHistoryDoubleList[CurrentIterationIndex];
            UpdateGraph();  
        }

        private void NextIterationTupleList()
        {
            if (CurrentIterationIndex + 1 == IterationsCount)
            {
                CurrentIterationIndex = -1;
            }
            ++CurrentIterationIndex;
            TargetDotCoord = ValuesHistoryTupleList[CurrentIterationIndex].Item3;
            AdditionalDotsCoord = new List<double>() {ValuesHistoryTupleList[CurrentIterationIndex].Item1, ValuesHistoryTupleList[CurrentIterationIndex].Item2};
            UpdateGraph();
        }

        private void PreviousIterationTupleList()
        {
            if (CurrentIterationIndex == 0)
            {
                CurrentIterationIndex = IterationsCount;
            }
            --CurrentIterationIndex;
            TargetDotCoord = ValuesHistoryTupleList[CurrentIterationIndex].Item3;
            AdditionalDotsCoord = new List<double>() {ValuesHistoryTupleList[CurrentIterationIndex].Item1, ValuesHistoryTupleList[CurrentIterationIndex].Item2};
            UpdateGraph();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}