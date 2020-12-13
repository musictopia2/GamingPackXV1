using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using BladesOfSteelCP.Data;
using BladesOfSteelCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using aa = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace BladesOfSteelBlazor.Views
{
    public partial class BladesOfSteelMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        [CascadingParameter]
        public BladesOfSteelVMData? VMData { get; set; }
        private BladesOfSteelGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _gameContainer = cons!.Resolve<BladesOfSteelGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Main Turn", nameof(BladesOfSteelMainViewModel.NormalTurn))
               .AddLabel("Other Turn", nameof(BladesOfSteelMainViewModel.OtherPlayer))
               .AddLabel("Status", nameof(BladesOfSteelMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(BladesOfSteelPlayerItem.ObjectCount))
                .AddColumn("Score", true, nameof(BladesOfSteelPlayerItem.Score));
            base.OnInitialized();
        }
        private string GetColumns()
        {
            return $"{aa.Auto} {aa.Auto} {aa.Auto}";
        }
        private string GetRows()
        {
            return $"{aa.Auto} {aa.Auto}";
        }
        private string EndTurnMethod => nameof(BladesOfSteelMainViewModel.EndTurnAsync);
        private string PassMethod => nameof(BladesOfSteelMainViewModel.PassAsync);
    }
}