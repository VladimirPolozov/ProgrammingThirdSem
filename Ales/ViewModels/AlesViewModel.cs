using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;

namespace ProgrammingThirdSem.Ales.ViewModels
{
    public class AlesViewModel : INotifyPropertyChanged
    {
        public class EquationRow
        {
            public double A { get; set; } // Коэффициенты A
            public double B { get; set; } // Вектор B
        }
        
        // Глобальные переменные для хранения данных
        public ObservableCollection<EquationRow> EquationRows { get; set; }
        public double[,] A; // Матрица коэффициентов
        public double[] B; // Вектор свободных членов
        public double[] X; // Вектор решений
        
        public ICommand LoadFromExcelCommand { get; }
        public ICommand CalculateCommand { get; }

        public AlesViewModel()
        {
            EquationRows = new ObservableCollection<EquationRow>();
            
            LoadFromExcelCommand = new RelayCommand(_ => LoadFromExcel());
            CalculateCommand = new RelayCommand(_ => Calculate());
        }

        private async void Calculate()
        {
            // Formulate the query string based on data from A and B
            var input = "";
            var n = A.GetLength(0); // Number of equations

            for (var i = 0; i < n; ++i)
            {
                input += string.Join(" + ", Enumerable.Range(0, n).Select((j) => $"{A[i, j]}x{j + 1}")); // Use x1, x2, x3 for variables
                input += $" = {-B[i]}";
                if (i < n - 1)
                    input += ", ";
            }

            var appId = "8A9G56-7RVGA48H8H"; // Replace with your App ID
            var url = $"https://api.wolframalpha.com/v2/query?input=solve+{Uri.EscapeDataString(input)}+for+x1,x2,x3&format=plaintext&output=JSON&appid={appId}";
            Console.WriteLine(url);

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Отправляем запрос
                    var response = await client.GetStringAsync(url);
                    var json = JObject.Parse(response);

                    // Проверка на наличие ошибок в ответе
                    if (json["queryresult"]["success"].Value<bool>() == false)
                    {
                        MessageBox.Show("Запрос не удался. Проверьте входные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Парсим ответ
                    var pods = json["queryresult"]["pods"];
                    string result = "";

                    foreach (var pod in pods)
                    {
                        result += pod["title"] + ": " + pod["subpods"][0]["plaintext"] + "\n";
                    }

                    // Показываем результат во всплывающем сообщении
                    MessageBox.Show(result, "Результат", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadFromExcel()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "Выберите файл Excel"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;

                // Загружаем данные из Excel
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Получаем первый лист
                    var rowCount = worksheet.Dimension.Rows; // Получаем количество строк

                    EquationRows.Clear(); // Очищаем предыдущие данные

                    // Инициализация матрицы A и вектора B
                    var n = rowCount - 1; // Предполагаем, что первая строка - заголовки
                    A = new double[n, n];
                    B = new double[n];

                    for (var row = 2; row <= rowCount; ++row) // Начинаем с 2, если первая строка - заголовки
                    {
                        for (var col = 1; col <= n; ++col) // Заполнение матрицы A
                        {
                            A[row - 2, col - 1] = worksheet.Cells[row, col].GetValue<double>();
                        }
                        B[row - 2] = worksheet.Cells[row, n + 1].GetValue<double>(); // Заполнение вектора B
                        EquationRows.Add(new EquationRow { A = A[row - 2, 0], B = B[row - 2] });
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}