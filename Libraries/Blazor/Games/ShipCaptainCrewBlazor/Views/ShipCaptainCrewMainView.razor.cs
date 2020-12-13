using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using ShipCaptainCrewCP.Data;
using ShipCaptainCrewCP.ViewModels;
namespace ShipCaptainCrewBlazor.Views
{
    public partial class ShipCaptainCrewMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ShipCaptainCrewMainViewModel.NormalTurn))
                 .AddLabel("Roll", nameof(ShipCaptainCrewMainViewModel.RollNumber))
                 .AddLabel("Status", nameof(ShipCaptainCrewMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Score", true, nameof(ShipCaptainCrewPlayerItem.Score))
                .AddColumn("Out", true, nameof(ShipCaptainCrewPlayerItem.WentOut), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Wins", true, nameof(ShipCaptainCrewPlayerItem.Wins));
            base.OnInitialized();
        }
        private string RollMethod => nameof(ShipCaptainCrewMainViewModel.RollDiceAsync);
    }
}