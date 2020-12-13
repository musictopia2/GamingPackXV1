using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using RackoCP.Data;
using RackoCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace RackoBlazor.Views
{
    public partial class RackoMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private RackoVMData? _vmData;
        private RackoGameContainer? _gameContainer;
        private string GetColumns => aa.RepeatAuto(2);
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<RackoVMData>();
            _gameContainer = cons.Resolve<RackoGameContainer>();
            _labels.AddLabel("Turn", nameof(RackoMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(RackoMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(RackoPlayerItem.ObjectCount))
                .AddColumn("Score Round", true, nameof(RackoPlayerItem.ScoreRound))
                .AddColumn("Score Game", true, nameof(RackoPlayerItem.TotalScore));
            int x;
            for (x = 1; x <= 10; x++)
            {
                _scores.AddColumn("Section" + x, false, "Value" + x, nameof(RackoPlayerItem.CanShowValues));// 2 bindings.
            }
            base.OnInitialized();
        }
        private string DiscardCurrentMethod => nameof(RackoMainViewModel.DiscardCurrentAsync);
        private string RackoMethod => nameof(RackoMainViewModel.RackoAsync);
    }
}