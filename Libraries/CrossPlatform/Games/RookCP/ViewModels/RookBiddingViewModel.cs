using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using RookCP.Data;
using RookCP.Logic;
using System.Threading.Tasks;
namespace RookCP.ViewModels
{
    [InstanceGame]
    public class RookBiddingViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly RookVMData Model;
        private readonly IBidProcesses _processes;
        public RookBiddingViewModel(CommandContainer commandContainer,
            RookVMData model,
            IBidProcesses processes
            )
        {
            CommandContainer = commandContainer;
            Model = model;
            _processes = processes;
        }
        public CommandContainer CommandContainer { get; set; }
        public bool CanBid => Model.BidChosen > -1;
        [Command(EnumCommandCategory.Plain)]
        public async Task BidAsync()
        {
            await _processes.ProcessBidAsync();
        }
        public bool CanPass => Model.CanPass;
        [Command(EnumCommandCategory.Plain)]
        public async Task PassAsync()
        {
            await _processes.PassBidAsync();
        }
    }
}