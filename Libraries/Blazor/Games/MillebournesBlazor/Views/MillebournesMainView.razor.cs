using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using MillebournesCP.Data;
using MillebournesCP.Logic;
using MillebournesCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace MillebournesBlazor.Views
{
    public partial class MillebournesMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private MillebournesVMData? _vmData;
        private MillebournesGameContainer? _gameContainer;
        private string GetRows => "33vh 33vh 33vh";
        private string GetColumns => "50vw 50vw";
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<MillebournesVMData>();
            _gameContainer = cons.Resolve<MillebournesGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(MillebournesMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(MillebournesMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Team", true, nameof(MillebournesPlayerItem.Team))
                .AddColumn("Miles", true, nameof(MillebournesPlayerItem.Miles))
                .AddColumn("Other Points", true, nameof(MillebournesPlayerItem.OtherPoints))
                .AddColumn("Total Points", true, nameof(MillebournesPlayerItem.TotalPoints))
                .AddColumn("# 200s", true, nameof(MillebournesPlayerItem.Number200s));
            base.OnInitialized();
        }
        private string GetAnimationTag(TeamCP team)
        {
            return $"team{team.TeamNumber}";
        }
    }
}