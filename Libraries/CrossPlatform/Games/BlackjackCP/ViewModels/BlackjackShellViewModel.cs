using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
namespace BlackjackCP.ViewModels
{
    public class BlackjackShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => false; //most games allow new game always.
        public BlackjackShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<BlackjackMainViewModel>();
            return model;
        }
    }
}