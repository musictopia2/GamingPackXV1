using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SkipboCP.Data;
using SkipboCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace SkipboBlazor.Views
{
    public partial class SkipboMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private SkipboVMData? _vmData;
        private SkipboGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<SkipboVMData>();
            _gameContainer = cons.Resolve<SkipboGameContainer>();
            _labels.AddLabel("Turn", nameof(SkipboMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(SkipboMainViewModel.Status))
               .AddLabel("RS Cards", nameof(SkipboMainViewModel.CardsToShuffle));
            _scores.Clear();
            _scores.AddColumn("In Stock", false, nameof(SkipboPlayerItem.InStock));
            int x;
            for (x = 1; x <= 4; x++)
            {
                var thisStr = "Discard" + x;
                _scores.AddColumn(thisStr, false, thisStr);
            }
            _scores.AddColumn("Stock Left", false, nameof(SkipboPlayerItem.StockLeft))
            .AddColumn("Cards Left", false, nameof(SkipboPlayerItem.ObjectCount));
            base.OnInitialized();
        }
    }
}