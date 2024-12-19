using System.Windows.Controls;
using ProgrammingThirdSem.Lsm.ViewModels;

namespace ProgrammingThirdSem.Lsm.Views
{
    public partial class LsmView : UserControl
    {
        public LsmView()
        {
            InitializeComponent();
            DataContext = new LsmViewModel();
        }
    }
}