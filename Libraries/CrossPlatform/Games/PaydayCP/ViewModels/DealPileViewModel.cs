using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
namespace PaydayCP.ViewModels
{
    [InstanceGame]
    public class DealPileViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public DealPileViewModel(CommandContainer commandContainer)
        {
            CommandContainer = commandContainer;
        }
        public CommandContainer CommandContainer { get; set; }
    }
}