using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using FlinchCP.Data;
using FlinchCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace FlinchBlazor.Views
{
    public partial class FlinchMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private FlinchVMData? _vmData;
        private FlinchGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<FlinchVMData>();
            _gameContainer = cons.Resolve<FlinchGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(FlinchMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(FlinchMainViewModel.Status))
               .AddLabel("RS Cards", nameof(FlinchMainViewModel.CardsToShuffle));
            _scores.Clear();
            _scores.AddColumn("In Stock", false, nameof(FlinchPlayerItem.InStock));
            int x;
            for (x = 1; x <= 5; x++)
            {
                var thisStr = "Discard" + x;
                _scores.AddColumn(thisStr, false, thisStr);
            }
            _scores.AddColumn("Stock Left", false, nameof(FlinchPlayerItem.StockLeft))
            .AddColumn("Cards Left", false, nameof(FlinchPlayerItem.ObjectCount));
            base.OnInitialized();
        }
        private string EndTurnMethod => nameof(FlinchMainViewModel.EndTurnAsync);
    }
}