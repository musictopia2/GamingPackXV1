using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using DominosRegularCP.Data;
using DominosRegularCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace DominosRegularBlazor.Views
{
    public partial class DominosRegularMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(DominosRegularMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(DominosRegularMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Total Score", true, nameof(DominosRegularPlayerItem.TotalScore))
                .AddColumn("Dominos Left", true, nameof(DominosRegularPlayerItem.ObjectCount));
            base.OnInitialized();
        }
        public SimpleDominoInfo GetDomino
        {
            get
            {
                SimpleDominoInfo output = new SimpleDominoInfo();
                output.IsUnknown = true;
                output.Deck = 1; //needed so the back can show up properly.
                return output;
            }
        }
        private string EndMethod => nameof(DominosRegularMainViewModel.EndTurnAsync);
    }
}