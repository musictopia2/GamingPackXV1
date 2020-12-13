using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using Rummy500CP.Data;
using Rummy500CP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace Rummy500Blazor.Views
{
    public partial class Rummy500MainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private Rummy500VMData? _vmData;
        private Rummy500GameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<Rummy500VMData>();
            _gameContainer = cons.Resolve<Rummy500GameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(Rummy500MainViewModel.NormalTurn))
               .AddLabel("Status", nameof(Rummy500MainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(Rummy500PlayerItem.ObjectCount))
                .AddColumn("Points Played", false, nameof(Rummy500PlayerItem.PointsPlayed))
                .AddColumn("Cards Played", false, nameof(Rummy500PlayerItem.CardsPlayed))
                .AddColumn("Score Current", false, nameof(Rummy500PlayerItem.CurrentScore))
                .AddColumn("Score Total", false, nameof(Rummy500PlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string DiscardCurrentMethod => nameof(Rummy500MainViewModel.DiscardCurrentAsync);
        private string CreateNewRummyMethod => nameof(Rummy500MainViewModel.CreateSetAsync);
    }
}