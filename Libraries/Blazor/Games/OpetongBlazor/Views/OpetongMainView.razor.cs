using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using OpetongCP.Data;
using OpetongCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace OpetongBlazor.Views
{
    public partial class OpetongMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private OpetongVMData? _vmData;
        private OpetongGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<OpetongVMData>();
            _gameContainer = cons.Resolve<OpetongGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(OpetongMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(OpetongMainViewModel.Status))
               .AddLabel("Instructions", nameof(OpetongMainViewModel.Instructions));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(OpetongPlayerItem.ObjectCount))
                .AddColumn("Sets Played", true, nameof(OpetongPlayerItem.SetsPlayed))
                .AddColumn("Score", true, nameof(OpetongPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string LayDownMethod => nameof(OpetongMainViewModel.PlaySetAsync);
    }
}