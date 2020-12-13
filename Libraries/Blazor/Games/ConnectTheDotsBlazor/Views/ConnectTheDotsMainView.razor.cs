using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using ConnectTheDotsCP.Data;
using ConnectTheDotsCP.Graphics;
using ConnectTheDotsCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace ConnectTheDotsBlazor.Views
{
    public partial class ConnectTheDotsMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private GameBoardGraphicsCP? _graphics;
        private ConnectTheDotsGameContainer? _container;
        protected override void OnInitialized()
        {
            _graphics = cons!.Resolve<GameBoardGraphicsCP>();
            _container = cons.Resolve<ConnectTheDotsGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ConnectTheDotsMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(ConnectTheDotsMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Score", true, nameof(ConnectTheDotsPlayerItem.Score));
            base.OnInitialized();
        }
        private string EndMethod => nameof(ConnectTheDotsMainViewModel.EndTurnAsync);
    }
}