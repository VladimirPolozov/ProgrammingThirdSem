using System.Windows.Controls;
using ProgrammingThirdSem.Ales.ViewModels;

namespace ProgrammingThirdSem.ALES.Views
{
    public partial class AlesView : UserControl
    {
        public AlesView()
        {
            InitializeComponent();
            DataContext = new AlesViewModel();
        }
    }
}