using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using RageCardGameCP.Cards;
using RageCardGameCP.Logic;
using System.Linq;
using System.Threading.Tasks;
namespace RageCardGameCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class RageCardGameVMData : ObservableObject, ITrickCardGamesData<RageCardGameCardInformation, EnumColor>
    {
        private readonly RageCardGameGameContainer _gameContainer;
        public RageCardGameVMData(CommandContainer command,
            SpecificTrickAreaObservable trickArea1,
            IGamePackageResolver resolver,
            RageCardGameGameContainer gameContainer
            )
        {
            Deck1 = new DeckObservablePile<RageCardGameCardInformation>(command);
            Pile1 = new SingleObservablePile<RageCardGameCardInformation>(command);
            PlayerHand1 = new HandObservable<RageCardGameCardInformation>(command);
            PlayerHand1.Maximum = 11;
            TrickArea1 = trickArea1;
            _gameContainer = gameContainer;
            Color1 = new SimpleEnumPickerVM<EnumColor>(command, new ColorListChooser<EnumColor>());
            Color1.AutoSelectCategory = EnumAutoSelectCategory.AutoEvent;
            Color1.ItemClickedAsync += Color1_ItemClickedAsync;
            Bid1 = new NumberPicker(command, resolver);
            Bid1.ChangedNumberValueAsync += Bid1_ChangedNumberValueAsync;
        }
        private async Task Color1_ItemClickedAsync(EnumColor piece)
        {
            if (piece == _gameContainer!.SaveRoot!.TrumpSuit && _gameContainer!.SaveRoot!.TrickList.Last().SpecialType == EnumSpecialType.Change)
            {
                ToastPlatform.ShowError($"{piece} is already current trump.  Choose a different one");
                return;
            }
            ColorChosen = piece;
            if (_gameContainer.ColorChosenAsync == null)
            {
                throw new BasicBlankException("Nobody is handling the color chosen.  Rethink");
            }
            await _gameContainer!.ColorChosenAsync!.Invoke();
        }
        private Task Bid1_ChangedNumberValueAsync(int chosen)
        {
            BidAmount = chosen;
            return Task.CompletedTask;
        }
        BasicTrickAreaObservable<EnumColor, RageCardGameCardInformation> ITrickCardGamesData<RageCardGameCardInformation, EnumColor>.TrickArea1
        {
            get => TrickArea1;
            set => TrickArea1 = (SpecificTrickAreaObservable)value;
        }
        public SimpleEnumPickerVM<EnumColor> Color1;
        public NumberPicker Bid1;
        public int BidAmount { get; set; } = -1;
        public SpecificTrickAreaObservable TrickArea1 { get; set; }
        public DeckObservablePile<RageCardGameCardInformation> Deck1 { get; set; }
        public SingleObservablePile<RageCardGameCardInformation> Pile1 { get; set; }
        public HandObservable<RageCardGameCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<RageCardGameCardInformation>? OtherPile { get; set; }
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
        private EnumColor _trumpSuit;
        [VM]
        public EnumColor TrumpSuit
        {
            get { return _trumpSuit; }
            set
            {
                if (SetProperty(ref _trumpSuit, value)) { }
            }
        }
        private string _lead = "";
        [VM]
        public string Lead
        {
            get { return _lead; }
            set
            {
                if (SetProperty(ref _lead, value))
                {
                    
                }
            }
        }
        public EnumColor ColorChosen { get; set; }
    }
}