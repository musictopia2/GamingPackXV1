using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CaliforniaJackCP.Data;
using CaliforniaJackCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace CaliforniaJackBlazor.Views
{
    public partial class CaliforniaJackMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private CaliforniaJackVMData? _vmData;
        private CaliforniaJackGameContainer? _gameContainer;

        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<CaliforniaJackVMData>();
            _gameContainer = cons.Resolve<CaliforniaJackGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(CaliforniaJackMainViewModel.NormalTurn))
                 .AddLabel("Trump", nameof(CaliforniaJackMainViewModel.TrumpSuit))
                 .AddLabel("Status", nameof(CaliforniaJackMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(CaliforniaJackPlayerItem.ObjectCount))
                .AddColumn("Tricks Won", true, nameof(CaliforniaJackPlayerItem.TricksWon))
                .AddColumn("Points", true, nameof(CaliforniaJackPlayerItem.Points))
                .AddColumn("Total Score", true, nameof(CaliforniaJackPlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}