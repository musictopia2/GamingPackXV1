using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using PaydayCP.Cards;
using System.Linq;
using System.Threading.Tasks;
namespace PaydayCP.Data
{
    [SingletonGame]
    //[AutoReset]
    public class PaydayVMData : ObservableObject, IDiceBoardGamesData
    {
        public string PopUpChosen { get; set; } = "";
        private readonly CommandContainer _command;
        private readonly IGamePackageResolver _resolver;
        public PaydayVMData(CommandContainer command, IGamePackageResolver resolver)
        {
            _command = command;
            _resolver = resolver;
            PopUpList = new ListViewPicker(command, resolver);
            PopUpList.IndexMethod = ListViewPicker.EnumIndexMethod.OneBased;
            PopUpList.ItemSelectedAsync += PopUpList_ItemSelectedAsync;
            CurrentDealList = new HandObservable<DealCard>(command);
            CurrentDealList.AutoSelect = EnumHandAutoType.None;
            CurrentDealList.Text = "Deal List";
            CurrentMailList = new HandObservable<MailCard>(command);
            CurrentMailList.Text = "Mail List";
            DealPile = new SingleObservablePile<DealCard>(command);
            DealPile.CurrentOnly = true;
            DealPile.Text = "Deal Pile";
            MailPile = new SingleObservablePile<MailCard>(command);
            MailPile.CurrentOnly = true;
            MailPile.Text = "Mail Pile";
        }
        private Task PopUpList_ItemSelectedAsync(int selectedIndex, string selectedText)
        {
            PopUpChosen = selectedText;
            return Task.CompletedTask;
        }
        public SingleObservablePile<DealCard> DealPile;
        public SingleObservablePile<MailCard> MailPile;
        public HandObservable<DealCard> CurrentDealList;
        public HandObservable<MailCard> CurrentMailList;
        public ListViewPicker PopUpList;
        internal void AddPopupLists(CustomBasicList<string> list)
        {
            PopUpChosen = "";
            PopUpList.LoadTextList(list);
            PopUpList.UnselectAll();
            if (list.Count == 1)
            {
                PopUpChosen = list.Single();
                PopUpList.SelectSpecificItem(1);
            }
        }
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
        private string _monthLabel = "";
        [VM]
        public string MonthLabel
        {
            get
            {
                return _monthLabel;
            }
            set
            {
                if (SetProperty(ref _monthLabel, value) == true)
                {
                }
            }
        }
        private string _otherLabel = "";
        [VM]
        public string OtherLabel
        {
            get
            {
                return _otherLabel;
            }
            set
            {
                if (SetProperty(ref _otherLabel, value) == true)
                {
                }
            }
        }
        public DiceCup<SimpleDice>? Cup { get; set; }
        public void LoadCup(ISavedDiceList<SimpleDice> saveRoot, bool autoResume)
        {
            if (Cup != null && autoResume)
            {
                return;
            }
            Cup = new DiceCup<SimpleDice>(saveRoot.DiceList, _resolver, _command);
            if (autoResume == true)
            {
                Cup.CanShowDice = true;
            }
            Cup.HowManyDice = 1;
            Cup.Visible = true;
        }


    }
}