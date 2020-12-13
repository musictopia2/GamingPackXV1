using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using LottoDominosCP.Data;
using LottoDominosCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace LottoDominosBlazor.Views
{
    public partial class LottoDominosMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(LottoDominosMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(LottoDominosMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("# Chosen", true, nameof(LottoDominosPlayerItem.NumberChosen))
                .AddColumn("# Won", true, nameof(LottoDominosPlayerItem.NumberWon));
            base.OnInitialized();
        }
    }
}