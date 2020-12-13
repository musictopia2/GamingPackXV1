using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SixtySix2PlayerCP.Data;
using SixtySix2PlayerCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace SixtySix2PlayerBlazor.Views
{
    public partial class SixtySix2PlayerMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private SixtySix2PlayerVMData? _vmData;
        private SixtySix2PlayerGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<SixtySix2PlayerVMData>();
            _gameContainer = cons.Resolve<SixtySix2PlayerGameContainer>();
            _labels.AddLabel("Turn", nameof(SixtySix2PlayerMainViewModel.NormalTurn))
                .AddLabel("Trump", nameof(SixtySix2PlayerMainViewModel.TrumpSuit))
                .AddLabel("Deck Count", nameof(SixtySix2PlayerMainViewModel.DeckCount))
                .AddLabel("Status", nameof(SixtySix2PlayerMainViewModel.Status))
                .AddLabel("Bonus", nameof(SixtySix2PlayerMainViewModel.BonusPoints));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(SixtySix2PlayerPlayerItem.ObjectCount))
                .AddColumn("Tricks Won", true, nameof(SixtySix2PlayerPlayerItem.TricksWon))
                .AddColumn("Score Round", true, nameof(SixtySix2PlayerPlayerItem.ScoreRound))
                .AddColumn("Game Points Round", true, nameof(SixtySix2PlayerPlayerItem.GamePointsRound))
                .AddColumn("Total Points Game", true, nameof(SixtySix2PlayerPlayerItem.GamePointsGame));
            base.OnInitialized();
        }
        private string OutMethod => nameof(SixtySix2PlayerMainViewModel.GoOutAsync);
        private string MarriageMethod => nameof(SixtySix2PlayerMainViewModel.AnnouceMarriageAsync);
    }
}