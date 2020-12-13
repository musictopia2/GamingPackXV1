using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using PickelCardGameCP.Cards;
using PickelCardGameCP.Data;
using PickelCardGameCP.Logic;
using System.Threading.Tasks;
namespace PickelCardGameCP.ViewModels
{
    [InstanceGame]
    public class PickelCardGameMainViewModel : TrickCardGamesVM<PickelCardGameCardInformation, EnumSuitList>
    {
        private readonly PickelCardGameVMData _model;
        public PickelCardGameMainViewModel(CommandContainer commandContainer,
            PickelCardGameMainGameClass mainGame,
            PickelCardGameVMData viewModel,
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