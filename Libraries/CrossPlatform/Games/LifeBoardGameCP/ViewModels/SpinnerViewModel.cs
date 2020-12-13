using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using LifeBoardGameCP.Data;
using LifeBoardGameCP.Logic;
using System.Threading.Tasks;
namespace LifeBoardGameCP.ViewModels
{
    [InstanceGame]
    public class SpinnerViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        private readonly ISpinnerProcesses _processes;
        public SpinnerViewModel(CommandContainer commandContainer,
            ISpinnerProcesses processes,
            LifeBoardGameGameContainer gameContainer
            )
        {
            CommandContainer = commandContainer;
            _processes = processes;
            GameContainer = gameContainer;
        }
        public CommandContainer CommandContainer { get; set; }
        public async Task SpinAsync() //no need for the attributes because a different approach is used this time.
        {
            await _processes.StartSpinningAsync();
        }
        public LifeBoardGameGameContainer GameContainer { get; }
    }
}