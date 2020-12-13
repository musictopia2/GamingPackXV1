using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using Phase10CP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using Phase10CP.Data;
namespace Phase10Blazor.Views
{
    public partial class Phase10MainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private Phase10VMData? _vmData;
        private Phase10GameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<Phase10VMData>();
            _gameContainer = cons.Resolve<Phase10GameContainer>();
            _labels.AddLabel("Turn", nameof(Phase10MainViewModel.NormalTurn))
                .AddLabel("Status", nameof(Phase10MainViewModel.Status))
                .AddLabel("Phase", nameof(Phase10MainViewModel.CurrentPhase));
            _scores.Clear();
            _scores.AddColumn("Score", true, nameof(Phase10PlayerItem.TotalScore))
                .AddColumn("Cards Left", true, nameof(Phase10PlayerItem.ObjectCount))
                .AddColumn("Phase", true, nameof(Phase10PlayerItem.Phase))
                .AddColumn("Skipped", true, nameof(Phase10PlayerItem.MissNextTurn), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Completed", true, nameof(Phase10PlayerItem.Completed), category: EnumScoreSpecialCategory.TrueFalse);
            base.OnInitialized();
        }
        private string CompleteMethod => nameof(Phase10MainViewModel.CompletePhaseAsync);
    }
}