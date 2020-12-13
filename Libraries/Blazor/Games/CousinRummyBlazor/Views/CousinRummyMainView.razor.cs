using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using CousinRummyCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CousinRummyCP.Data;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace CousinRummyBlazor.Views
{
    public partial class CousinRummyMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private CousinRummyVMData? _vmData;
        private CousinRummyGameContainer? _gameContainer;
        private string GetFirstRows => aa.RepeatAuto(2);
        private string GetFirstColumns => aa.RepeatAuto(2);
        private string GetSecondColumns => aa.RepeatAuto(3);
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<CousinRummyVMData>();
            _gameContainer = cons.Resolve<CousinRummyGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(CousinRummyMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(CousinRummyMainViewModel.Status))
                .AddLabel("Other Turn", nameof(CousinRummyMainViewModel.OtherLabel))
                .AddLabel("Phase", nameof(CousinRummyMainViewModel.PhaseData));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(CousinRummyPlayerItem.ObjectCount))
                .AddColumn("Tokens Left", false, nameof(CousinRummyPlayerItem.TokensLeft))
                .AddColumn("Current Score", false, nameof(CousinRummyPlayerItem.CurrentScore))
                .AddColumn("Total Score", false, nameof(CousinRummyPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string InitMethod => nameof(CousinRummyMainViewModel.FirstSetsAsync);
        private string OtherMethod => nameof(CousinRummyMainViewModel.OtherSetsAsync);
        private string BuyMethod => nameof(CousinRummyMainViewModel.BuyAsync);
        private string PassMethod => nameof(CousinRummyMainViewModel.PassAsync);
    }
}