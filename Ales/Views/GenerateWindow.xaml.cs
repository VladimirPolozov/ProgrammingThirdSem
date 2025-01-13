using System.Windows;

namespace ProgrammingThirdSem.Ales.Views
{
    public partial class GenerateWindow : Window
    {
        public static int? MinValue { get; private set; }
        public static int? MaxValue { get; private set; }
        
        public GenerateWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(MaxValueTextBox.Text, out var maxValue) && int.TryParse(MinValueTextBox.Text, out var minValue))
            {
                MinValue = minValue;
                MaxValue = maxValue;
                DialogResult = true; // Устанавливаем результат, чтобы закрыть окно
            }
            else
            {
                MessageBox.Show("Введите корректное положительное число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}