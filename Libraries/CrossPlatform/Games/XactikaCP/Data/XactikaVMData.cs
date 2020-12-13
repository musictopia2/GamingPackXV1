using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Threading.Tasks;
using XactikaCP.Cards;
using XactikaCP.Logic;
using static BasicGameFrameworkLibrary.ChooserClasses.ListViewPicker;
namespace XactikaCP.Data
{
    [SingletonGame]
    [AutoReset]
    public class XactikaVMData : ObservableObject, ITrickCardGamesData<XactikaCardInformation, EnumShapes>, IBasicEnableProcess
    {
        private readonly XactikaGameContainer _gameContainer;
        public XactikaVMData(CommandContainer command,
            BasicTrickAreaObservable<EnumShapes, XactikaCardInformation> trickArea1,
            XactikaGameContainer gameContainer
            )
        {
            Deck1 = new DeckObservablePile<XactikaCardInformation>(command);
            Pile1 = new SingleObservablePile<XactikaCardInformation>(command);
            PlayerHand1 = new HandObservable<XactikaCardInformation>(command);
            TrickArea1 = trickArea1;
            _gameContainer = gameContainer;
            ModeChoose1 = new ListViewPicker(command, gameContainer.Resolver);
            ShapeChoose1 = new ChooseShapeObservable(_gameContainer);
            Bid1 = new NumberPicker(command, gameContainer.Resolver);
            ModeChoose1.ItemSelectedAsync += ModeChooser1_ItemSelectedAsync;
            Bid1.ChangedNumberValueAsync += Bid1_ChangedNumberValueAsync;
            PlayerHand1!.Maximum = 8;
            ModeChoose1.IndexMethod = EnumIndexMethod.OneBased;
            CustomBasicList<string> tempList = new CustomBasicList<string> { "To Win", "To Lose", "Bid" };
            ModeChoose1.LoadTextList(tempList);
            ShapeChoose1.SendEnableProcesses(this, (() => _gameContainer.SaveRoot.GameStatus == EnumStatusList.CallShape));
        }
        private Task ModeChooser1_ItemSelectedAsync(int selectedIndex, string selectedText)
        {
            _gameContainer!.SaveRoot!.GameMode = (EnumGameMode)selectedIndex;
            return Task.CompletedTask;
        }
        private Task Bid1_ChangedNumberValueAsync(int chosen)
        {
            BidChosen = chosen;
            return Task.CompletedTask;
        }
        bool IBasicEnableProcess.CanEnableBasics()
        {
            return true;
        }

        public ChooseShapeObservable ShapeChoose1;
        public NumberPicker Bid1;
        public int BidChosen { get; set; } = -1;
        public ListViewPicker ModeChoose1;
        public BasicTrickAreaObservable<EnumShapes, XactikaCardInformation> TrickArea1 { get; set; }
        public DeckObservablePile<XactikaCardInformation> Deck1 { get; set; }
        public SingleObservablePile<XactikaCardInformation> Pile1 { get; set; }
        public HandObservable<XactikaCardInformation> PlayerHand1 { get; set; }
        public SingleObservablePile<XactikaCardInformation>? OtherPile { get; set; }
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
        private EnumShapes _trumpSuit;
        [VM]
        public EnumShapes TrumpSuit
        {
            get { return _trumpSuit; }
            set
            {
                if (SetProperty(ref _trumpSuit, value)) { }
            }
        }
        private string _gameModeText = "";
        [VM]
        public string GameModeText
        {
            get { return _gameModeText; }
            set
            {
                if (SetProperty(ref _gameModeText, value))
                {
                    
                }
            }
        }
        private EnumShapes _shapeChosen;
        public EnumShapes ShapeChosen
        {
            get { return _shapeChosen; }
            set
            {
                if (SetProperty(ref _shapeChosen, value))
                {
                    
                }
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
        public EnumGameMode ModeChosen { get; set; } 
    }
}