using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using DominosMexicanTrainCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using DominosMexicanTrainCP.Data;
using BasicGameFrameworkLibrary.Dominos;
namespace DominosMexicanTrainBlazor.Views
{
    public partial class DominosMexicanTrainMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(DominosMexicanTrainMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(DominosMexicanTrainMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Dominos Left", true, nameof(DominosMexicanTrainPlayerItem.ObjectCount))
                .AddColumn("Total Score", true, nameof(DominosMexicanTrainPlayerItem.TotalScore))
                .AddColumn("Previous Score", true, nameof(DominosMexicanTrainPlayerItem.PreviousScore))
                .AddColumn("# Previous", true, nameof(DominosMexicanTrainPlayerItem.PreviousLeft));
            base.OnInitialized();
        }
        public MexicanDomino GetDomino
        {
            get
            {
                MexicanDomino output = new MexicanDomino();
                output.IsUnknown = true;
                output.Deck = 1; //needed so the back can show up properly.
                return output;
            }
        }
        public string EndMethod => nameof(DominosMexicanTrainMainViewModel.EndTurnAsync);
        public string LongestMethod => nameof(DominosMexicanTrainMainViewModel.LongestTrain);
    }
}