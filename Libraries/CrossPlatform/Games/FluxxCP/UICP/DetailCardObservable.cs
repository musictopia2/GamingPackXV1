using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using FluxxCP.Cards;
using FluxxCP.Data;
namespace FluxxCP.UICP
{
    public class DetailCardObservable : ObservableObject
    {
        public FluxxCardInformation CurrentCard { get; set; } 
        public void ResetCard()
        {
            CurrentCard = new FluxxCardInformation();
            CurrentCard.IsUnknown = true;
        }
        public void ShowCard<F>(F thisCard)
            where F : FluxxCardInformation, new()
        {
            if (thisCard.Deck == CurrentCard.Deck)
            {
                return;
            }
            CurrentCard = FluxxDetailClass.GetNewCard(thisCard.Deck);
            CurrentCard.Populate(thisCard.Deck);
        }
        public DetailCardObservable()
        {
            CurrentCard = new FluxxCardInformation();
            CurrentCard.IsUnknown = true;
        }
    }
}