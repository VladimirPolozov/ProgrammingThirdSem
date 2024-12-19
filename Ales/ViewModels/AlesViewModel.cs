using System;
using System.ComponentModel;
using System.Windows.Input;

namespace ProgrammingThirdSem.Ales.ViewModels
{
    public sealed class AlesViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}