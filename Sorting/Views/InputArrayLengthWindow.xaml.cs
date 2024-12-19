using System.Windows;

namespace ProgrammingThirdSem.Sorting.Views
{
    public partial class InputArrayLengthWindow : Window
    {
        public int? ArrayLength { get; private set; }
        public int? MinValue { get; private set; }
        public int? MaxValue { get; private set; }
        
        public InputArrayLengthWindow()
        {
            InitializeComponent();
        }
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ArrayLengthTextBox.Text, out int length) && int.TryParse(MinValueTextBox.Text, out int minValue) && int.TryParse(MaxValueTextBox.Text, out int maxValue) && length > 0)
            {
                ArrayLength = length;
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