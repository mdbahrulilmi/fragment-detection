using fragment_detection.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class ResultViewModel
{
    public ObservableCollection<CardViewModel> Cards { get; }

    public ResultViewModel(IEnumerable<CardViewModel> cards)
    {
        Cards = new ObservableCollection<CardViewModel>(cards);
    }
}