using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using ConnectTheDotsCP.Data;
using ConnectTheDotsCP.Logic;
namespace ConnectTheDotsCP.ViewModels
{
    [InstanceGame]
    public class ConnectTheDotsMainViewModel : SimpleBoardGameVM
    {
        public ConnectTheDotsMainViewModel(CommandContainer commandContainer,
            ConnectTheDotsMainGameClass mainGame,
            ConnectTheDotsVMData model,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, model, basicData, test, resolver)
        {
        }
    }
}