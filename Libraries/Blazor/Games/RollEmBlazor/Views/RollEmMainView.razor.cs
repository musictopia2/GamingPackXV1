using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using RollEmCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using RollEmCP.Logic;
using RollEmCP.Data;

namespace RollEmBlazor.Views
{
    public partial class RollEmMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private GameBoardGraphicsCP? _boardData;
        protected override void OnInitialized()
        {
            _boardData = cons!.Resolve<GameBoardGraphicsCP>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(RollEmMainViewModel.NormalTurn))
                 .AddLabel("Round", nameof(RollEmMainViewModel.Round))
                 .AddLabel("Status", nameof(RollEmMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Score Round", true, nameof(RollEmPlayerItem.ScoreRound))
                .AddColumn("Score Game", true, nameof(RollEmPlayerItem.ScoreGame));
            base.OnInitialized();
        }
        private string RollMethod => nameof(RollEmMainViewModel.RollDiceAsync);
        private string EndMethod => nameof(RollEmMainViewModel.EndTurnAsync);
    }
}