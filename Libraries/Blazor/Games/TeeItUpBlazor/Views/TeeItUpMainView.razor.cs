using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using TeeItUpCP.Data;
using TeeItUpCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace TeeItUpBlazor.Views
{
    public partial class TeeItUpMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private TeeItUpVMData? _vmData;
        private TeeItUpGameContainer? _gameContainer;
        private CustomBasicList<TeeItUpPlayerItem> _players = new CustomBasicList<TeeItUpPlayerItem>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<TeeItUpVMData>();
            _gameContainer = cons.Resolve<TeeItUpGameContainer>();
            if (_gameContainer.BasicData.MultiPlayer)
            {
                _players = _gameContainer!.PlayerList!.GetAllPlayersStartingWithSelf();
            }
            else
            {
                _players = _gameContainer.PlayerList!.ToCustomBasicList();
            }
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(TeeItUpMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(TeeItUpMainViewModel.Status))
                .AddLabel("Round", nameof(TeeItUpMainViewModel.Round))
                .AddLabel("Instructions", nameof(TeeItUpMainViewModel.Instructions));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(TeeItUpPlayerItem.ObjectCount))
                .AddColumn("Went Out", true, nameof(TeeItUpPlayerItem.WentOut), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("Previous Score", true, nameof(TeeItUpPlayerItem.PreviousScore))
                .AddColumn("Total Score", true, nameof(TeeItUpPlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}