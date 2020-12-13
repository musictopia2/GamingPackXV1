using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.Exceptions;
using FluxxCP.Containers;
using FluxxCP.Data;
using System.Threading.Tasks;
namespace FluxxCP.ViewModels
{
    public class FluxxShellViewModel : BasicMultiplayerShellViewModel<FluxxPlayerItem>
    {
        private readonly FluxxDelegates _delegates;

        public FluxxShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test,
            FluxxDelegates delegates
            )
            : base(mainContainer, container, gameData, basicData, save, test)
        {
            delegates.LoadMainScreenAsync = LoadMainScreenAsync;
            delegates.LoadProperActionScreenAsync = LoadActionScreenAsync;
            delegates.LoadKeeperScreenAsync = LoadKeeperScreenAsync;
            delegates.CurrentScreen = GetCurrentScreenCategory;
            _delegates = delegates;
        }
        private async Task LoadMainScreenAsync()
        {
            await StartNewGameAsync();
            RefreshEnables();
        }
        private EnumActionScreen GetCurrentScreenCategory()
        {
            if (MainVM != null)
            {
                return EnumActionScreen.None;
            }
            if (KeeperScreen != null)
            {
                return EnumActionScreen.KeeperScreen;
            }
            if (ActionScreen != null)
            {
                return EnumActionScreen.ActionScreen;
            }
            throw new BasicBlankException("Cannot find the screen used.  Rethink");
        }
        protected override async Task StartNewGameAsync()
        {
            await CloseActionScreenAsync();
            await CloseKeeperScreenAsync();
            await base.StartNewGameAsync();
        }
        private async Task LoadActionScreenAsync(ActionContainer actionContainer)
        {
            if (ActionScreen != null)
            {
                throw new BasicBlankException("Previous action was not loaded.  Rethink");
            }
            await CloseMainAsync();
            await CloseKeeperScreenAsync();
            switch (actionContainer.ActionCategory)
            {
                case EnumActionCategory.None:
                    break;
                case EnumActionCategory.Rules:
                    break;
                case EnumActionCategory.Directions:
                    break;
                case EnumActionCategory.DoAgain:
                    break;
                case EnumActionCategory.TradeHands:
                    break;
                case EnumActionCategory.UseTake:
                    break;
                case EnumActionCategory.Everybody1:
                    break;
                case EnumActionCategory.DrawUse:
                    break;
                case EnumActionCategory.FirstRandom:
                    break;
                default:
                    break;
            }
            ActionScreen = actionContainer.ActionCategory switch
            {
                EnumActionCategory.Rules => MainContainer.Resolve<ActionDiscardRulesViewModel>(),
                EnumActionCategory.Directions => MainContainer.Resolve<ActionDirectionViewModel>(),
                EnumActionCategory.DoAgain => MainContainer.Resolve<ActionDoAgainViewModel>(),
                EnumActionCategory.DrawUse => MainContainer.Resolve<ActionDrawUseViewModel>(),
                EnumActionCategory.Everybody1 => MainContainer.Resolve<ActionEverybodyGetsOneViewModel>(),
                EnumActionCategory.FirstRandom => MainContainer.Resolve<ActionFirstCardRandomViewModel>(),
                EnumActionCategory.TradeHands => MainContainer.Resolve<ActionTradeHandsViewModel>(),
                EnumActionCategory.UseTake => MainContainer.Resolve<ActionTakeUseViewModel>(),
                _ => throw new BasicBlankException("Cannot figure out action screen.  Rethink")
            };
            await LoadScreenAsync(ActionScreen);
            RefreshEnables();
        }
        private void RefreshEnables()
        {
            if (_delegates.RefreshEnables == null)
            {
                throw new BasicBlankException("Nobody is refreshing enables.  Rethink");
            }
            _delegates.RefreshEnables.Invoke();
        }
        private async Task LoadKeeperScreenAsync(KeeperContainer keeperContainer)
        {
            if (KeeperScreen != null)
            {
                return;
            }
            await CloseActionScreenAsync();
            await CloseMainAsync();
            KeeperScreen = keeperContainer.Section switch
            {
                EnumKeeperSection.None => MainContainer.Resolve<KeeperShowViewModel>(),
                EnumKeeperSection.Trash => MainContainer.Resolve<KeeperTrashViewModel>(),
                EnumKeeperSection.Steal => MainContainer.Resolve<KeeperStealViewModel>(),
                EnumKeeperSection.Exchange => MainContainer.Resolve<KeeperExchangeViewModel>(),
                _ => throw new BasicBlankException("Not Supported"),
            };
            await LoadScreenAsync(KeeperScreen);
        }
        private async Task CloseMainAsync()
        {
            if (MainVM != null)
            {
                await CloseSpecificChildAsync(MainVM);
                MainVM = null;
            }
        }
        private async Task CloseActionScreenAsync()
        {
            if (ActionScreen != null)
            {
                await CloseSpecificChildAsync(ActionScreen);
                ActionScreen = null;
            }
        }
        private async Task CloseKeeperScreenAsync()
        {
            if (KeeperScreen != null)
            {
                await CloseSpecificChildAsync(KeeperScreen);
                KeeperScreen = null;
            }
        }
        public BasicActionScreen? ActionScreen { get; set; }
        public BasicKeeperScreen? KeeperScreen { get; set; }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<FluxxMainViewModel>();
            return model;
        }
    }
}