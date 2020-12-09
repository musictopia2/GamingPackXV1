using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
namespace XPuzzleCP.ViewModels
{
    public class XPuzzleShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => true;
        public XPuzzleShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            
            var model = MainContainer.Resolve<XPuzzleMainViewModel>();
            return model;
        }
    }
}