using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using Pinochle2PlayerCP.Data;
using Pinochle2PlayerCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace Pinochle2PlayerBlazor.Views
{
    public partial class Pinochle2PlayerMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private Pinochle2PlayerVMData? _vmData;
        private Pinochle2PlayerGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<Pinochle2PlayerVMData>();
            _gameContainer = cons.Resolve<Pinochle2PlayerGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(Pinochle2PlayerMainViewModel.NormalTurn))
               .AddLabel("Deck Count", nameof(Pinochle2PlayerMainViewModel.DeckCount))
               .AddLabel("Trump", nameof(Pinochle2PlayerMainViewModel.TrumpSuit))
               .AddLabel("Status", nameof(Pinochle2PlayerMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(Pinochle2PlayerPlayerItem.ObjectCount))
                .AddColumn("Tricks Won", false, nameof(Pinochle2PlayerPlayerItem.TricksWon))
                .AddColumn("Current Score", false, nameof(Pinochle2PlayerPlayerItem.CurrentScore))
                .AddColumn("Total Score", false, nameof(Pinochle2PlayerPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string EndMethod => nameof(Pinochle2PlayerMainViewModel.EndTurnAsync);
        private string MeldMethod => nameof(Pinochle2PlayerMainViewModel.MeldAsync);
    }
}