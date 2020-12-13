using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using UnoCP.Data;
using UnoCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace UnoBlazor.Views
{
    public partial class UnoMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private UnoVMData? _vmData;
        private UnoGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<UnoVMData>();
            _gameContainer = cons.Resolve<UnoGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(UnoMainViewModel.NormalTurn))
                .AddLabel("Next", nameof(UnoMainViewModel.NextPlayer))
                .AddLabel("Status", nameof(UnoMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(UnoPlayerItem.ObjectCount))
                .AddColumn("Total Points", true, nameof(UnoPlayerItem.TotalPoints))
                .AddColumn("Previous Points", true, nameof(UnoPlayerItem.PreviousPoints));
            base.OnInitialized();
        }
        private string EndTurnMethod => nameof(UnoMainViewModel.EndTurnAsync);
    }
}