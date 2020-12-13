using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using GalaxyCardGameCP.Data;
using GalaxyCardGameCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace GalaxyCardGameBlazor.Views
{
    public partial class GalaxyCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private GalaxyCardGameVMData? _vmData;
        private GalaxyCardGameGameContainer? _gameContainer;
        private CustomBasicList<GalaxyCardGamePlayerItem> _players = new CustomBasicList<GalaxyCardGamePlayerItem>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<GalaxyCardGameVMData>();
            _gameContainer = cons.Resolve<GalaxyCardGameGameContainer>();
            _players = _gameContainer.PlayerList!.GetAllPlayersStartingWithSelf();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(GalaxyCardGameMainViewModel.NormalTurn))
                .AddLabel("Trump", nameof(GalaxyCardGameMainViewModel.TrumpSuit))
                .AddLabel("Status", nameof(GalaxyCardGameMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(GalaxyCardGamePlayerItem.ObjectCount))
                .AddColumn("Tricks Won", true, nameof(GalaxyCardGamePlayerItem.TricksWon));
            base.OnInitialized();
        }
        private string EndMethod => nameof(GalaxyCardGameMainViewModel.EndTurnAsync);
        private string MoonMethod => nameof(GalaxyCardGameMainViewModel.MoonAsync);
    }
}