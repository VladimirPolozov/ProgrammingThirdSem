using System.Windows.Controls;
using DotNetEnv;
using ProgrammingThirdSem.NumericalMethods.ViewModels;

namespace ProgrammingThirdSem.NumericalMethods.Views
{
    public partial class NumericalMethodsView : UserControl
    {
        public NumericalMethodsView()
        {
            Env.Load();
            InitializeComponent();
            DataContext = new NumericalMethodsViewModel();
        }
    }
}