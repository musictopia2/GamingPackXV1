using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using HitTheDeckCP.Cards;
namespace HitTheDeckCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class HitTheDeckVMData : ObservableObject, IBasicCardGamesData<HitTheDeckCardInformation>
    {
        public HitTheDeckVMData(CommandContainer command)
        {
            Deck1 = new DeckObservablePile<HitTheDeckCardInformation>(command);
            Pile1 = new SingleObservablePile<HitTheDeckCardInformation>(command);
            PlayerHand1 = new HandObservable<HitTheDeckCardInformation>(command);
        }
        public DeckObservablePile<HitTheDeckCardInformation> Deck1 { get; set; }
        public SingleObservablePile<HitTheDeckCardInformation> Pile1 { get; set; }
        public HandObservable<HitTheDeckCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<HitTheDeckCardInformation>? OtherPile { get; set; }
        private string _normalTurn = "";
        [VM]
        public string NormalTurn
        {
            get { return _normalTurn; }
            set
            {
                if (SetProperty(ref _normalTurn, value))
                {
                    
                }
            }
        }

        private string _status = "";
        [VM] //use this tag to transfer to the actual view model.  this is being done to avoid overflow errors.
        public string Status
        {
            get { return _status; }
            set
            {
                if (SetProperty(ref _status, value))
                {
                    
                }
            }
        }
        private string _nextPlayer = "";
        [VM]
        public string NextPlayer
        {
            get { return _nextPlayer; }
            set
            {
                if (SetProperty(ref _nextPlayer, value))
                {
                    
                }
            }
        }
    }
}