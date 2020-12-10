using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
namespace KlondikeSolitaireCP.ViewModels
{
    public class KlondikeSolitaireShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => true; //most games allow new game always.

        public KlondikeSolitaireShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<KlondikeSolitaireMainViewModel>();
            return model;
        }
    }
}
