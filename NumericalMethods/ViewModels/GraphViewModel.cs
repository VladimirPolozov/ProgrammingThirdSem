using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using org.mariuszgromada.math.mxparser;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using ProgrammingThirdSem.NumericalMethods.Models;

namespace ProgrammingThirdSem.NumericalMethods.ViewModels
{
    public class GraphViewModel : INotifyPropertyChanged
    {
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
                OnPropertyChanged(nameof(IterationsCount));
            }
        }
        
        public int CurrentIterationIndex
        {
            get => _currentIterationIndex;
            set
            {
                _currentIterationIndex = value;
                OnPropertyChanged(nameof(CurrentIterationIndex));
            }
        }
        
        public string IterationsInfo => $"Итерация №{CurrentIterationIndex} из {IterationsCount}";

        private void ConstructGraph(List<(double, double, double)> valuesHistory, Function function)
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
                var y = NumericalMethodsModel.SolveFunc(function, x);
                series.Points.Add(new DataPoint(x, y));
            }

            // Рисуем вертикальную линию
            for (var y = yAxis.Minimum; y <= yAxis.Maximum; y += 1)
            {
                // mark.Points.Add(new DataPoint(markCoordX, y));
            }

            PlotModel.Series.Add(series);
            PlotModel.Series.Add(mark);
            PlotModel.InvalidatePlot(true);
        }
        
        public ICommand ConstructGraphCommand { get; }
        
        public GraphViewModel(List<(double, double, double)> valuesHistory, Function function)
        {
            ConstructGraphCommand = new RelayCommand(_ => ConstructGraph(valuesHistory, function));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}