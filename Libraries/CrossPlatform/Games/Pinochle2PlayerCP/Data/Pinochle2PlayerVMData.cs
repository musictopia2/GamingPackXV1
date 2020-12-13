using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Pinochle2PlayerCP.Cards;
namespace Pinochle2PlayerCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class Pinochle2PlayerVMData : ObservableObject, ITrickCardGamesData<Pinochle2PlayerCardInformation, EnumSuitList>
    {

        public Pinochle2PlayerVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumSuitList, Pinochle2PlayerCardInformation> trickArea1,
            IGamePackageResolver resolver
            )
        {
            Deck1 = new DeckObservablePile<Pinochle2PlayerCardInformation>(command);
            Pile1 = new SingleObservablePile<Pinochle2PlayerCardInformation>(command);
            PlayerHand1 = new HandObservable<Pinochle2PlayerCardInformation>(command);
            TrickArea1 = trickArea1;
            Guide1 = new ScoreGuideViewModel();
            YourMelds = new HandObservable<Pinochle2PlayerCardInformation>(command);
            OpponentMelds = new HandObservable<Pinochle2PlayerCardInformation>(command);
            TempSets = new TempSetsObservable<EnumSuitList, EnumRegularColorList, Pinochle2PlayerCardInformation>(command, resolver);
            TempSets.HowManySets = 2;
            Deck1.DrawInCenter = true;
            YourMelds.Text = "Yours";
            OpponentMelds.Text = "Opponents";
            YourMelds.IgnoreMaxRules = true;
            OpponentMelds.IgnoreMaxRules = true;
            YourMelds.AutoSelect = EnumHandAutoType.SelectOneOnly;
        }
        public HandObservable<Pinochle2PlayerCardInformation> YourMelds;
        public HandObservable<Pinochle2PlayerCardInformation> OpponentMelds;
        public TempSetsObservable<EnumSuitList, EnumRegularColorList, Pinochle2PlayerCardInformation> TempSets;
        public ScoreGuideViewModel Guide1;
        public BasicTrickAreaObservable<EnumSuitList, Pinochle2PlayerCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<Pinochle2PlayerCardInformation> Deck1 { get; set; }
        public SingleObservablePile<Pinochle2PlayerCardInformation> Pile1 { get; set; }
        public HandObservable<Pinochle2PlayerCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<Pinochle2PlayerCardInformation>? OtherPile { get; set; }
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
        private EnumSuitList _trumpSuit;
        [VM]
        public EnumSuitList TrumpSuit
        {
            get { return _trumpSuit; }
            set
            {
                if (SetProperty(ref _trumpSuit, value)) { }
            }
        }
    }
}