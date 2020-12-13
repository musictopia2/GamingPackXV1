using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using YahtzeeHandsDownCP.Data;
namespace YahtzeeHandsDownCP.ViewModels
{
    public class YahtzeeHandsDownShellViewModel : BasicMultiplayerShellViewModel<YahtzeeHandsDownPlayerItem>
    {
        public YahtzeeHandsDownShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test)
            : base(mainContainer, container, gameData, basicData, save, test)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<YahtzeeHandsDownMainViewModel>();
            return model;
        }
    }
}