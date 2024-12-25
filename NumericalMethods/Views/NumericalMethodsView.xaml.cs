using DotNetEnv;
using ProgrammingThirdSem.NumericalMethods.ViewModels;

namespace ProgrammingThirdSem.NumericalMethods.Views
{
    public partial class NumericalMethodsView
    {
        public NumericalMethodsView()
        {
            Env.Load();
            InitializeComponent();
            DataContext = new NumericalMethodsViewModel();
        }
    }
}