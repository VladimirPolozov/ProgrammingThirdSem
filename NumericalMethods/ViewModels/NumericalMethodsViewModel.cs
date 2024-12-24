using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using org.mariuszgromada.math.mxparser;
using ProgrammingThirdSem.NumericalMethods.Models;
using ProgrammingThirdSem.NumericalMethods.Views;
using License = org.mariuszgromada.math.mxparser.License;

namespace ProgrammingThirdSem.NumericalMethods.ViewModels
{
    public sealed class NumericalMethodsViewModel : INotifyPropertyChanged
    {
        // чек-боксы выбора методов
        private bool _isDichotomyMethodChecked;
        private bool _isGoldenRatioMethodChecked;
        private bool _isNewtonMethodChecked;
        private bool _isCoordinateDescentMethodChecked;
        private bool _isRectangleMethodChecked;
        private bool _isTrapezoidMethodChecked;
        private bool _isParabolaMethodChecked;
        
        public bool IsDichotomyMethodChecked
        {
            get => _isDichotomyMethodChecked;
            set
            {
                _isDichotomyMethodChecked = value;
                OnPropertyChanged(nameof(IsDichotomyMethodChecked));
            }
        }

        public bool IsGoldenRatioMethodChecked
        {
            get => _isGoldenRatioMethodChecked;
            set
            {
                _isGoldenRatioMethodChecked = value;
                OnPropertyChanged(nameof(IsGoldenRatioMethodChecked));
            }
        }

        public bool IsNewtonMethodChecked
        {
            get => _isNewtonMethodChecked;
            set
            {
                _isNewtonMethodChecked = value;
                OnPropertyChanged(nameof(IsNewtonMethodChecked));
            }
        }
        
        public bool IsCoordinateDescentMethodChecked
        {
            get => _isCoordinateDescentMethodChecked;
            set
            {
                _isCoordinateDescentMethodChecked = value;
                OnPropertyChanged(nameof(IsCoordinateDescentMethodChecked));
            }
        }

        public bool IsRectangleMethodChecked
        {
            get => _isRectangleMethodChecked;
            set
            {
                _isRectangleMethodChecked = value;
                OnPropertyChanged(nameof(IsRectangleMethodChecked));
            }
        }
        
        public bool IsTrapezoidMethodChecked
        {
            get => _isTrapezoidMethodChecked;
            set
            {
                _isTrapezoidMethodChecked = value;
                OnPropertyChanged(nameof(IsTrapezoidMethodChecked));
            }
        }

        public bool IsParabolaMethodChecked
        {
            get => _isParabolaMethodChecked;
            set
            {
                _isParabolaMethodChecked = value;
                OnPropertyChanged(nameof(IsParabolaMethodChecked));
            }
        }
        
        // результат выполения методов
        private string _dichotomyMethodResult;
        private string _goldenRatioMethodResult;
        private string _newtonMethodResult;
        private string _coordinateDescentMethodResult;
        private string _rectangleMethodResult;
        private string _trapezoidMethodResult;
        private string _parabolaMethodResult;
        
        public string DichotomyMethodResult
        {
            get => _dichotomyMethodResult;
            set
            {
                _dichotomyMethodResult = value;
                OnPropertyChanged(nameof(DichotomyMethodResult));
            }
        }

        public string GoldenRatioMethodResult
        {
            get => _goldenRatioMethodResult;
            set
            {
                _goldenRatioMethodResult = value;
                OnPropertyChanged(nameof(GoldenRatioMethodResult));
            }
        }
        
        public string NewtonMethodResult
        {
            get => _newtonMethodResult;
            set
            {
                _newtonMethodResult = value;
                OnPropertyChanged(nameof(NewtonMethodResult));
            }
        }
        
        public string CoordinateDescentMethodResult
        {
            get => _coordinateDescentMethodResult;
            set
            {
                _coordinateDescentMethodResult = value;
                OnPropertyChanged(nameof(CoordinateDescentMethodResult));
            }
        }

        public string RectangleMethodResult
        {
            get => _rectangleMethodResult;
            set
            {
                _rectangleMethodResult = value;
                OnPropertyChanged(nameof(RectangleMethodResult));
            }
        }

        public string TrapezoidMethodResult
        {
            get => _trapezoidMethodResult;
            set
            {
                _trapezoidMethodResult = value;
                OnPropertyChanged(nameof(TrapezoidMethodResult));
            }
        }
        
        public string ParabolaMethodResult
        {
            get => _parabolaMethodResult;
            set
            {
                _parabolaMethodResult = value;
                OnPropertyChanged(nameof(ParabolaMethodResult));
            }
        }
        
        // история вычислений
        private List<(double, double, double)> _dichotomyValuesHistory;
        private List<(double, double, double)> _goldenRatioMaxValuesHistory;
        private List<(double, double, double)> _goldenRatioMinValuesHistory;
        private List<(double, double, double)> _newtonValuesHistory;
        private List<(double, double, double)> _coordinateDescentValuesHistory;
        private List<(double, double, double)> _rectangleValuesHistory;
        private List<(double, double, double)> _trapezoidValuesHistory;
        private List<(double, double, double)> _parabolaValuesHistory;
        
        public List<(double, double, double)> DichotomyValuesHistory
        {
            get => _dichotomyValuesHistory;
            set
            {
                _dichotomyValuesHistory = value;
                OnPropertyChanged(nameof(DichotomyValuesHistory));
            }
        }

        public List<(double, double, double)> GoldenRatioMaxValuesHistory
        {
            get => _goldenRatioMaxValuesHistory;
            set
            {
                _goldenRatioMaxValuesHistory = value;
                OnPropertyChanged(nameof(GoldenRatioMaxValuesHistory));
            }
        }
        
        public List<(double, double, double)> GoldenRatioMinValuesHistory
        {
            get => _goldenRatioMinValuesHistory;
            set
            {
                _goldenRatioMinValuesHistory = value;
                OnPropertyChanged(nameof(GoldenRatioMinValuesHistory));
            }
        }
        
        public List<(double, double, double)> NewtonValuesHistory
        {
            get => _newtonValuesHistory;
            set
            {
                _newtonValuesHistory = value;
                OnPropertyChanged(nameof(NewtonValuesHistory));
            }
        }
        
        public List<(double, double, double)> CoordinateDescentValuesHistory
        {
            get => _coordinateDescentValuesHistory;
            set
            {
                _coordinateDescentValuesHistory = value;
                OnPropertyChanged(nameof(CoordinateDescentValuesHistory));
            }
        }

        public List<(double, double, double)> RectangleValuesHistory
        {
            get => _rectangleValuesHistory;
            set
            {
                _rectangleValuesHistory = value;
                OnPropertyChanged(nameof(RectangleValuesHistory));
            }
        }

        public List<(double, double, double)> TrapezoidValuesHistory
        {
            get => _trapezoidValuesHistory;
            set
            {
                _trapezoidValuesHistory = value;
                OnPropertyChanged(nameof(TrapezoidValuesHistory));
            }
        }
        
        public List<(double, double, double)> ParabolaValuesHistory
        {
            get => _parabolaValuesHistory;
            set
            {
                _parabolaValuesHistory = value;
                OnPropertyChanged(nameof(ParabolaValuesHistory));
            }
        }

        private string _functionExpression;
        private double _parameterA;
        private double _parameterB;
        private double _epsilon;
        private int _singsAfterCommaCount;

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

        public int SingsAfterCommaCount
        {
            get => _singsAfterCommaCount;
            set
            {
                _singsAfterCommaCount = value;
                OnPropertyChanged(nameof(SingsAfterCommaCount));
            }
        }

        private void Calculate()
        {
            ClearData();

            if (IsDichotomyMethodChecked)
            {
                ExecuteCalculation(
                    NumericalMethodsModel.DichotomyMethod,
                    FunctionExpression,
                    ParameterA,
                    ParameterB,
                    Epsilon,
                    out var result
                );

                DichotomyValuesHistory = result;
                DichotomyMethodResult = RoundItem(DichotomyValuesHistory.Last().Item3, SingsAfterCommaCount).ToString();
            }

            if (IsGoldenRatioMethodChecked)
            {
                ExecuteCalculation(
                    NumericalMethodsModel.GoldenRatioMinMethod,
                    FunctionExpression,
                    ParameterA,
                    ParameterB,
                    Epsilon,
                    out var result
                );
                GoldenRatioMinValuesHistory = result;
                
                ExecuteCalculation(
                    NumericalMethodsModel.GoldenRatioMaxMethod,
                    FunctionExpression,
                    ParameterA,
                    ParameterB,
                    Epsilon,
                    out result
                    );
                GoldenRatioMaxValuesHistory = result;
                
                var minResult = RoundItem(GoldenRatioMinValuesHistory.Last().Item3, SingsAfterCommaCount);
                var maxResult = RoundItem(GoldenRatioMaxValuesHistory.Last().Item3, SingsAfterCommaCount);
                GoldenRatioMethodResult = $"min: {minResult}; max: {maxResult}";
            }
        }

        private static double RoundItem(double item3, int singsAfterCommaCount)
        {
            return Math.Round(item3, singsAfterCommaCount, MidpointRounding.AwayFromZero);
        }

        private static void ExecuteCalculation(
            CalculationMethod calculationMethod,
            string functionExpression,
            double parameterA,
            double parameterB,
            double epsilon,
            out List<(double, double, double)> result
            )
        {
            try
            {
                var function = ConvertStringToFunc(functionExpression);
                result = calculationMethod(function, parameterA, parameterB, epsilon);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private static Function ConvertStringToFunc(string functionExpression)
        {
            return new Function("f(x) = " + functionExpression);
        }

        private delegate List<(double, double, double)> CalculationMethod(Function function, double parameterA, double parameterB, double epsilon);

        private void ClearData()
        {
            DichotomyMethodResult = "";
            GoldenRatioMethodResult = "";
            NewtonMethodResult = "";
            CoordinateDescentMethodResult = "";
            RectangleMethodResult = "";
            TrapezoidMethodResult = "";
            ParabolaMethodResult = "";
        }
        
        public ICommand CalculateCommand { get; }
        
        // Показать график
        public ICommand DichotomyShowGraphCommand { get; }
        public ICommand GoldenRatioMinShowGraphCommand { get; }
        public ICommand GoldenRatioMaxShowGraphCommand { get; }

        public NumericalMethodsViewModel()
        {
            // вставляем в форму данные по умолчанию
            SetDefaultData();

            CalculateCommand = new RelayCommand(_ => Calculate());
            
            DichotomyShowGraphCommand = new RelayCommand(_ => DichotomyShowGraph());
            GoldenRatioMinShowGraphCommand = new RelayCommand(_ => GoldenRatioMinShowGraph());
            GoldenRatioMaxShowGraphCommand = new RelayCommand(_ => GoldenRatioMaxShowGraph());
        }

        private void GoldenRatioMaxShowGraph()
        {
            var showGraphic = new Graph(GoldenRatioMaxValuesHistory, ConvertStringToFunc(FunctionExpression));
            showGraphic.ShowDialog();
        }

        private void GoldenRatioMinShowGraph()
        {
            var showGraphic = new Graph(GoldenRatioMinValuesHistory, ConvertStringToFunc(FunctionExpression));
            showGraphic.ShowDialog();
        }

        private void DichotomyShowGraph()
        {
            var showGraphic = new Graph(DichotomyValuesHistory, ConvertStringToFunc(FunctionExpression));
            showGraphic.ShowDialog();
        }

        private void SetDefaultData()
        {
            FunctionExpression = "x";
            ParameterA = -50;
            ParameterB = 50;
            Epsilon = 0.01;
            SingsAfterCommaCount = 2;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}