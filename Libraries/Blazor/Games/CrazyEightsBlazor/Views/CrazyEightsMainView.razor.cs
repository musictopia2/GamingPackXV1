using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CrazyEightsCP.Data;
using CrazyEightsCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace CrazyEightsBlazor.Views
{
    public partial class CrazyEightsMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private CrazyEightsVMData? _vmData;
        private CrazyEightsGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<CrazyEightsVMData>();
            _gameContainer = cons.Resolve<CrazyEightsGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(CrazyEightsMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(CrazyEightsMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(CrazyEightsPlayerItem.ObjectCount));
            base.OnInitialized();
        }
    }
}