using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using YachtRaceCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using YachtRaceCP.Data;
namespace YachtRaceBlazor.Views
{
    public partial class YachtRaceMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(YachtRaceMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(YachtRaceMainViewModel.Status))
                .AddLabel("Error Message", nameof(YachtRaceMainViewModel.ErrorMessage));
            _scores.Clear();
            _scores.AddColumn("Time", true, nameof(YachtRacePlayerItem.Time));
            base.OnInitialized();
        }
        private string RollMethod => nameof(YachtRaceMainViewModel.RollDiceAsync);
        private string EndMethod => nameof(YachtRaceMainViewModel.FiveKindAsync);
    }
}