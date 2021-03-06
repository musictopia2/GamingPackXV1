using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CaliforniaJackCP.Data;
namespace CaliforniaJackCP.ViewModels
{
    public class CaliforniaJackShellViewModel : BasicTrickShellViewModel<CaliforniaJackPlayerItem>
    {
        public CaliforniaJackShellViewModel(IGamePackageResolver mainContainer,
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
            var model = MainContainer.Resolve<CaliforniaJackMainViewModel>();
            return model;
        }
    }
}