using System.Collections.ObjectModel;
using fragment_detection.ViewModels;

public class CardService
{
    private static CardService? _instance;
    public static CardService Instance => _instance ??= new CardService();

    public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

    private CardService()
    {
        // Bisa tambahkan inisialisasi di sini jika perlu
    }
}
