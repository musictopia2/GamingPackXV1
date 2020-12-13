using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using FiveCrownsCP.Data;
using FiveCrownsCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace FiveCrownsBlazor.Views
{
    public partial class FiveCrownsMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private FiveCrownsVMData? _vmData;
        private FiveCrownsGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<FiveCrownsVMData>();
            _gameContainer = cons.Resolve<FiveCrownsGameContainer>();
            _labels.AddLabel("Turn", nameof(FiveCrownsMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(FiveCrownsMainViewModel.Status))
                 .AddLabel("Up To", nameof(FiveCrownsMainViewModel.UpTo));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(FiveCrownsPlayerItem.ObjectCount))
                .AddColumn("Current Score", true, nameof(FiveCrownsPlayerItem.CurrentScore))
                .AddColumn("Total Score", true, nameof(FiveCrownsPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string LayDownMethod => nameof(FiveCrownsMainViewModel.LayDownSetsAsync);
        private string BackMethod => nameof(FiveCrownsMainViewModel.Back);
    }
}