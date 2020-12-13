using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using HitTheDeckCP.Data;
using HitTheDeckCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace HitTheDeckBlazor.Views
{
    public partial class HitTheDeckMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private HitTheDeckVMData? _vmData;
        private HitTheDeckGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<HitTheDeckVMData>();
            _gameContainer = cons.Resolve<HitTheDeckGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(HitTheDeckMainViewModel.NormalTurn))
                .AddLabel("Next", nameof(HitTheDeckMainViewModel.NextPlayer))
                .AddLabel("Status", nameof(HitTheDeckMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(HitTheDeckPlayerItem.ObjectCount))
                .AddColumn("Total Points", true, nameof(HitTheDeckPlayerItem.TotalPoints))
                .AddColumn("Previous Points", true, nameof(HitTheDeckPlayerItem.PreviousPoints));
            base.OnInitialized();
        }
        private string FlipMethod => nameof(HitTheDeckMainViewModel.FlipAsync);
        private string CutMethod => nameof(HitTheDeckMainViewModel.CutAsync);
        private string EndMethod => nameof(HitTheDeckMainViewModel.EndTurnAsync);
    }
}