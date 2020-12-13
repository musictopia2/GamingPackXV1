using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using DeadDie96CP.Data;
using DeadDie96CP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace DeadDie96Blazor.Views
{
    public partial class DeadDie96MainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(DeadDie96MainViewModel.NormalTurn))
                 .AddLabel("Roll", nameof(DeadDie96MainViewModel.RollNumber))
                 .AddLabel("Status", nameof(DeadDie96MainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Current Score", true, nameof(DeadDie96PlayerItem.CurrentScore))
                .AddColumn("Total Score", true, nameof(DeadDie96PlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string RollMethod => nameof(DeadDie96MainViewModel.RollDiceAsync);
        private string EndMethod => nameof(DeadDie96MainViewModel.EndTurnAsync);
    }
}