using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using Spades2PlayerCP.Data;
using Spades2PlayerCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace Spades2PlayerBlazor.Views
{
    public partial class Spades2PlayerMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private Spades2PlayerVMData? _vmData;
        private Spades2PlayerGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<Spades2PlayerVMData>();
            _gameContainer = cons.Resolve<Spades2PlayerGameContainer>();
            _labels.AddLabel("Turn", nameof(Spades2PlayerMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(Spades2PlayerMainViewModel.Status))
                .AddLabel("Round", nameof(Spades2PlayerMainViewModel.RoundNumber));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(Spades2PlayerPlayerItem.ObjectCount))
                .AddColumn("# Bidded", false, nameof(Spades2PlayerPlayerItem.HowManyBids))
                .AddColumn("Tricks Won", false, nameof(Spades2PlayerPlayerItem.TricksWon))
                .AddColumn("Bags", false, nameof(Spades2PlayerPlayerItem.Bags))
                .AddColumn("Current Score", false, nameof(Spades2PlayerPlayerItem.CurrentScore))
                .AddColumn("Total Score", false, nameof(Spades2PlayerPlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}