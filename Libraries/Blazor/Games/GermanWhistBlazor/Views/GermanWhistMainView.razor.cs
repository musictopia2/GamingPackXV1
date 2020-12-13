using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using GermanWhistCP.Data;
using GermanWhistCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace GermanWhistBlazor.Views
{
    public partial class GermanWhistMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private GermanWhistVMData? _vmData;
        private GermanWhistGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<GermanWhistVMData>();
            _gameContainer = cons.Resolve<GermanWhistGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(GermanWhistMainViewModel.NormalTurn))
               .AddLabel("Trump", nameof(GermanWhistMainViewModel.TrumpSuit))
               .AddLabel("Status", nameof(GermanWhistMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(GermanWhistPlayerItem.ObjectCount))
                .AddColumn("Tricks Won", true, nameof(GermanWhistPlayerItem.TricksWon));
            base.OnInitialized();
        }
    }
}