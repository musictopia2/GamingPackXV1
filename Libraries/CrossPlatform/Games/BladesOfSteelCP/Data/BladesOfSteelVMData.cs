using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BladesOfSteelCP.CustomPiles;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BladesOfSteelCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class BladesOfSteelVMData : ObservableObject, IBasicCardGamesData<RegularSimpleCard>
    {
        public BladesOfSteelVMData(CommandContainer command, BladesOfSteelGameContainer gameContainer)
        {
            Deck1 = new DeckObservablePile<RegularSimpleCard>(command);
            Pile1 = new SingleObservablePile<RegularSimpleCard>(command);
            PlayerHand1 = new HandObservable<RegularSimpleCard>(command);
            YourFaceOffCard = new SingleObservablePile<RegularSimpleCard>(command);
            YourFaceOffCard.IsEnabled = false;
            YourFaceOffCard.Text = "Your";
            OpponentFaceOffCard = new SingleObservablePile<RegularSimpleCard>(command);
            OpponentFaceOffCard.IsEnabled = false;
            OpponentFaceOffCard.Text = "Opponent";
            MainDefense1 = new MainDefenseCP(gameContainer);
            YourAttackPile = new PlayerAttackCP(command);
            YourDefensePile = new PlayerDefenseCP(command);
            OpponentAttackPile = new PlayerAttackCP(command);
            OpponentDefensePile = new PlayerDefenseCP(command);
        }
        public SingleObservablePile<RegularSimpleCard> YourFaceOffCard;
        public SingleObservablePile<RegularSimpleCard> OpponentFaceOffCard;
        public MainDefenseCP MainDefense1;
        public PlayerAttackCP YourAttackPile;
        public PlayerDefenseCP YourDefensePile;
        public PlayerAttackCP OpponentAttackPile;
        public PlayerDefenseCP OpponentDefensePile;
        public DeckObservablePile<RegularSimpleCard> Deck1 { get; set; }
        public SingleObservablePile<RegularSimpleCard> Pile1 { get; set; }
        public HandObservable<RegularSimpleCard> PlayerHand1 { get; set; }
        public SingleObservablePile<RegularSimpleCard>? OtherPile { get; set; }
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
        private string _otherPlayer = "";
        [VM]
        public string OtherPlayer
        {
            get { return _otherPlayer; }
            set
            {
                if (SetProperty(ref _otherPlayer, value))
                {
                    
                }
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