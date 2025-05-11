using System;
using System.ComponentModel;
using System.Windows.Input;

namespace fragment_detection.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {
        private int _jumlahFragmen;

        public int JumlahFragmen
        {
            get => _jumlahFragmen;
            set
            {
                if (_jumlahFragmen != value)
                {
                    _jumlahFragmen = value;
                    OnPropertyChanged(nameof(JumlahFragmen));
                }
            }
        }

        public ICommand IncrementCommand { get; }
        public ICommand DecrementCommand { get; }

        public CardViewModel()
        {
            JumlahFragmen = 265; // Nilai awal
            IncrementCommand = new RelayCommand(Increment);
            DecrementCommand = new RelayCommand(Decrement);
        }

        private void Increment()
        {
            JumlahFragmen++;
        }

        private void Decrement()
        {
            if (JumlahFragmen > 0)
                JumlahFragmen--;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _execute();
    }
}
