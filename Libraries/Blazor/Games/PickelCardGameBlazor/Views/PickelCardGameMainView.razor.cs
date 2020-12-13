using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using PickelCardGameCP.Data;
using PickelCardGameCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace PickelCardGameBlazor.Views
{
    public partial class PickelCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private PickelCardGameVMData? _vmData;
        private PickelCardGameGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<PickelCardGameVMData>();
            _gameContainer = cons.Resolve<PickelCardGameGameContainer>();
            _labels.AddLabel("Turn", nameof(PickelCardGameMainViewModel.NormalTurn))
                 .AddLabel("Trump", nameof(PickelCardGameMainViewModel.TrumpSuit))
                 .AddLabel("Status", nameof(PickelCardGameMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Suit Desired", true, nameof(PickelCardGamePlayerItem.SuitDesired))
                .AddColumn("Bid Amount", false, nameof(PickelCardGamePlayerItem.BidAmount))
                .AddColumn("Tricks Won", false, nameof(PickelCardGamePlayerItem.TricksWon))
                .AddColumn("Current Score", false, nameof(PickelCardGamePlayerItem.CurrentScore))
                .AddColumn("Total Score", false, nameof(PickelCardGamePlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}