using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BuncoDiceGameCP.EventModels;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BuncoDiceGameCP.ViewModels
{
    [InstanceGame]
    public class BuncoNewRoundViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        private readonly IEventAggregator _aggregator;

        public BuncoNewRoundViewModel(CommandContainer commandContainer, IEventAggregator aggregator)
        {
            CommandContainer = commandContainer;
            _aggregator = aggregator;
            commandContainer.ManuelFinish = false;
            commandContainer.IsExecuting = false;
        }
        public CommandContainer CommandContainer { get; set; }
        [Command(EnumCommandCategory.Plain)]
        public async Task NewRoundAsync()
        {
            await _aggregator.PublishAsync(new ChoseNewRoundEventModel());
        }
    }
}
