using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using RageCardGameCP.ViewModels;
namespace RageCardGameBlazor.Views
{
    public partial class RageBiddingView
    {
        private string BidMethod => nameof(RageBiddingViewModel.BidAsync);
        private CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnParametersSet()
        {
            _scores = ScoreModule.GetScores();
            _labels.Clear();
            _labels.AddLabel("Trump", nameof(RageBiddingViewModel.TrumpSuit))
                .AddLabel("Turn", nameof(RageBiddingViewModel.NormalTurn));
            base.OnParametersSet();
        }
    }
}