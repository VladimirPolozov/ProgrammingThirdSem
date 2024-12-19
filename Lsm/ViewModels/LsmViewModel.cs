using System;
using System.ComponentModel;
using System.Windows.Input;

namespace ProgrammingThirdSem.Lsm.ViewModels
{
    public sealed class LsmViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}