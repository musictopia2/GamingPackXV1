using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using RookCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using RookCP.Data;
namespace RookBlazor.Views
{
    public partial class RookMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private RookVMData? _vmData;
        private RookGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<RookVMData>();
            _gameContainer = cons.Resolve<RookGameContainer>();
            _labels.AddLabel("Turn", nameof(RookMainViewModel.NormalTurn))
                .AddLabel("Trump", nameof(RookMainViewModel.TrumpSuit))
                .AddLabel("Status", nameof(RookMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Bid Amount", true, nameof(RookPlayerItem.BidAmount))
                .AddColumn("Tricks Won", false, nameof(RookPlayerItem.TricksWon))
                .AddColumn("Current Score", false, nameof(RookPlayerItem.CurrentScore))
                .AddColumn("Total Score", false, nameof(RookPlayerItem.TotalScore));
            base.OnInitialized();
        }

    }
}