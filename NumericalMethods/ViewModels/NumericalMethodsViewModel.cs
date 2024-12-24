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
        private List<double> _newtonNullNullValuesHistory;
        private List<double> _newtonExtremeValuesHistory;
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
        
        public List<double> NewtonNullValuesHistory
        {
            get => _newtonNullNullValuesHistory;
            set
            {
                _newtonNullNullValuesHistory = value;
                OnPropertyChanged(nameof(NewtonNullValuesHistory));
            }
        }
        
        public List<double> NewtonExtremeValuesHistory
        {
            get => _newtonExtremeValuesHistory;
            set
            {
                _newtonExtremeValuesHistory = value;
                OnPropertyChanged(nameof(NewtonExtremeValuesHistory));
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

        private string _functionExpressionString;
        private Function _functionExpression;
        private double _parameterA;
        private double _parameterB;
        private double _epsilon;
        private int _singsAfterCommaCount;

        public string FunctionExpressionString
        {
            get => _functionExpressionString;
            set
            {
                _functionExpressionString = value.ToLower();
                OnPropertyChanged(nameof(FunctionExpressionString));
            }
        }
        
        public Function FunctionExpression
        {
            get => _functionExpression;
            set
            {
                _functionExpression = value;
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
            
            try
            {
                FunctionExpression = ConvertStringToFunc(FunctionExpressionString);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            if (IsDichotomyMethodChecked)
            {
                DichotomyValuesHistory = NumericalMethodsModel.DichotomyMethod(FunctionExpression, ParameterA, ParameterB, Epsilon);
                DichotomyMethodResult = RoundItem(DichotomyValuesHistory.Last().Item3, SingsAfterCommaCount).ToString();
            }
            
            if (IsGoldenRatioMethodChecked)
            {
                GoldenRatioMinValuesHistory = NumericalMethodsModel.GoldenRatioMinMethod(FunctionExpression, ParameterA, ParameterB, Epsilon);
                GoldenRatioMaxValuesHistory = NumericalMethodsModel.GoldenRatioMaxMethod(FunctionExpression, ParameterA, ParameterB, Epsilon);
                GoldenRatioMethodResult = $"min: {RoundItem(GoldenRatioMinValuesHistory.Last().Item3, SingsAfterCommaCount)}; " +
                                          $"max: {RoundItem(GoldenRatioMaxValuesHistory.Last().Item3, SingsAfterCommaCount)}";
            }

            if (IsNewtonMethodChecked)
            {
                NewtonNullValuesHistory = NumericalMethodsModel.NewtonNullMethod(FunctionExpression, ParameterB, Epsilon);
                NewtonExtremeValuesHistory = NumericalMethodsModel.NewtonExtremeMethod(FunctionExpression, ParameterB, Epsilon);
                NewtonMethodResult = $"null: {RoundItem(NewtonNullValuesHistory.Last(), SingsAfterCommaCount).ToString()}; " +
                                     $"extreme: {RoundItem(NewtonExtremeValuesHistory.Last(), SingsAfterCommaCount).ToString()}";
            }
        }

        private static double RoundItem(double item, int singsAfterCommaCount)
        {
            return Math.Round(item, singsAfterCommaCount, MidpointRounding.AwayFromZero);
        }

        private static Function ConvertStringToFunc(string functionExpression)
        {
            return new Function("f(x) = " + functionExpression);
        }

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
        public ICommand NewtonNullShowGraphCommand { get; }
        public ICommand NewtonExtremeShowGraphCommand { get; }

        public NumericalMethodsViewModel()
        {
            // вставляем в форму данные по умолчанию
            SetDefaultData();

            CalculateCommand = new RelayCommand(_ => Calculate());
            
            DichotomyShowGraphCommand = new RelayCommand(_ => DichotomyShowGraph());
            GoldenRatioMinShowGraphCommand = new RelayCommand(_ => GoldenRatioMinShowGraph());
            GoldenRatioMaxShowGraphCommand = new RelayCommand(_ => GoldenRatioMaxShowGraph());
            NewtonNullShowGraphCommand = new RelayCommand(_ => NewtonNullShowGraph());
            NewtonExtremeShowGraphCommand = new RelayCommand(_ => NewtonExtremeShowGraph());
        }

        private void NewtonExtremeShowGraph()
        {
            if (NewtonExtremeValuesHistory.Count == 0)
            {
                Calculate();
            }
            var showGraphic = new Graph(NewtonExtremeValuesHistory, FunctionExpression);
            showGraphic.ShowDialog();
        }

        private void NewtonNullShowGraph()
        {
            if (NewtonNullValuesHistory.Count == 0)
            {
                Calculate();
            }
            var showGraphic = new Graph(NewtonNullValuesHistory, FunctionExpression);
            showGraphic.ShowDialog();
        }

        private void GoldenRatioMaxShowGraph()
        {
            if (GoldenRatioMaxValuesHistory.Count == 0)
            {
                Calculate();
            }
            var showGraphic = new Graph(GoldenRatioMaxValuesHistory, FunctionExpression);
            showGraphic.ShowDialog();
        }

        private void GoldenRatioMinShowGraph()
        {
            if (GoldenRatioMinValuesHistory.Count == 0)
            {
                Calculate();
            }
            var showGraphic = new Graph(GoldenRatioMinValuesHistory, FunctionExpression);
            showGraphic.ShowDialog();
        }

        private void DichotomyShowGraph()
        {
            if (DichotomyValuesHistory.Count == 0)
            {
                Calculate();
            }
            var showGraphic = new Graph(DichotomyValuesHistory, FunctionExpression);
            showGraphic.ShowDialog();
        }

        private void SetDefaultData()
        {
            FunctionExpressionString = "x";
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