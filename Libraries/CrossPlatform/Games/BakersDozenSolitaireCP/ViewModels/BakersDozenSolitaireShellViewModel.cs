using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
namespace BakersDozenSolitaireCP.ViewModels
{
    public class BakersDozenSolitaireShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => true; //most games allow new game always.
        public BakersDozenSolitaireShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<BakersDozenSolitaireMainViewModel>();
            return model;
        }
    }
}