using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using BasicGameFrameworkLibrary.SpecializedGameTypes.StockClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using SkipboCP.Cards;
using SkipboCP.Data;
using SkipboCP.Logic;
using System.Threading.Tasks;
using static SkipboCP.Data.GlobalConstants;
namespace SkipboCP.ViewModels
{
    [InstanceGame]
    public class PlayerPilesViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly SkipboVMData Model;
        private readonly SkipboMainGameClass _mainGame;
        public PlayerPilesViewModel(CommandContainer commandContainer, SkipboGameContainer gameContainer, SkipboVMData model, SkipboMainGameClass mainGame)
        {
            gameContainer.SingleInfo = gameContainer.PlayerList!.GetWhoPlayer();
            CommandContainer = commandContainer;
            GameContainer = gameContainer;
            Model = model;
            _mainGame = mainGame;
            Model.StockPile.ClearCards();
            gameContainer.SingleInfo!.StockList.ForEach(x =>
            {
                model.StockPile.AddCard(x);
            });
            Model.DiscardPiles = new DiscardPilesVM<SkipboCardInformation>(commandContainer);
            Model.DiscardPiles.Init(HowManyDiscards);
            if (gameContainer.SingleInfo!.DiscardList.Count > 0)
            {
                model.DiscardPiles!.PileList!.ReplaceRange(gameContainer.SingleInfo.DiscardList);
            }
            Model.DiscardPiles.PileClickedAsync += DiscardPiles_PileClickedAsync;
            Model.StockPile!.StockClickedAsync += StockPile_StockClickedAsync;
        }
        protected override Task TryCloseAsync()
        {
            Model.DiscardPiles!.PileClickedAsync -= DiscardPiles_PileClickedAsync;
            Model.StockPile!.StockClickedAsync -= StockPile_StockClickedAsync;
            return base.TryCloseAsync();
        }
        public CommandContainer CommandContainer { get; set; }
        public SkipboGameContainer GameContainer { get; }
        private async Task DiscardPiles_PileClickedAsync(int index, BasicPileInfo<SkipboCardInformation> thisPile)
        {
            int playerDeck = Model!.PlayerHand1!.ObjectSelected();
            if (playerDeck > 0)
            {
                await _mainGame!.AddToDiscardAsync(index, playerDeck);
                return;
            }
            if (Model.DiscardPiles!.HasCard(index) == false)
            {
                return;
            }
            if (Model.DiscardPiles.PileList![index].IsSelected == true)
            {
                Model.DiscardPiles.PileList[index].IsSelected = false;
                return;
            }
            _mainGame!.UnselectAllCards();
            Model.DiscardPiles.SelectUnselectSinglePile(index);
        }
        private Task StockPile_StockClickedAsync()
        {
            int nums = Model.StockPile!.CardSelected();
            _mainGame!.UnselectAllCards();
            if (nums > 0)
            {
                Model.StockPile.UnselectCard();
                return Task.CompletedTask;
            }
            Model.StockPile.SelectCard();
            return Task.CompletedTask;
        }
    }
}