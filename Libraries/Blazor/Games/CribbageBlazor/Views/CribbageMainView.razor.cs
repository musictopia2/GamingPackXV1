using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CribbageCP.Data;
using CribbageCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace CribbageBlazor.Views
{
    public partial class CribbageMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private CribbageVMData? _vmData;
        private CribbageGameContainer? _gameContainer;
        private readonly CustomBasicList<LabelGridModel> _counts = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<CribbageVMData>();
            _gameContainer = cons.Resolve<CribbageGameContainer>();
            _labels.AddLabel("Turn", nameof(CribbageMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(CribbageMainViewModel.Status))
                .AddLabel("Dealer", nameof(CribbageMainViewModel.Dealer));
            _counts.Clear();
            _counts.AddLabel("Count", nameof(CribbageMainViewModel.TotalCount));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(CribbagePlayerItem.ObjectCount))
                .AddColumn("Is Skunk Hole", false, nameof(CribbagePlayerItem.IsSkunk), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("First Position", false, nameof(CribbagePlayerItem.FirstPosition))
                .AddColumn("Second Position", false, nameof(CribbagePlayerItem.SecondPosition))
                .AddColumn("Score Round", false, nameof(CribbagePlayerItem.ScoreRound))
                .AddColumn("Score Game", false, nameof(CribbagePlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string ContinueMethod => nameof(CribbageMainViewModel.ContinueAsync);
        private string CribMethod => nameof(CribbageMainViewModel.CribAsync);
        private string PlayMethod => nameof(CribbageMainViewModel.PlayAsync);
        private string Columns => aa.RepeatAuto(2);
    }
}