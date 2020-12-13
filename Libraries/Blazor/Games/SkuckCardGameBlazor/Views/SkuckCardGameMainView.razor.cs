using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SkuckCardGameCP.Data;
using SkuckCardGameCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace SkuckCardGameBlazor.Views
{
    public partial class SkuckCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private SkuckCardGameVMData? _vmData;
        private SkuckCardGameGameContainer? _gameContainer;
        private CustomBasicList<SkuckCardGamePlayerItem> _players = new CustomBasicList<SkuckCardGamePlayerItem>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<SkuckCardGameVMData>();
            _gameContainer = cons.Resolve<SkuckCardGameGameContainer>();
            _players = _gameContainer.PlayerList!.GetAllPlayersStartingWithSelf();
            _labels.Clear();
            _labels.AddLabel("Round", nameof(SkuckCardGameMainViewModel.RoundNumber))
                .AddLabel("Trump", nameof(SkuckCardGameMainViewModel.TrumpSuit))
                .AddLabel("Turn", nameof(SkuckCardGameMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(SkuckCardGameMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Strength Hand", false, nameof(SkuckCardGamePlayerItem.StrengthHand))
                .AddColumn("Tie Breaker", false, nameof(SkuckCardGamePlayerItem.TieBreaker))
                .AddColumn("Bid Amount", false, nameof(SkuckCardGamePlayerItem.BidAmount), visiblePath: nameof(SkuckCardGamePlayerItem.BidVisible))
                .AddColumn("Tricks Taken", false, nameof(SkuckCardGamePlayerItem.TricksWon))
                .AddColumn("Cards In Hane", false, nameof(SkuckCardGamePlayerItem.ObjectCount))
                .AddColumn("Perfect Rounds", false, nameof(SkuckCardGamePlayerItem.PerfectRounds))
                .AddColumn("Total Score", false, nameof(SkuckCardGamePlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}