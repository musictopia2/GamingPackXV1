using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using RoundsCardGameCP.Cards;
using RoundsCardGameCP.Data;
using RoundsCardGameCP.Logic;
using System.Threading.Tasks;
namespace RoundsCardGameCP.ViewModels
{
    [InstanceGame]
    public class RoundsCardGameMainViewModel : TrickCardGamesVM<RoundsCardGameCardInformation, EnumSuitList>
    {
        private readonly RoundsCardGameVMData _model;
        public RoundsCardGameMainViewModel(CommandContainer commandContainer,
            RoundsCardGameMainGameClass mainGame,
            RoundsCardGameVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _model = viewModel;
            _model.Deck1.NeverAutoDisable = true;
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