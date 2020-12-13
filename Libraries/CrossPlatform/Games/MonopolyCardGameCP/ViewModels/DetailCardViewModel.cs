using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using MonopolyCardGameCP.Cards;
namespace MonopolyCardGameCP.ViewModels
{
    public class DetailCardViewModel : ObservableObject
    {
        public MonopolyCardGameCardInformation CurrentCard { get; set; }
        public void Clear()
        {
            if (CurrentCard.Deck == 0)
            {
                return;
            }
            CurrentCard = new MonopolyCardGameCardInformation();
            CurrentCard.IsUnknown = true;
        }
        public void AdditionalInfo(int deck)
        {
            if (deck == CurrentCard.Deck)
            {
                return;
            }
            CurrentCard = new MonopolyCardGameCardInformation();
            CurrentCard.Populate(deck);
        }
        public DetailCardViewModel()
        {
            CurrentCard = new MonopolyCardGameCardInformation();
            CurrentCard.IsUnknown = true;
        }
    }
}