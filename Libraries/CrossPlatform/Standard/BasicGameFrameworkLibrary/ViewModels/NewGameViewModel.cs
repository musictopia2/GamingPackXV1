using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.ViewModels
{
    public sealed class NewGameViewModel : BlazorScreenViewModel, INewGameVM, IBlankGameVM
    {
        private readonly BasicData _basicData;
        public CommandContainer CommandContainer { get; set; }

        public NewGameViewModel(CommandContainer command, BasicData basicData)
        {
            CommandContainer = command;
            _basicData = basicData;
        }
        public bool CanStartNewGame() => _basicData.MultiPlayer == false || _basicData.Client == false;

        [Command(EnumCommandCategory.Old)] //try old.
        public Task StartNewGameAsync()
        {
            _basicData.GameDataLoading = true; //hopefully this simple (?)
            return Aggregator.PublishAsync(new NewGameEventModel()); //this does not care what happens with the new game.
        }
    }
}