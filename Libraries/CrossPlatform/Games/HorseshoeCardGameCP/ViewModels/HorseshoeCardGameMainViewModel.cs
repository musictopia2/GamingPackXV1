using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using HorseshoeCardGameCP.Cards;
using HorseshoeCardGameCP.Data;
using HorseshoeCardGameCP.Logic;
using System.Threading.Tasks;
namespace HorseshoeCardGameCP.ViewModels
{
    [InstanceGame]
    public class HorseshoeCardGameMainViewModel : TrickCardGamesVM<HorseshoeCardGameCardInformation, EnumSuitList>
    {
        private readonly HorseshoeCardGameMainGameClass _mainGame;
        private readonly HorseshoeCardGameVMData _model;
        public HorseshoeCardGameMainViewModel(CommandContainer commandContainer,
            HorseshoeCardGameMainGameClass mainGame,
            HorseshoeCardGameVMData viewModel,
            BasicData basicData,
            TestOptions test,
            IGamePackageResolver resolver
            )
            : base(commandContainer, mainGame, viewModel, basicData, test, resolver)
        {
            _mainGame = mainGame;
            _model = viewModel;
            _model.Deck1.NeverAutoDisable = true;
            LoadPlayerControls();
        }
        private void LoadPlayerControls()
        {
            _mainGame!.PlayerList!.ForEach(thisPlayer =>
            {
                if (thisPlayer.TempHand == null)
                {
                    throw new BasicBlankException("TempHand was never created.  Rethink");
                }
                thisPlayer.TempHand.SendEnableProcesses(this, () =>
                {
                    if (thisPlayer.PlayerCategory != EnumPlayerCategory.Self)
                    {
                        return false;
                    }
                    return true;
                });
                if (thisPlayer.PlayerCategory == EnumPlayerCategory.Self)
                {
                    thisPlayer.TempHand.SelectedCard -= TempHand_SelectedCard;
                    thisPlayer.TempHand.SelectedCard += TempHand_SelectedCard;
                }
            });
        }
        private void TempHand_SelectedCard()
        {
            _model.PlayerHand1!.UnselectAllObjects();
        }
        protected override Task OnAutoSelectedHandAsync()
        {
            HorseshoeCardGamePlayerItem thisPlayer = _mainGame!.PlayerList!.GetSelf();
            thisPlayer.TempHand!.UnselectAllCards();
            return base.OnAutoSelectedHandAsync();
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