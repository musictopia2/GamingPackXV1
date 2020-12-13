using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using LottoDominosCP.Logic;
namespace LottoDominosCP.ViewModels
{
    [InstanceGame]
    public class MainBoardViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public MainBoardViewModel(CommandContainer commandContainer, LottoDominosMainGameClass mainGame)
        {
            if (mainGame.SaveRoot.GameStatus != Data.EnumStatus.NormalPlay)
            {
                throw new BasicBlankException("Can't load the board view model when the status is not even normal play.  Rethink");
            }
            CommandContainer = commandContainer;
        }
        public CommandContainer CommandContainer { get; set; }
    }
}