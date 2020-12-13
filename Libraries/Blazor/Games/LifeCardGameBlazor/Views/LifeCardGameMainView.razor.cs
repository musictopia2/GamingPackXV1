using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using LifeCardGameCP.Data;
using LifeCardGameCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace LifeCardGameBlazor.Views
{
    public partial class LifeCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private LifeCardGameVMData? _vmData;
        private LifeCardGameGameContainer? _gameContainer;
        private string GetColums => aa.RepeatAuto(2);
        private CustomBasicList<LifeCardGamePlayerItem> _players = new CustomBasicList<LifeCardGamePlayerItem>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<LifeCardGameVMData>();
            _gameContainer = cons.Resolve<LifeCardGameGameContainer>();
            _players = _gameContainer.PlayerList!.GetAllPlayersStartingWithSelf();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(LifeCardGameMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(LifeCardGameMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(LifeCardGamePlayerItem.ObjectCount))
                .AddColumn("Points", true, nameof(LifeCardGamePlayerItem.Points));
            base.OnInitialized();
        }
        private string YearsMethod => nameof(LifeCardGameMainViewModel.YearsPassedAsync);
        private string PlayMethod => nameof(LifeCardGameMainViewModel.PlayCardAsync);
    }
}