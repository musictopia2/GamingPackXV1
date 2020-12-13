using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using HuseHeartsCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using HuseHeartsCP.Data;
namespace HuseHeartsBlazor.Views
{
    public partial class HuseHeartsMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private HuseHeartsVMData? _vmData;
        private HuseHeartsGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<HuseHeartsVMData>();
            _gameContainer = cons.Resolve<HuseHeartsGameContainer>();
            _labels.AddLabel("Turn", nameof(HuseHeartsMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(HuseHeartsMainViewModel.Status))
                .AddLabel("Round", nameof(HuseHeartsMainViewModel.RoundNumber));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(HuseHeartsPlayerItem.ObjectCount))
                .AddColumn("Tricks Won", false, nameof(HuseHeartsPlayerItem.TricksWon))
                .AddColumn("Current Score", false, nameof(HuseHeartsPlayerItem.CurrentScore))
                .AddColumn("Previous Score", false, nameof(HuseHeartsPlayerItem.PreviousScore))
                .AddColumn("Total Score", false, nameof(HuseHeartsPlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}