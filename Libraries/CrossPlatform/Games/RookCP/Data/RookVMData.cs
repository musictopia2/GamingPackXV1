using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using RookCP.Cards;
using RookCP.Logic;
using System.Linq;
using System.Threading.Tasks;
namespace RookCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class RookVMData : ObservableObject, ITrickCardGamesData<RookCardInformation, EnumColorTypes>,
        ITrickDummyHand<EnumColorTypes, RookCardInformation>
    {
        private readonly RookGameContainer _gameContainer;
        public RookVMData(CommandContainer command,
            RookTrickAreaCP trickArea1,
            IGamePackageResolver resolver,
            RookGameContainer gameContainer
            )
        {
            Deck1 = new DeckObservablePile<RookCardInformation>(command);
            Pile1 = new SingleObservablePile<RookCardInformation>(command);
            PlayerHand1 = new HandObservable<RookCardInformation>(command);
            TrickArea1 = trickArea1;
            _gameContainer = gameContainer;
            Bid1 = new NumberPicker(command, resolver);
            Color1 = new SimpleEnumPickerVM<EnumColorTypes>(command, new ColorListChooser<EnumColorTypes>());
            Dummy1 = new DummyHandCP(command);
            Bid1.ChangedNumberValueAsync += Bid1_ChangedNumberValueAsync;
            Color1.AutoSelectCategory = EnumAutoSelectCategory.AutoEvent;
            Color1.ItemClickedAsync += Color1_ItemClickedAsync;
        }
        private Task Bid1_ChangedNumberValueAsync(int chosen)
        {
            BidChosen = chosen;
            return Task.CompletedTask;
        }
        private Task Color1_ItemClickedAsync(EnumColorTypes piece)
        {
            ColorChosen = piece;
            TrumpSuit = piece;
            return Task.CompletedTask;
        }
        public NumberPicker Bid1;
        public SimpleEnumPickerVM<EnumColorTypes> Color1;
        public DummyHandCP Dummy1;
        public RookTrickAreaCP TrickArea1 { get; set; }
        BasicTrickAreaObservable<EnumColorTypes, RookCardInformation> ITrickCardGamesData<RookCardInformation, EnumColorTypes>.TrickArea1
        {
            get => TrickArea1;
            set => TrickArea1 = (RookTrickAreaCP)value;
        }
        public DeckObservablePile<RookCardInformation> Deck1 { get; set; }
        public SingleObservablePile<RookCardInformation> Pile1 { get; set; }
        public HandObservable<RookCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<RookCardInformation>? OtherPile { get; set; }
        public EnumColorTypes ColorChosen { get; set; }
        public int BidChosen { get; set; } = -1;
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
        private EnumColorTypes _trumpSuit;
        [VM]
        public EnumColorTypes TrumpSuit
        {
            get { return _trumpSuit; }
            set
            {
                if (SetProperty(ref _trumpSuit, value)) { }
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
        public DeckRegularDict<RookCardInformation> GetCurrentHandList()
        {
            if (_gameContainer!.SaveRoot!.DummyPlay == true && _gameContainer.PlayerList.Count() == 2)
            {
                return Dummy1!.HandList;
            }
            return _gameContainer.SingleInfo!.MainHandList;
        }
        public int CardSelected()
        {
            if (_gameContainer!.SaveRoot!.DummyPlay == true && _gameContainer.PlayerList.Count() == 2)
            {
                return Dummy1!.ObjectSelected();
            }
            else if (_gameContainer.SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
            {
                throw new BasicBlankException("Only self can show card selected.  If I am wrong, rethink");
            }
            return PlayerHand1!.ObjectSelected();
        }
        public void RemoveCard(int deck)
        {
            if (_gameContainer!.SaveRoot!.DummyPlay == true && _gameContainer.PlayerList.Count() == 2)
            {
                Dummy1!.RemoveDummyCard(deck);
            }
            else
            {
                _gameContainer.SingleInfo!.MainHandList.RemoveObjectByDeck(deck); //because computer player does this too.
            }
        }
        internal bool CanPass { get; set; }
    }
}