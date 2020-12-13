using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using SkuckCardGameCP.Cards;
using System.Threading.Tasks;
namespace SkuckCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class SkuckCardGameVMData : ObservableObject, ITrickCardGamesData<SkuckCardGameCardInformation, EnumSuitList>
        , ITrickDummyHand<EnumSuitList, SkuckCardGameCardInformation>
    {
        private readonly SkuckCardGameGameContainer _gameContainer;
        public SkuckCardGameVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumSuitList, SkuckCardGameCardInformation> trickArea1,
            IGamePackageResolver resolver,
            SkuckCardGameGameContainer gameContainer
            )
        {
            Deck1 = new DeckObservablePile<SkuckCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<SkuckCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<SkuckCardGameCardInformation>(command);
            TrickArea1 = trickArea1;
            _gameContainer = gameContainer;
            Bid1 = new NumberPicker(command, resolver);
            Suit1 = new SimpleEnumPickerVM<EnumSuitList>(command, new SuitListChooser());
            Suit1.AutoSelectCategory = EnumAutoSelectCategory.AutoSelect;
            Bid1.ChangedNumberValueAsync += Bid1_ChangedNumberValueAsync;
            Suit1.ItemSelectionChanged += Suit1_ItemSelectionChanged;
            Bid1.LoadNormalNumberRangeValues(1, 26);
        }
        public NumberPicker Bid1;
        public SimpleEnumPickerVM<EnumSuitList> Suit1;
        public BasicTrickAreaObservable<EnumSuitList, SkuckCardGameCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<SkuckCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<SkuckCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<SkuckCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<SkuckCardGameCardInformation>? OtherPile { get; set; }
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
        private EnumStatusList _gameStatus;
        [VM]
        public EnumStatusList GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {
                    
                }
            }
        }
        public int BidAmount { get; set; } = -1;
        DeckRegularDict<SkuckCardGameCardInformation> ITrickDummyHand<EnumSuitList, SkuckCardGameCardInformation>.GetCurrentHandList()
        {
            DeckRegularDict<SkuckCardGameCardInformation> output = _gameContainer!.SingleInfo!.MainHandList.ToRegularDeckDict();
            output.AddRange(_gameContainer.SingleInfo.TempHand!.ValidCardList);
            return output;
        }
        int ITrickDummyHand<EnumSuitList, SkuckCardGameCardInformation>.CardSelected()
        {
            if (_gameContainer!.SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
            {
                throw new BasicBlankException("Only self can get card selected.  If I am wrong, rethink");
            }
            int selects = PlayerHand1!.ObjectSelected();
            int others = _gameContainer.SingleInfo.TempHand!.CardSelected;
            if (selects != 0 && others != 0)
            {
                throw new BasicBlankException("You cannot choose from both hand and temps.  Rethink");
            }
            if (selects != 0)
            {
                return selects;
            }
            return others;
        }
        void ITrickDummyHand<EnumSuitList, SkuckCardGameCardInformation>.RemoveCard(int deck)
        {
            bool rets = _gameContainer!.SingleInfo!.MainHandList.ObjectExist(deck);
            if (rets == true)
            {
                _gameContainer.SingleInfo.MainHandList.RemoveObjectByDeck(deck);
                return;
            }
            var thisCard = _gameContainer.SingleInfo.TempHand!.CardList.GetSpecificItem(deck);
            if (thisCard.IsEnabled == false)
            {
                throw new BasicBlankException("Card was supposed to be disabled");
            }
            _gameContainer.SingleInfo.TempHand.HideCard(thisCard);
        }
        private Task Bid1_ChangedNumberValueAsync(int chosen)
        {
            BidAmount = chosen;
            return Task.CompletedTask;
        }
        private void Suit1_ItemSelectionChanged(EnumSuitList? piece)
        {
            if (piece.HasValue == false)
            {
                _gameContainer!.SaveRoot!.TrumpSuit = EnumSuitList.None;
            }
            else
            {
                _gameContainer!.SaveRoot!.TrumpSuit = piece!.Value;
            }
        }
    }
}