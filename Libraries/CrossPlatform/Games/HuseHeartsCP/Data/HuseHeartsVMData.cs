using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using HuseHeartsCP.Cards;
using HuseHeartsCP.Logic;
namespace HuseHeartsCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class HuseHeartsVMData : ObservableObject, ITrickCardGamesData<HuseHeartsCardInformation, EnumSuitList>,
        ITrickDummyHand<EnumSuitList, HuseHeartsCardInformation>
    {
        private readonly HuseHeartsGameContainer _gameContainer;
        public HuseHeartsVMData(CommandContainer command,
            HuseHeartsTrickAreaCP trickArea1,
            HuseHeartsGameContainer gameContainer
            )
        {
            Deck1 = new DeckObservablePile<HuseHeartsCardInformation>(command);
            Pile1 = new SingleObservablePile<HuseHeartsCardInformation>(command);
            PlayerHand1 = new HandObservable<HuseHeartsCardInformation>(command);
            TrickArea1 = trickArea1;
            _gameContainer = gameContainer;
            Dummy1 = new HandObservable<HuseHeartsCardInformation>(command);
            Blind1 = new HandObservable<HuseHeartsCardInformation>(command);
            Blind1.Maximum = 4;
            Blind1.Text = "Blind";
            Dummy1.Text = "Dummy Hand";
            Dummy1.AutoSelect = EnumHandAutoType.SelectOneOnly;

        }
        public HandObservable<HuseHeartsCardInformation> Dummy1;
        public HandObservable<HuseHeartsCardInformation> Blind1;
        public HuseHeartsTrickAreaCP TrickArea1 { get; set; }
        BasicTrickAreaObservable<EnumSuitList, HuseHeartsCardInformation> ITrickCardGamesData<HuseHeartsCardInformation, EnumSuitList>.TrickArea1
        {
            get => TrickArea1;
            set => TrickArea1 = (HuseHeartsTrickAreaCP)value;
        }
        public DeckObservablePile<HuseHeartsCardInformation> Deck1 { get; set; }
        public SingleObservablePile<HuseHeartsCardInformation> Pile1 { get; set; }
        public HandObservable<HuseHeartsCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<HuseHeartsCardInformation>? OtherPile { get; set; }
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
        private int _roundNumber;

        [VM]
        public int RoundNumber
        {
            get { return _roundNumber; }
            set
            {
                if (SetProperty(ref _roundNumber, value))
                {
                    
                }
            }
        }
        private EnumStatus _gameStatus;
        [VM]
        public EnumStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {
                    
                }
            }
        }
        public DeckRegularDict<HuseHeartsCardInformation> GetCurrentHandList()
        {
            if (TrickArea1!.FromDummy == true)
            {
                return Dummy1!.HandList;
            }
            else
            {
                return _gameContainer!.SingleInfo!.MainHandList;
            }
        }
        public int CardSelected()
        {
            if (TrickArea1!.FromDummy == true)
            {
                return Dummy1!.ObjectSelected();
            }
            else if (_gameContainer!.SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
            {
                throw new BasicBlankException("Only self can show card selected.  If I am wrong, rethink");
            }
            return PlayerHand1!.ObjectSelected();
        }
        public void RemoveCard(int deck)
        {
            if (TrickArea1!.FromDummy == true)
            {
                Dummy1!.HandList.RemoveObjectByDeck(deck);
            }
            else
            {
                _gameContainer.SingleInfo!.MainHandList.RemoveObjectByDeck(deck);
            }
        }
    }
}