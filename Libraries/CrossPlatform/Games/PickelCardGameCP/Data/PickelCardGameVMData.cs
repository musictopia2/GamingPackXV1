using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using PickelCardGameCP.Cards;
namespace PickelCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class PickelCardGameVMData : ObservableObject, ITrickCardGamesData<PickelCardGameCardInformation, EnumSuitList>
    {
        public PickelCardGameVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumSuitList, PickelCardGameCardInformation> trickArea1,
            IGamePackageResolver resolver
            )
        {
            Deck1 = new DeckObservablePile<PickelCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<PickelCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<PickelCardGameCardInformation>(command);
            PlayerHand1.Text = "Your Cards";
            TrickArea1 = trickArea1;
            Bid1 = new NumberPicker(command, resolver);
            Suit1 = new SimpleEnumPickerVM<EnumSuitList>(command, new SuitListChooser());
            Suit1.AutoSelectCategory = EnumAutoSelectCategory.AutoSelect;
        }
        public NumberPicker Bid1;
        public SimpleEnumPickerVM<EnumSuitList> Suit1;
        public BasicTrickAreaObservable<EnumSuitList, PickelCardGameCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<PickelCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<PickelCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<PickelCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<PickelCardGameCardInformation>? OtherPile { get; set; }
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
        private int _bidAmount = -1;
        public int BidAmount
        {
            get { return _bidAmount; }
            set
            {
                if (SetProperty(ref _bidAmount, value))
                {
                    
                }
            }
        }
    }
}