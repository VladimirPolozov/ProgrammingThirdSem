using System.Windows.Controls;
using ProgrammingThirdSem.NumericalMethods.ViewModels;

namespace ProgrammingThirdSem.NumericalMethods.Views
{
    public partial class NumericalMethodsView : UserControl
    {
        public NumericalMethodsView()
        {
            InitializeComponent();
            DataContext = new NumericalMethodsViewModel();
        }
    }
}