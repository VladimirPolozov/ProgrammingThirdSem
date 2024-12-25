using System;
using System.ComponentModel;
using System.Windows.Input;

namespace ProgrammingThirdSem.Ales.ViewModels
{
    public sealed class AlesViewModel : INotifyPropertyChanged
    {
        public ICommand LoadFromExcelCommand { get; }
        public ICommand CalculateCommand { get; }

        public AlesViewModel()
        {
            LoadFromExcelCommand = new RelayCommand(_ => LoadFromExcel());
            CalculateCommand = new RelayCommand(_ => Calculate());
        }

        private void Calculate()
        {
            throw new NotImplementedException();
        }

        private void LoadFromExcel()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}