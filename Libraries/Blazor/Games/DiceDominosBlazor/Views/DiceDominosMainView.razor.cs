using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using DiceDominosCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using DiceDominosCP.Data;
namespace DiceDominosBlazor.Views
{
    public partial class DiceDominosMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(DiceDominosMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(DiceDominosMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Dominos Won", true, nameof(DiceDominosPlayerItem.DominosWon));
            base.OnInitialized();
        }
        private string RollMethod => nameof(DiceDominosMainViewModel.RollDiceAsync);
        private string EndMethod => nameof(DiceDominosMainViewModel.EndTurnAsync);
    }
}