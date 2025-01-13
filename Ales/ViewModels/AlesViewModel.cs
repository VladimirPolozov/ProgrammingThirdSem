using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProgrammingThirdSem.Ales.Models;
using ClosedXML.Excel;
using ProgrammingThirdSem.Ales.Views;

namespace ProgrammingThirdSem.Ales.ViewModels
{
    public sealed class AlesViewModel : INotifyPropertyChanged
    {
        private int _singsAfterCommaCount = 2;

        public int SingsAfterCommaCount
        {
            get => _singsAfterCommaCount;
            set
            {
                _singsAfterCommaCount = value;
                OnPropertyChanged(nameof(SingsAfterCommaCount));
            }
        
        }
        
        private string _gaussCalculationResult;
        private string _gaussJordanCalculationResult;
        private string _kramerCalculationResult;
        
        public string GaussCalculationResult
        {
            get => _gaussCalculationResult;
            set
            {
                _gaussCalculationResult = value;
                OnPropertyChanged(nameof(GaussCalculationResult));
            }
        }
        
        public string GaussJordanCalculationResult
        {
            get => _gaussJordanCalculationResult;
            set
            {
                _gaussJordanCalculationResult = value;
                OnPropertyChanged(nameof(GaussJordanCalculationResult));
            }
        }
        
        public string KramerCalculationResult
        {
            get => _kramerCalculationResult;
            set
            {
                _kramerCalculationResult = value;
                OnPropertyChanged(nameof(KramerCalculationResult));
            }
        }
        
        private DataGrid DataGridAles { get; set; }
        private DataView _dataView = new DataView();
        private DataTable _dataTable = new DataTable("Matrix");
        private DataTable _matrixDataTable = new DataTable();
        
        public DataView DataView
        {
            get => _dataView;
            set
            {
                _dataView = value;
                OnPropertyChanged(nameof(DataView));
            }
        }
        private int _lastColumnRow = 3;
        private const int ColumnWidth = 40;

        private bool _isGaussMethodChecked = true;
        private bool _isJordanGaussMethodChecked;
        private bool _isKramerMethodChecked;
        
        public bool IsGaussMethodChecked
        {
            get => _isGaussMethodChecked;
            set
            {
                _isGaussMethodChecked = value;
                OnPropertyChanged(nameof(IsGaussMethodChecked));
            }
        }

        public bool IsJordanGaussMethodChecked
        {
            get => _isJordanGaussMethodChecked;
            set
            {
                _isJordanGaussMethodChecked = value;
                OnPropertyChanged(nameof(IsJordanGaussMethodChecked));
            }
        }
        
        public bool IsKramerMethodChecked
        {
            get => _isKramerMethodChecked;
            set
            {
                _isKramerMethodChecked = value;
                OnPropertyChanged(nameof(IsKramerMethodChecked));
            }
        }

        public ICommand AddToTableCommand { get; }
        public ICommand RemoveFromTableCommand { get; }
        public ICommand LoadFromExcelCommand { get; }
        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand GenerateCommand { get; }

        public AlesViewModel(DataGrid dataGridAles)
        {
            DataGridAles = dataGridAles;
            
            InitTable();
            
            LoadFromExcelCommand = new RelayCommand(_ => LoadFromExcel());
            CalculateCommand = new RelayCommand(_ => Calculate());
            AddToTableCommand = new RelayCommand(AddToTable);
            RemoveFromTableCommand = new RelayCommand(RemoveFromTable);
            ClearCommand = new RelayCommand(Clear);
            GenerateCommand = new RelayCommand(Generate);
        }

        private void Generate(object obj)
        {
            try
            {

                var generate = new GenerateWindow();

                if (generate.ShowDialog() == true)
                {
                    var minValue = GenerateWindow.MinValue ?? -10;
                    var maxValue = GenerateWindow.MaxValue ?? 10;

                    var dataTable = new DataTable();

                    _matrixDataTable = DataView.Table;

                    dataTable.Columns.Add("x1", typeof(double));
                    for (var columnIndex = 2; columnIndex < _lastColumnRow + 1; ++columnIndex)
                    {
                        if (columnIndex == _lastColumnRow)
                        {
                            dataTable.Columns.Add("=", typeof(double));
                        }
                        else
                        {
                            dataTable.Columns.Add("x" + columnIndex.ToString(), typeof(double));
                        }

                        dataTable.Rows.Add();
                    }

                    var random = new Random();
                    // наполнение случайными данными
                    for (var row = 0; row < _matrixDataTable.Rows.Count; ++row)
                    {
                        for (var col = 0; col < _matrixDataTable.Columns.Count; ++col)
                        {
                            dataTable.DefaultView.Table.Rows[row][col] = random.Next(minValue, maxValue + 1);
                        }
                    }

                    DataView = dataTable.DefaultView;
                    _dataTable = dataTable;
                    UpdateDataGridWidth();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear(object obj)
        {
            for (var row = 0; row < DataView.Table.DefaultView.Table.Rows.Count; ++row)
            {
                for (var col = 0; col < DataView.Table.DefaultView.Table.Columns.Count; ++col)
                {
                    DataView.Table.DefaultView.Table.Rows[row][col] = DBNull.Value;
                }
            }
        }
        
        private void InitTable()
        {
            _dataTable.Columns.Add("x1", typeof(double));
            _dataTable.Columns.Add("x2", typeof(double));
            _dataTable.Columns.Add("=", typeof(double));

            _dataTable.Rows.Add();
            _dataTable.Rows.Add();

            DataView = _dataTable.DefaultView;
            UpdateDataGridWidth();
        }

        private void UpdateDataGridWidth()
        {
            var width = _lastColumnRow * ColumnWidth;
            DataGridAles.Width = width + 1;

            foreach (var column in DataGridAles.Columns)
            {
                column.Width = ColumnWidth;
            }
        }

        private void RemoveFromTable(object obj)
        {
            try
            {
                if (_lastColumnRow <= 3)
                {
                    throw new Exception("Достигнут минимальный размер матрицы");
                }

                var dataTable = new DataTable();

                _matrixDataTable = DataView.Table;

                --_lastColumnRow;

                dataTable.Columns.Add("x1", typeof(double));
                for (var columnIndex = 2; columnIndex < _lastColumnRow + 1; ++columnIndex)
                {
                    if (columnIndex == _lastColumnRow)
                    {
                        dataTable.Columns.Add("=", typeof(double));   
                    }
                    else
                    {
                        dataTable.Columns.Add("x" + columnIndex.ToString(), typeof(double));   
                    }
                    dataTable.Rows.Add();
                }

                // наполнение прошлыми данными
                for (var row = 0; row < dataTable.DefaultView.Table.Rows.Count; ++row)
                {
                    for (var col = 0; col < dataTable.DefaultView.Table.Columns.Count; ++col)
                    {
                        dataTable.DefaultView.Table.Rows[row][col] = _matrixDataTable.Rows[row][col];
                    }
                }

                DataView = dataTable.DefaultView;
                _dataTable = dataTable;
                UpdateDataGridWidth();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void AddToTable(object obj)
        {
            try
            {
                if (_lastColumnRow >= 49)
                {
                    throw new Exception("Достигнут максимальный размер матрицы");
                }

                var dataTable = new DataTable();

                _matrixDataTable = DataView.Table;

                ++_lastColumnRow;

                dataTable.Columns.Add("x1", typeof(double));
                for (var columnIndex = 2; columnIndex < _lastColumnRow + 1; ++columnIndex)
                {
                    if (columnIndex == _lastColumnRow)
                    {
                        dataTable.Columns.Add("=", typeof(double));   
                    }
                    else
                    {
                        dataTable.Columns.Add("x" + columnIndex.ToString(), typeof(double));   
                    }
                    dataTable.Rows.Add();
                }

                // наполнение прошлыми данными
                for (var row = 0; row < _matrixDataTable.Rows.Count; ++row)
                {
                    for (var col = 0; col < _matrixDataTable.Columns.Count; ++col)
                    {
                        dataTable.DefaultView.Table.Rows[row][col] = _matrixDataTable.Rows[row][col];
                    }
                }

                DataView = dataTable.DefaultView;
                _dataTable = dataTable;
                UpdateDataGridWidth();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Calculate()
        {
            GaussCalculationResult = "";
            GaussJordanCalculationResult = "";
            KramerCalculationResult = "";
            
            var rows = DataView.Table.DefaultView.Table.Rows.Count;
            var cols = DataView.Table.DefaultView.Table.Columns.Count;
            var coefficients = new double[rows, cols - 1];
            var constants = new double[rows];

            for (var row = 0; row < rows; ++row)
            {
                for (var col = 0; col < cols - 1; ++col)
                {
                    coefficients[row, col] = _dataTable.Rows[row][col] != DBNull.Value
                        ? (double)_dataTable.Rows[row][col]
                        : 0.0; 
                }
            }

            for (var row = 0; row < rows; ++row)
            {
                constants[row] = _dataTable.Rows[row][cols - 1] != DBNull.Value
                    ? (double)_dataTable.Rows[row][cols - 1]
                    : 0.0;
            }
            
            try
            {
                if (IsGaussMethodChecked)
                {
                    var resultGaussDoubles = AlesModel.GaussMethod.Solve(coefficients, constants);
                    GaussCalculationResult += "Метод Гаусса:\n";

                    int col;
                    for (col = 0; col < cols - 1; ++col)
                    {
                        if (col == cols - 2)
                        {
                            GaussCalculationResult += "x" + (col + 1) + " = " + RoundItem(
                                resultGaussDoubles[col], SingsAfterCommaCount).ToString("F3") + "\n";
                        }
                        else
                        {
                            GaussCalculationResult += "x" + (col + 1) + " = " + RoundItem(
                                resultGaussDoubles[col], SingsAfterCommaCount).ToString("F3") + ", " + "\n";
                        }
                    }
                }

                if (IsJordanGaussMethodChecked)
                {
                    var resultJordanGaussDoubles = AlesModel.JordanGaussMethod.Solve(coefficients, constants);
                    GaussJordanCalculationResult += "Метод Жордана-Гаусса:\n";

                    for (var col = 0; col < cols - 1; ++col)
                    {
                        if (col == cols - 2)
                        {
                            GaussJordanCalculationResult += "x" + (col + 1) + " = " + RoundItem(
                                resultJordanGaussDoubles[col], SingsAfterCommaCount).ToString("F3") + "\n";
                        }
                        else
                        {
                            GaussJordanCalculationResult += "x" + (col + 1) + " = " + RoundItem(
                                resultJordanGaussDoubles[col], SingsAfterCommaCount).ToString("F3") + ", " + "\n";
                        }
                    }
                }

                if (IsKramerMethodChecked)
                {
                    var resultKramerDoubles = AlesModel.KramerMethod.Solve(coefficients, constants);
                    KramerCalculationResult += "Метод Крамера:\n";

                    for (var col = 0; col < cols - 1; ++col)
                    {
                        if (col == cols - 2)
                        {
                            KramerCalculationResult += "x" + (col + 1) + " = " + RoundItem(
                                resultKramerDoubles[col], SingsAfterCommaCount).ToString("F3") + "\n";
                        }
                        else
                        {
                            KramerCalculationResult += "x" + (col + 1) + " = " + RoundItem(
                                resultKramerDoubles[col], SingsAfterCommaCount).ToString("F3") + ", " + "\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static double RoundItem(double item, int singsAfterCommaCount)
        {
            return Math.Round(item, singsAfterCommaCount, MidpointRounding.AwayFromZero);
        }

        private void LoadFromExcel()
        {
            var dataTable = new DataTable();
        
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "Выберите файл для загрузки",
                Multiselect = false
            };
        
            if (fileDialog.ShowDialog() == true)
            {
                var filePath = fileDialog.FileName;
        
                // Чтение данных из Excel файла
                using (var workbook = new XLWorkbook(filePath)) // Открываем книгу
                {
                    var workbookxls = new XLWorkbook(filePath);
                    var worksheet = workbookxls.Worksheet(1);
                    var range = worksheet.RangeUsed();
                    if (range != null)
                    {
                        _lastColumnRow = range.FirstColumn().CellCount() + 1;

                        for (var i = 0; i < range.FirstRow().CellCount(); ++i)
                        {
                            dataTable.Columns.Add($"x{i + 1}");
                        }

                        foreach (var row in range.RowsUsed())
                        {
                            var drA = dataTable.NewRow();

                            for (var i = 0; i < row.Cells().Count(); ++i)
                            {
                                drA[i] = row.Cell(i + 1).Value;
                            }

                            dataTable.Rows.Add(drA);
                        }
                    }

                    // Присваиваем DataView и DataTable (предполагается, что у вас есть соответствующие члены класса)
                    DataView = dataTable.DefaultView;
                    _dataTable = dataTable;
                }
            }
            
            UpdateDataGridWidth();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}