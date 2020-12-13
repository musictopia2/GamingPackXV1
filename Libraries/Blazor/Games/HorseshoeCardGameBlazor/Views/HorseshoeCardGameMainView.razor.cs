using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using HorseshoeCardGameCP.Data;
using HorseshoeCardGameCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace HorseshoeCardGameBlazor.Views
{
    public partial class HorseshoeCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private HorseshoeCardGameVMData? _vmData;
        private HorseshoeCardGameGameContainer? _gameContainer;
        private CustomBasicList<HorseshoeCardGamePlayerItem> _players = new CustomBasicList<HorseshoeCardGamePlayerItem>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<HorseshoeCardGameVMData>();
            _gameContainer = cons.Resolve<HorseshoeCardGameGameContainer>();
            _players = _gameContainer.PlayerList!.GetAllPlayersStartingWithSelf();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(HorseshoeCardGameMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(HorseshoeCardGameMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(HorseshoeCardGamePlayerItem.ObjectCount))
                .AddColumn("Tricks Won", false, nameof(HorseshoeCardGamePlayerItem.TricksWon))
                .AddColumn("Previous Score", false, nameof(HorseshoeCardGamePlayerItem.PreviousScore))
                .AddColumn("Total Score", false, nameof(HorseshoeCardGamePlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}