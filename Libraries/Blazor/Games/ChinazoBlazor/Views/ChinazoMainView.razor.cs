using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using ChinazoCP.Data;
using ChinazoCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace ChinazoBlazor.Views
{
    public partial class ChinazoMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private ChinazoVMData? _vmData;
        private ChinazoGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<ChinazoVMData>();
            _gameContainer = cons.Resolve<ChinazoGameContainer>();
            _labels.AddLabel("Turn", nameof(ChinazoMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(ChinazoMainViewModel.Status))
                .AddLabel("Other Turn", nameof(ChinazoMainViewModel.OtherLabel))
                .AddLabel("Phase", nameof(ChinazoMainViewModel.PhaseData));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(ChinazoPlayerItem.ObjectCount))
                .AddColumn("Current Score", false, nameof(ChinazoPlayerItem.CurrentScore))
                .AddColumn("Total Score", false, nameof(ChinazoPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string PassMethod => nameof(ChinazoMainViewModel.PassAsync);
        private string TakeMethod => nameof(ChinazoMainViewModel.TakeAsync);
        private string LayDownMethod => nameof(ChinazoMainViewModel.FirstSetsAsync);
    }
}