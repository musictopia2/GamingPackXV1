using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using Spades2PlayerCP.Data;
using Spades2PlayerCP.Logic;
using System.Threading.Tasks;
namespace Spades2PlayerCP.ViewModels
{
    [InstanceGame]
    public class SpadesBiddingViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly Spades2PlayerVMData Model;
        private readonly Spades2PlayerMainGameClass _mainGame;
        public SpadesBiddingViewModel(CommandContainer commandContainer, Spades2PlayerVMData model, Spades2PlayerMainGameClass mainGame)
        {
            CommandContainer = commandContainer;
            Model = model;
            _mainGame = mainGame;
            Model.Bid1.ChangedNumberValueAsync += Bid1_ChangedNumberValueAsync;
        }
        protected override Task TryCloseAsync()
        {
            Model.Bid1.ChangedNumberValueAsync -= Bid1_ChangedNumberValueAsync;
            return base.TryCloseAsync();
        }
        public CommandContainer CommandContainer { get; set; }
        private Task Bid1_ChangedNumberValueAsync(int chosen)
        {
            Model.BidAmount = chosen;
            return Task.CompletedTask;
        }
        public bool CanBid => Model.BidAmount > -1;
        [Command(EnumCommandCategory.Plain)]
        public async Task BidAsync()
        {
            if (_mainGame.BasicData!.MultiPlayer == true)
            {
                await _mainGame.Network!.SendAllAsync("bid", Model.BidAmount);
            }
            await _mainGame.ProcessBidAsync();
        }
    }
}