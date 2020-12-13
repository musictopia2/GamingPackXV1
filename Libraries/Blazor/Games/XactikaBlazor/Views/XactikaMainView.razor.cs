using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using XactikaCP.Data;
using XactikaCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace XactikaBlazor.Views
{
    public partial class XactikaMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private XactikaVMData? _vmData;
        private XactikaGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<XactikaVMData>();
            _gameContainer = cons.Resolve<XactikaGameContainer>();
            _labels.AddLabel("Turn", nameof(XactikaMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(XactikaMainViewModel.Status))
                 .AddLabel("Round", nameof(XactikaMainViewModel.RoundNumber))
                 .AddLabel("Mode", nameof(XactikaMainViewModel.GameModeText));
            _scores.Clear();
            _scores.AddColumn("Cards Left", false, nameof(XactikaPlayerItem.ObjectCount))
                .AddColumn("Bid Amount", false, nameof(XactikaPlayerItem.BidAmount))
                .AddColumn("Tricks Won", false, nameof(XactikaPlayerItem.TricksWon))
                .AddColumn("Current Score", false, nameof(XactikaPlayerItem.CurrentScore))
                .AddColumn("Total Score", false, nameof(XactikaPlayerItem.TotalScore));
            base.OnInitialized();
        }
    }
}