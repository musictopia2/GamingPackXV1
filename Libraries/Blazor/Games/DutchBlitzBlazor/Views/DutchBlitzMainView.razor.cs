using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using DutchBlitzCP.Data;
using DutchBlitzCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace DutchBlitzBlazor.Views
{
    public partial class DutchBlitzMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private DutchBlitzVMData? _vmData;
        private DutchBlitzGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<DutchBlitzVMData>();
            _gameContainer = cons.Resolve<DutchBlitzGameContainer>();
            _labels.AddLabel("Turn", nameof(DutchBlitzMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(DutchBlitzMainViewModel.Status))
                .AddLabel("Error", nameof(DutchBlitzMainViewModel.ErrorMessage));
            _scores.Clear();
            _scores.AddColumn("Stock Left", false, nameof(DutchBlitzPlayerItem.StockLeft))
                .AddColumn("Points Round", false, nameof(DutchBlitzPlayerItem.PointsRound))
                .AddColumn("Points Game", false, nameof(DutchBlitzPlayerItem.PointsGame));
            base.OnInitialized();
        }
        private string DutchMethod => nameof(DutchBlitzMainViewModel.DutchAsync);

    }
}