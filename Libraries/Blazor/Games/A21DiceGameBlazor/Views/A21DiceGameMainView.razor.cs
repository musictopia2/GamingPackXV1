using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using A21DiceGameCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using A21DiceGameCP.Data;
namespace A21DiceGameBlazor.Views
{
    public partial class A21DiceGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(A21DiceGameMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(A21DiceGameMainViewModel.Status));
            _scores.Clear();
            //use addcolumn for the columns to add.
            _scores.AddColumn("# Of Rolls", true, nameof(A21DiceGamePlayerItem.NumberOfRolls))
                .AddColumn("Score", true, nameof(A21DiceGamePlayerItem.Score))
                .AddColumn("Was Tie", true, nameof(A21DiceGamePlayerItem.IsFaceOff), category: EnumScoreSpecialCategory.TrueFalse);
            base.OnInitialized();
        }
        private string RollMethod => nameof(A21DiceGameMainViewModel.RollDiceAsync);
        private string EndMethod => nameof(A21DiceGameMainViewModel.EndTurnAsync);
    }
}