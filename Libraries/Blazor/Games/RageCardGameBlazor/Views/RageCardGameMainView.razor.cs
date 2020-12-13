using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using RageCardGameCP.Data;
using RageCardGameCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace RageCardGameBlazor.Views
{
    public partial class RageCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private RageCardGameVMData? _vmData;
        private RageCardGameGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<RageCardGameVMData>();
            _gameContainer = cons.Resolve<RageCardGameGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(RageCardGameMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(RageCardGameMainViewModel.Status))
                .AddLabel("Trump", nameof(RageCardGameMainViewModel.TrumpSuit))
                .AddLabel("Lead", nameof(RageCardGameMainViewModel.Lead));
            _scores = ScoreModule.GetScores();
            base.OnInitialized();
        }
    }
}