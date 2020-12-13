using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using GoFishCP.Data;
using GoFishCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace GoFishBlazor.Views
{
    public partial class GoFishMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private GoFishVMData? _vmData;
        private GoFishGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<GoFishVMData>();
            _gameContainer = cons.Resolve<GoFishGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(GoFishMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(GoFishMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(GoFishPlayerItem.ObjectCount))
                .AddColumn("Pairs", true, nameof(GoFishPlayerItem.Pairs));
            base.OnInitialized();
        }
        private string EndTurnMethod => nameof(GoFishMainViewModel.EndTurnAsync);
    }
}