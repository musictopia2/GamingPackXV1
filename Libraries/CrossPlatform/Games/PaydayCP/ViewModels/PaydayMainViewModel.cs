using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using PaydayCP.Cards;
using PaydayCP.Data;
using PaydayCP.Logic;
using System.Threading.Tasks;
namespace PaydayCP.ViewModels
{
    [InstanceGame]
    public class PaydayMainViewModel : SimpleBoardGameVM
    {
        private readonly PaydayMainGameClass _mainGame;
        private readonly PaydayVMData _model;
        private readonly IGamePackageResolver _resolver;
        private readonly IBuyProcesses _processes;
        public PaydayMainViewModel(CommandContainer commandContainer,
            PaydayMainGameClass mainGame,
            PaydayVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver,
            IBuyProcesses processes
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _model = model;
            _resolver = resolver;
            _processes = processes;
        }
        public DiceCup<SimpleDice> GetCup => _model.Cup!; 
        private bool _didInit;
        protected override async Task ActivateAsync()
        {
            await base.ActivateAsync();
            _model.DealPile.SendEnableProcesses(this, () => _mainGame.SaveRoot.GameStatus == EnumStatus.ChooseBuy);
            _model.CurrentDealList.ObjectClickedAsync += CurrentDealList_ObjectClickedAsync;
            LoadProperScreensAsync();
        }
        private async Task CurrentDealList_ObjectClickedAsync(DealCard payLoad, int index)
        {
            await _processes.BuyerSelectedAsync(payLoad.Deck);
        }
        private async void LoadProperScreensAsync()
        {
            switch (_mainGame.SaveRoot.GameStatus)
            {
                case EnumStatus.Starts:
                case EnumStatus.None:
                case EnumStatus.MakeMove:
                case EnumStatus.RollLottery:
                case EnumStatus.RollRadio:
                case EnumStatus.EndingTurn:
                case EnumStatus.RollCharity:
                    await LoadMainScreensAsync();
                    break;
                case EnumStatus.ChooseDeal:
                    await CloseRollerScreenAsync();
                    await CloseMailListScreenAsync();
                    await LoadDealPileScreenAsync();
                    await LoadChooseDealScreenAsync();
                    break;
                case EnumStatus.ChoosePlayer:
                    await CloseMailListScreenAsync();
                    await CloseRollerScreenAsync();
                    await LoadMailPileScreenAsync();
                    await LoadPlayerScreenAsync();
                    break;
                case EnumStatus.ChooseLottery:
                    await CloseMailListScreenAsync();
                    await CloseRollerScreenAsync();
                    await LoadLotteryScreenAsync();
                    break;
                case EnumStatus.ChooseBuy:
                    //leave the comments because not sure about other platforms (?)
                    await LoadMainScreensAsync(); //for now, does not know anything about xamarin forms
                    //may require rethinking (?)

                    //if (_mainGame.BasicData.IsXamarinForms)
                    //{
                    //    await CloseMailListScreenAsync();
                    //    await CloseDealOrBuyScreenAsync();
                    //    await LoadBuyDealScreenAsync();
                    //}
                    //else
                    //{
                    //    await LoadMainScreensAsync(); //desktop can show all.
                    //}
                    break;
                case EnumStatus.DealOrBuy:
                    await CloseRollerScreenAsync();
                    await LoadMailPileScreenAsync();
                    await CloseMailListScreenAsync();
                    await LoadDealOrBuyScreenAsync();
                    break;
                case EnumStatus.ViewMail:
                    await CloseRollerScreenAsync();
                    await LoadMailPileScreenAsync();
                    break;
                case EnumStatus.ViewYardSale:
                    await CloseRollerScreenAsync();
                    await LoadDealPileScreenAsync();
                    break;
                default:
                    break;
            }
            _didInit = true;
        }
        #region Screen Processes
        private async Task CloseBuyDealScreenAsync()
        {
            if (BuyDealScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(BuyDealScreen);
            BuyDealScreen = null;
        }
        private async Task CloseChooseDealScreenAsync()
        {
            if (ChooseDealScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(ChooseDealScreen);
            ChooseDealScreen = null;
        }
        private async Task CloseDealOrBuyScreenAsync()
        {
            if (DealOrBuyScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(DealOrBuyScreen);
            DealOrBuyScreen = null;
        }
        private async Task CloseDealPileScreenAsync()
        {
            if (DealPileScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(DealPileScreen);
            DealPileScreen = null;
        }
        private async Task CloseLotteryScreenAsync()
        {
            if (LotteryScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(LotteryScreen);
            LotteryScreen = null;
        }
        private async Task CloseMailPileScreenAsync()
        {
            if (MailPileScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(MailPileScreen);
            MailPileScreen = null;
        }
        private async Task ClosePlayerScreenAsync()
        {
            if (PlayerScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(PlayerScreen);
            PlayerScreen = null;
        }
        private async Task CloseRollerScreenAsync()
        {
            if (RollerScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(RollerScreen);
            RollerScreen = null;
        }
        private async Task CloseMailListScreenAsync()
        {
            if (MailListScreen == null)
            {
                return;
            }
            await CloseSpecificChildAsync(MailListScreen);
            MailListScreen = null;
        }
        public MailListViewModel? MailListScreen { get; set; }
        public BuyDealViewModel? BuyDealScreen { get; set; }
        public ChooseDealViewModel? ChooseDealScreen { get; set; }
        public DealOrBuyViewModel? DealOrBuyScreen { get; set; }
        public DealPileViewModel? DealPileScreen { get; set; }
        public LotteryViewModel? LotteryScreen { get; set; }
        public MailPileViewModel? MailPileScreen { get; set; }
        public PlayerPickerViewModel? PlayerScreen { get; set; }
        public RollerViewModel? RollerScreen { get; set; }
        private async Task LoadMailListScreenAsync()
        {
            if (MailListScreen != null)
            {
                return;
            }
            MailListScreen = _resolver.Resolve<MailListViewModel>();
            await LoadScreenAsync(MailListScreen);
        }
        //private async Task LoadBuyDealScreenAsync()
        //{
        //    if (BuyDealScreen != null)
        //    {
        //        return;
        //    }
        //    BuyDealScreen = _resolver.Resolve<BuyDealViewModel>();
        //    await LoadScreenAsync(BuyDealScreen);
        //}
        private async Task LoadChooseDealScreenAsync()
        {
            if (ChooseDealScreen != null)
            {
                return;
            }
            ChooseDealScreen = _resolver.Resolve<ChooseDealViewModel>();
            await LoadScreenAsync(ChooseDealScreen);
        }
        private async Task LoadDealOrBuyScreenAsync()
        {
            if (DealOrBuyScreen != null)
            {
                return;
            }
            DealOrBuyScreen = _resolver.Resolve<DealOrBuyViewModel>();
            await LoadScreenAsync(DealOrBuyScreen);
        }
        private async Task LoadDealPileScreenAsync()
        {
            if (DealPileScreen != null)
            {
                return;
            }
            DealPileScreen = _resolver.Resolve<DealPileViewModel>();
            await LoadScreenAsync(DealPileScreen);
        }
        private async Task LoadLotteryScreenAsync()
        {
            if (LotteryScreen != null)
            {
                return;
            }
            LotteryScreen = _resolver.Resolve<LotteryViewModel>();
            await LoadScreenAsync(LotteryScreen);
        }
        private async Task LoadMailPileScreenAsync()
        {
            if (MailPileScreen != null)
            {
                return;
            }
            MailPileScreen = _resolver.Resolve<MailPileViewModel>();
            await LoadScreenAsync(MailPileScreen);
        }
        private async Task LoadPlayerScreenAsync()
        {
            if (PlayerScreen != null)
            {
                return;
            }
            PlayerScreen = _resolver.Resolve<PlayerPickerViewModel>();
            await LoadScreenAsync(PlayerScreen);
        }
        private async Task LoadRollerScreenAsync()
        {
            if (RollerScreen != null)
            {
                return;
            }
            RollerScreen = _resolver.Resolve<RollerViewModel>();
            await LoadScreenAsync(RollerScreen);
        }
        private async Task LoadMainScreensAsync()
        {
            await CloseBuyDealScreenAsync();
            await CloseChooseDealScreenAsync();
            await CloseDealOrBuyScreenAsync();
            await CloseDealPileScreenAsync();
            await CloseLotteryScreenAsync();
            await CloseMailPileScreenAsync();
            await ClosePlayerScreenAsync();
            await LoadRollerScreenAsync();
            await LoadMailListScreenAsync();
        }
        #endregion
        private EnumStatus _gameStatus;
        [VM]
        public EnumStatus GameStatus
        {
            get { return _gameStatus; }
            set
            {
                if (SetProperty(ref _gameStatus, value))
                {
                    if (_didInit == false)
                    {
                        return;
                    }
                    LoadProperScreensAsync();
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
    }
}