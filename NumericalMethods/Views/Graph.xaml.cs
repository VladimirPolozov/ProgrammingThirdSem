using System.Windows;
using ProgrammingThirdSem.NumericalMethods.ViewModels;

namespace ProgrammingThirdSem.NumericalMethods.Views
{
    public partial class Graph : Window
    {
        public Graph()
        {
            InitializeComponent();
            DataContext = new GraphViewModel();
        }
    }
}