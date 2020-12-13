using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using RoundsCardGameCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using RoundsCardGameCP.Data;
namespace RoundsCardGameBlazor.Views
{
    public partial class RoundsCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private RoundsCardGameVMData? _vmData;
        private RoundsCardGameGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<RoundsCardGameVMData>();
            _gameContainer = cons.Resolve<RoundsCardGameGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(RoundsCardGameMainViewModel.NormalTurn))
               .AddLabel("Trump", nameof(RoundsCardGameMainViewModel.TrumpSuit))
               .AddLabel("Status", nameof(RoundsCardGameMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("# In Hand", true, nameof(RoundsCardGamePlayerItem.ObjectCount))
                .AddColumn("Tricks Won", true, nameof(RoundsCardGamePlayerItem.TricksWon))
                .AddColumn("Rounds Won", true, nameof(RoundsCardGamePlayerItem.RoundsWon))
                .AddColumn("Points", true, nameof(RoundsCardGamePlayerItem.CurrentPoints))
                .AddColumn("Total Score", true, nameof(RoundsCardGamePlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}