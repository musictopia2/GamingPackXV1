using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using YahtzeeHandsDownCP.Data;
using YahtzeeHandsDownCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace YahtzeeHandsDownBlazor.Views
{
    public partial class YahtzeeHandsDownMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private YahtzeeHandsDownVMData? _vmData;
        private YahtzeeHandsDownGameContainer? _gameContainer;
        private string GetColumns => aa.RepeatAuto(2);
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<YahtzeeHandsDownVMData>();
            _gameContainer = cons.Resolve<YahtzeeHandsDownGameContainer>();
            _labels.AddLabel("Turn", nameof(YahtzeeHandsDownMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(YahtzeeHandsDownMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(YahtzeeHandsDownPlayerItem.ObjectCount))
                .AddColumn("Total Score", true, nameof(YahtzeeHandsDownPlayerItem.TotalScore))
                .AddColumn("Won Last Round", true, nameof(YahtzeeHandsDownPlayerItem.WonLastRound))
                .AddColumn("Score Round", true, nameof(YahtzeeHandsDownPlayerItem.ScoreRound));
            base.OnInitialized();
        }
        private string OutMethod => nameof(YahtzeeHandsDownMainViewModel.GoOutAsync);
        private string EndMethod => nameof(YahtzeeHandsDownMainViewModel.EndTurnAsync);
    }
}