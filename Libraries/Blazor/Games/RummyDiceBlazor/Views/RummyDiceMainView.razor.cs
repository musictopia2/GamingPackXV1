using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using RummyDiceCP.Data;
using RummyDiceCP.ViewModels;
namespace RummyDiceBlazor.Views
{
    public partial class RummyDiceMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(RummyDiceMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(RummyDiceMainViewModel.Status))
                 .AddLabel("Phase", nameof(RummyDiceMainViewModel.CurrentPhase))
                 .AddLabel("Roll", nameof(RummyDiceMainViewModel.RollNumber))
                 .AddLabel("Score", nameof(RummyDiceMainViewModel.Score));
            _scores.Clear();
            _scores.AddColumn("Score Round", false, nameof(RummyDicePlayerItem.ScoreRound))
                .AddColumn("Score Game", false, nameof(RummyDicePlayerItem.ScoreGame))
                .AddColumn("Phase", false, nameof(RummyDicePlayerItem.Phase));
            base.OnInitialized();
        }
        private string EndTurnMethod => nameof(RummyDiceMainViewModel.EndTurnAsync);
        private string PutBackMethod => nameof(RummyDiceMainViewModel.BoardAsync);
        private string RollMethod => nameof(RummyDiceMainViewModel.RollAsync);
        private string ScoreMethod => nameof(RummyDiceMainViewModel.CheckAsync);
    }
}