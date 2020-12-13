using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
namespace FourSuitRummyCP.ViewModels
{
    [InstanceGame]
    public class PlayerSetsViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public PlayerSetsViewModel(CommandContainer commandContainer)
        {
            CommandContainer = commandContainer;
        }
        public CommandContainer CommandContainer { get; set; }
    }
}