using A8RoundRummyCP.Data;
using A8RoundRummyCP.ViewModels;
using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace A8RoundRummyBlazor.Views
{
    public partial class A8RoundRummyMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private A8RoundRummyVMData? _vmData;
        private A8RoundRummyGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<A8RoundRummyVMData>();
            _gameContainer = cons.Resolve<A8RoundRummyGameContainer>();
            _labels.AddLabel("Turn", nameof(A8RoundRummyMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(A8RoundRummyMainViewModel.Status))
                .AddLabel("Next", nameof(A8RoundRummyMainViewModel.NextTurn));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(A8RoundRummyPlayerItem.ObjectCount))
                .AddColumn("Total Score", true, nameof(A8RoundRummyPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string GoOutMethod => nameof(A8RoundRummyMainViewModel.GoOutAsync);
        private string GetColumns => aa.RepeatAuto(2);
    }
}