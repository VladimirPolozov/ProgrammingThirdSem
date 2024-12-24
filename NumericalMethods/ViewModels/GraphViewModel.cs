using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
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
        
        public ICommand ConstructGraphCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand ShowNextIterationCommand { get; }
        public ICommand ShowPreviousIterationCommand { get; }
        
        public GraphViewModel(List<T> valuesHistory, Function function, Window window)
        {
            _function = function;
            _window = window;

            if (valuesHistory is List<(double, double, double)> tupleList)
            {
                ValuesHistoryTupleList = tupleList;
                IterationsCount = ValuesHistoryTupleList.Count;
                CurrentIterationIndex = IterationsCount - 1;
                TargetDotCoord = ValuesHistoryTupleList[CurrentIterationIndex].Item3;
                AdditionalDotsCoord = new List<double>() {ValuesHistoryTupleList.Last().Item1, ValuesHistoryTupleList.Last().Item2};
                
                ShowNextIterationCommand = new RelayCommand(_ => NextIterationTupleList());
                ShowPreviousIterationCommand = new RelayCommand(_ => PreviousIterationTupleList());
            } else if (valuesHistory is List<double> doubleList)
            {
                ValuesHistoryDoubleList = doubleList;
                IterationsCount = ValuesHistoryDoubleList.Count;
                CurrentIterationIndex = IterationsCount - 1;
                TargetDotCoord = ValuesHistoryDoubleList[CurrentIterationIndex];
                AdditionalDotsCoord = new List<double>();
                
                ShowNextIterationCommand = new RelayCommand(_ => NextIterationDoubleList());
                ShowPreviousIterationCommand = new RelayCommand(_ => PreviousIterationDoubleList());
            }
            else
            {
                throw new ArgumentException("Передан неверный тип данных");
            }
            
            UpdateGraph();
            
            ConstructGraphCommand = new RelayCommand(_ => UpdateGraph());
            CloseCommand = new RelayCommand(_ => CloseWindow());
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

        private void CloseWindow()
        {
            _window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}