using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using BasicMultiplayerDiceGamesCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
namespace BasicMultiplayerDiceGamesBlazor.Views
{
    public partial class BasicMultiplayerDiceGamesMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(BasicMultiplayerDiceGamesMainViewModel.NormalTurn))
                 .AddLabel("Roll", nameof(BasicMultiplayerDiceGamesMainViewModel.RollNumber))
                 .AddLabel("Status", nameof(BasicMultiplayerDiceGamesMainViewModel.Status));

            _scores.Clear();
            //use addcolumn for the columns to add.
            base.OnInitialized();
        }
        private string RollMethod => nameof(BasicMultiplayerDiceGamesMainViewModel.RollDiceAsync);
        private string EndMethod => nameof(BasicMultiplayerDiceGamesMainViewModel.EndTurnAsync);
    }
}