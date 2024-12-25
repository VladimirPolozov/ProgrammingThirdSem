using ProgrammingThirdSem.Ales.ViewModels;

namespace ProgrammingThirdSem.Ales.Views
{
    public partial class AlesView
    {
        public AlesView()
        {
            InitializeComponent();
            DataContext = new AlesViewModel();
        }
    }
}