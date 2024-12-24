using System.Collections.Generic;
using org.mariuszgromada.math.mxparser;
using ProgrammingThirdSem.NumericalMethods.ViewModels;

namespace ProgrammingThirdSem.NumericalMethods.Views
{
    public partial class Graph
    {
        public Graph(List<double> valuesHistory, Function function)
        {
            InitializeComponent();
            DataContext = new GraphViewModel<double>(valuesHistory, function, this);
        }
        public Graph(List<(double, double, double)> valuesHistory, Function function)
        {
            InitializeComponent();
            DataContext = new GraphViewModel<(double, double, double)>(valuesHistory, function, this);
        }
    }
}