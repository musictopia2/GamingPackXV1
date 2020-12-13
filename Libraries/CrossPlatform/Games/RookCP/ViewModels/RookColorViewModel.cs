using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using RookCP.Data;
using RookCP.Logic;
using System.Threading.Tasks;
namespace RookCP.ViewModels
{
    [InstanceGame]
    public class RookColorViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly RookVMData Model;
        private readonly ITrumpProcesses _processes;
        public RookColorViewModel(CommandContainer commandContainer,
            RookVMData model,
            ITrumpProcesses processes
            )
        {
            CommandContainer = commandContainer;
            Model = model;
            _processes = processes;
        }
        public CommandContainer CommandContainer { get; set; }
        public bool CanTrump => Model.ColorChosen != EnumColorTypes.None;
        [Command(EnumCommandCategory.Plain)]
        public async Task TrumpAsync()
        {
            await _processes.ProcessTrumpAsync();
        }
    }
}