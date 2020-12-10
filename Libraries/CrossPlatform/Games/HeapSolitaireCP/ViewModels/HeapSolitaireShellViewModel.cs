using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
namespace HeapSolitaireCP.ViewModels
{
    public class HeapSolitaireShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => true; //most games allow new game always.

        public HeapSolitaireShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<HeapSolitaireMainViewModel>();
            return model;
        }
    }
}