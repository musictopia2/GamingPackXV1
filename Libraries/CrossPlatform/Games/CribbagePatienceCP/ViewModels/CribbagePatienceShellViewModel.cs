using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModels;
namespace CribbagePatienceCP.ViewModels
{
    public class CribbagePatienceShellViewModel : SinglePlayerShellViewModel
    {
        protected override bool AlwaysNewGame => false; //most games allow new game always.
        public CribbagePatienceShellViewModel(IGamePackageResolver mainContainer, CommandContainer container, IGameInfo GameData, ISaveSinglePlayerClass saves) : base(mainContainer, container, GameData, saves)
        {
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<CribbagePatienceMainViewModel>();
            return model;
        }
    }
}