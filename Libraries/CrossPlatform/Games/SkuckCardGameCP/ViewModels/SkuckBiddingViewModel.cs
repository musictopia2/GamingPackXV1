using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using SkuckCardGameCP.Data;
using SkuckCardGameCP.Logic;
using System.Linq;
using System.Threading.Tasks;
namespace SkuckCardGameCP.ViewModels
{
    [InstanceGame]
    public class SkuckBiddingViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly SkuckCardGameVMData Model;
        private readonly SkuckCardGameGameContainer _gameContainer;
        private readonly IBidProcesses _processes;
        public SkuckBiddingViewModel(CommandContainer commandContainer,
            SkuckCardGameVMData model,
            SkuckCardGameGameContainer gameContainer,
            IBidProcesses processes
            )
        {
            CommandContainer = commandContainer;
            Model = model;
            _gameContainer = gameContainer;
            _processes = processes;
        }
        public CommandContainer CommandContainer { get; set; }
        public bool CanBid => Model.BidAmount > -1;
        [Command(EnumCommandCategory.Plain)]
        public async Task BidAsync()
        {
            if (_gameContainer.BasicData!.MultiPlayer == true)
            {
                await _gameContainer.Network!.SendAllAsync("bid", Model.BidAmount);
            }
            int id = _gameContainer!.PlayerList!.Where(items => items.PlayerCategory == EnumPlayerCategory.Self).Single().Id;
            await _processes.ProcessBidAmountAsync(id);
        }
    }
}