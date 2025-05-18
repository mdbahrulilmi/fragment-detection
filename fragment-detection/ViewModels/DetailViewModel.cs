using System.Collections.ObjectModel;
using System.Linq;

namespace fragment_detection.ViewModels
{
    public class DetailViewModel
    {
        public ObservableCollection<CardViewModel> Cards { get; set; }
        public CardViewModel SelectedCard { get; set; }

        public DetailViewModel()
        {
            var originalCards = CardService.Instance.Cards;

            var indexedCards = originalCards.Select((card, i) =>
            {
                card.Index = i + 1;
                return card;
            });

            Cards = new ObservableCollection<CardViewModel>(indexedCards);
        }
    }
}
