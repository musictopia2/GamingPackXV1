using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.EventModels;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.ViewModels
{
    public class RestoreViewModel : BlazorScreenViewModel, IRestoreVM, IBlankGameVM
    {
        private readonly IEventAggregator _aggregator;
        public CommandContainer CommandContainer { get; set; }
        public RestoreViewModel(CommandContainer command, IEventAggregator aggregator)
        {
            CommandContainer = command;
            _aggregator = aggregator;
        }
        [Command(EnumCommandCategory.Old)] //try this one.  even if its not your turn, you can still restore.
        public Task RestoreAsync()
        {
            return _aggregator.PublishAsync(new RestoreEventModel());
        }
    }
}