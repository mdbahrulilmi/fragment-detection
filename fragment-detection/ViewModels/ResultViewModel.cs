using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace fragment_detection.ViewModels
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ImageResultModel> Results { get; }

        private bool _isSingle;
        public bool IsSingle
        {
            get => _isSingle;
            set
            {
                if (_isSingle != value)
                {
                    _isSingle = value;
                    OnPropertyChanged(nameof(IsSingle));
                }
            }
        }

        private bool _isMultiple;
        public bool IsMultiple
        {
            get => _isMultiple;
            set
            {
                if (_isMultiple != value)
                {
                    _isMultiple = value;
                    OnPropertyChanged(nameof(IsMultiple));
                }
            }
        }

        public ResultViewModel()
        {
            Results = new ObservableCollection<ImageResultModel>();
            Results.CollectionChanged += Results_CollectionChanged;

            // Contoh data (bisa dihapus kalau pakai data dari luar)
            Results.Add(new ImageResultModel { Nama = "Test 1", Tanggal = "2025-05-10", Waktu = "12:00", JumlahFragmen = 5 });
            Results.Add(new ImageResultModel { Nama = "Test 2", Tanggal = "2025-05-11", Waktu = "13:00", JumlahFragmen = 3 });

            UpdateFlags();
        }

        private void Results_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateFlags();
        }

        private void UpdateFlags()
        {
            IsSingle = Results.Count == 1;
            IsMultiple = Results.Count > 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class ImageResultModel
    {
        public string Nama { get; set; }
        public string ImagePath { get; set; }
        public string Tanggal { get; set; }
        public string Waktu { get; set; }
        public int JumlahFragmen { get; set; }
    }
}
