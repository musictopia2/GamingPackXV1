using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SinisterSixCP.Data;
using SinisterSixCP.ViewModels;
namespace SinisterSixBlazor.Views
{
    public partial class SinisterSixMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(SinisterSixMainViewModel.NormalTurn))
                 .AddLabel("Roll", nameof(SinisterSixMainViewModel.RollNumber))
                 .AddLabel("Status", nameof(SinisterSixMainViewModel.Status));

            _scores.Clear();
            _scores.AddColumn("Score", true, nameof(SinisterSixPlayerItem.Score));
            base.OnInitialized();
        }
        private string RollMethod => nameof(SinisterSixMainViewModel.RollDiceAsync);
        private string EndMethod => nameof(SinisterSixMainViewModel.EndTurnAsync);
        private string RemoveSelectedMethod => nameof(SinisterSixMainViewModel.RemoveDiceAsync);
    }
}