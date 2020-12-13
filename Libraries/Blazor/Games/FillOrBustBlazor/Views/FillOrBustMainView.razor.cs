using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using FillOrBustCP.Data;
using FillOrBustCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace FillOrBustBlazor.Views
{
    public partial class FillOrBustMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private FillOrBustVMData? _vmData;
        private FillOrBustGameContainer? _gameContainer;
        private readonly CustomBasicList<LabelGridModel> _temps = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<FillOrBustVMData>();
            _gameContainer = cons.Resolve<FillOrBustGameContainer>();
            _labels.Clear();
            _temps.Clear();
            _labels.AddLabel("Turn", nameof(FillOrBustMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(FillOrBustMainViewModel.Status));
            _temps.AddLabel("Temporary Score", nameof(FillOrBustMainViewModel.TempScore))
                .AddLabel("Score", nameof(FillOrBustMainViewModel.DiceScore));
            _scores.Clear();
            _scores.AddColumn("Current Score", true, nameof(FillOrBustPlayerItem.CurrentScore))
                .AddColumn("Total Score", true, nameof(FillOrBustPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string RollMethod => nameof(FillOrBustMainViewModel.RollDiceAsync);
        private string RemoveMethod => nameof(FillOrBustMainViewModel.ChooseDiceAsync);
        private string EndMethod => nameof(FillOrBustMainViewModel.EndTurnAsync);
    }
}