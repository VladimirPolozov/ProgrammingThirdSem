using System.Windows.Controls;
using ProgrammingThirdSem.Sorting.ViewModels;

namespace ProgrammingThirdSem.Sorting.Views
{
    public partial class SortingView : UserControl
    {
        public SortingView()
        {
            InitializeComponent();
            DataContext = new SortingViewModel();
        }
    }
}