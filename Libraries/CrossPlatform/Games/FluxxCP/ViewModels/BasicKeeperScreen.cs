using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using FluxxCP.Containers;
namespace FluxxCP.ViewModels
{
    public abstract class BasicKeeperScreen : BlazorScreenViewModel, IBlankGameVM
    {
        public BasicKeeperScreen(FluxxGameContainer gameContainer, KeeperContainer keeperContainer)
        {
            GameContainer = gameContainer;
            KeeperContainer = keeperContainer;
            CommandContainer = gameContainer.Command;
        }
        public CommandContainer CommandContainer { get; set; }
        protected FluxxGameContainer GameContainer { get; }
        protected KeeperContainer KeeperContainer { get; }
    }
}