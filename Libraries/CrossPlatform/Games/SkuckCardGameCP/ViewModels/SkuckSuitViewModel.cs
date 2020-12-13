using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using SkuckCardGameCP.Data;
using SkuckCardGameCP.Logic;
using System.Threading.Tasks;
namespace SkuckCardGameCP.ViewModels
{
    [InstanceGame]
    public class SkuckSuitViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly SkuckCardGameVMData Model;
        private readonly SkuckCardGameGameContainer _gameContainer;
        private readonly ITrumpProcesses _processes;
        public SkuckSuitViewModel(CommandContainer commandContainer,
            SkuckCardGameVMData model,
            SkuckCardGameGameContainer gameContainer,
            ITrumpProcesses processes
            )
        {
            CommandContainer = commandContainer;
            Model = model;
            _gameContainer = gameContainer;
            _processes = processes;
        }
        public CommandContainer CommandContainer { get; set; }
        public bool CanTrump => Model.TrumpSuit != EnumSuitList.None;
        [Command(EnumCommandCategory.Plain)]
        public async Task TrumpAsync()
        {
            if (_gameContainer.BasicData!.MultiPlayer == true)
            {
                await _gameContainer.Network!.SendAllAsync("trump", Model.TrumpSuit);
            }
            await _processes!.TrumpChosenAsync();
        }
    }
}