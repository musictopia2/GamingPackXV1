using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using SnagCardGameCP.Cards;
using SnagCardGameCP.Logic;
namespace SnagCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class SnagCardGameVMData : ObservableObject, ITrickCardGamesData<SnagCardGameCardInformation, EnumSuitList>
    {
        public SnagCardGameVMData(CommandContainer command,
            SnagTrickObservable trickArea1,
            SnagCardGameGameContainer gameContainer
            )
        {
            Deck1 = new DeckObservablePile<SnagCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<SnagCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<SnagCardGameCardInformation>(command);
            TrickArea1 = trickArea1;
            Bar1 = new BarObservable(gameContainer);
            Human1 = new HandObservable<SnagCardGameCardInformation>(command);
            Opponent1 = new HandObservable<SnagCardGameCardInformation>(command);
            Bar1.Visible = true;
            Bar1.AutoSelect = EnumHandAutoType.SelectOneOnly;
            Human1.Text = "Your Cards Won";
            Opponent1.Text = "Opponent Cards Won";
            Human1.Visible = false;
            Opponent1.Visible = false;
        }
        BasicTrickAreaObservable<EnumSuitList, SnagCardGameCardInformation> ITrickCardGamesData<SnagCardGameCardInformation, EnumSuitList>.TrickArea1
        {
            get => TrickArea1;
            set => TrickArea1 = (SnagTrickObservable)value;
        }
        public BarObservable Bar1;
        public HandObservable<SnagCardGameCardInformation> Human1;
        public HandObservable<SnagCardGameCardInformation> Opponent1;
        public SnagTrickObservable TrickArea1 { get; set; }
        public DeckObservablePile<SnagCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<SnagCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<SnagCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<SnagCardGameCardInformation>? OtherPile { get; set; }
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
        private string _instructions = "";
        [VM]
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    
                }
            }
        }
    }
}