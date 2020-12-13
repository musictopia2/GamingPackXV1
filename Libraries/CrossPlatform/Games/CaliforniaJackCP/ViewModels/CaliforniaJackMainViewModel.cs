using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using CaliforniaJackCP.Cards;
using CaliforniaJackCP.Data;
using CaliforniaJackCP.Logic;
using System.Threading.Tasks;
namespace CaliforniaJackCP.ViewModels
{
    [InstanceGame]
    public class CaliforniaJackMainViewModel : TrickCardGamesVM<CaliforniaJackCardInformation, EnumSuitList>
    {
        private readonly CaliforniaJackVMData _model;
        public CaliforniaJackMainViewModel(CommandContainer commandContainer,
            CaliforniaJackMainGameClass mainGame,
            CaliforniaJackVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _model = viewModel;
            _model.Deck1.NeverAutoDisable = true;
            _model.PlayerHand1!.Maximum = 6;
            _model.Deck1!.DeckStyle = EnumDeckPileStyle.AlwaysKnown;
        }
        protected override bool CanEnableDeck()
        {
            return false; 
        }
        protected override bool CanEnablePile1()
        {
            return false;
        }
        protected override async Task ProcessDiscardClickedAsync()
        {
            await Task.CompletedTask;
        }
        public override bool CanEnableAlways()
        {
            return true;
        }
    }
}