using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.TestUtilities;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels
{
    public abstract class SimpleBoardGameVM : BasicMultiplayerMainVM
    {
        public SimpleBoardGameVM(
            CommandContainer commandContainer,
            IEndTurn mainGame,
            ISimpleBoardGamesData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver) : base(commandContainer,
                mainGame,
                viewModel,
                basicData,
                test,
                resolver)
        {

        }
        private string _instructions = "";
        [VM]
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    //can decide what to do when property changes
                }

            }
        }
    }
}