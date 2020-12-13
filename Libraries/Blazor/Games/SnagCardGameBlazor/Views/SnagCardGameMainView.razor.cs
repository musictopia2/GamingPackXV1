using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using SnagCardGameCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using SnagCardGameCP.Data;
namespace SnagCardGameBlazor.Views
{
    public partial class SnagCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private SnagCardGameVMData? _vmData;
        private SnagCardGameGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<SnagCardGameVMData>();
            _gameContainer = cons.Resolve<SnagCardGameGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(SnagCardGameMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(SnagCardGameMainViewModel.Status))
               .AddLabel("Instructions", nameof(SnagCardGameMainViewModel.Instructions));
            _scores.Clear();
            _scores.AddColumn("Cards Won", true, nameof(SnagCardGamePlayerItem.CardsWon))
                .AddColumn("Current Points", true, nameof(SnagCardGamePlayerItem.CurrentPoints))
                .AddColumn("Total Points", true, nameof(SnagCardGamePlayerItem.TotalPoints));
            base.OnInitialized();
        }
    }
}