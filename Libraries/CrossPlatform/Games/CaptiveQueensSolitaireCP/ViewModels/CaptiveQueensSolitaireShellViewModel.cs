using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
namespace CaptiveQueensSolitaireCP.ViewModels
{
    public class CaptiveQueensSolitaireShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => true; //most games allow new game always.
        public CaptiveQueensSolitaireShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<CaptiveQueensSolitaireMainViewModel>();
            return model;
        }
    }
}