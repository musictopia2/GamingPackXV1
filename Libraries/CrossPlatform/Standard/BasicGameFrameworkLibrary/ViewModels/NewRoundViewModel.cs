using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.ViewModels
{
    public class NewRoundViewModel : BlazorScreenViewModel, INewRoundVM, IBlankGameVM
    {
        private readonly IEventAggregator _aggregator;
        private readonly BasicData _basicData;
        public CommandContainer CommandContainer { get; set; }

        public NewRoundViewModel(CommandContainer command, IEventAggregator aggregator, BasicData basicData)
        {
            CommandContainer = command;
            _aggregator = aggregator;
            _basicData = basicData;
        }
        public bool CanStartNewRound
        {
            get
            {
                if (_basicData.MultiPlayer == false)
                {
                    return true;
                }
                return _basicData.Client == false;
            }
        }


        [Command(EnumCommandCategory.Old)]
        public Task StartNewRoundAsync()
        {
            return _aggregator.PublishAsync(new NewRoundEventModel()); //this does not care what happens with the new round.
        }
    }
}