using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
namespace CalculationSolitaireCP.ViewModels
{
    public class CalculationSolitaireShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => true; //most games allow new game always.
        public CalculationSolitaireShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<CalculationSolitaireMainViewModel>();
            return model;
        }
    }
}