using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using FourSuitRummyCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using FourSuitRummyCP.Data;
namespace FourSuitRummyBlazor.Views
{
    public partial class FourSuitRummyMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private FourSuitRummyVMData? _vmData;
        private FourSuitRummyGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<FourSuitRummyVMData>();
            _gameContainer = cons.Resolve<FourSuitRummyGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(FourSuitRummyMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(FourSuitRummyMainViewModel.Status));
            _scores.AddColumn("Cards Left", true, nameof(FourSuitRummyPlayerItem.ObjectCount))
                .AddColumn("Total Score", true, nameof(FourSuitRummyPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string PlaySetsMethod => nameof(FourSuitRummyMainViewModel.PlaySetsAsync);
        private FourSuitRummyPlayerItem GetSelf => _gameContainer!.PlayerList!.GetSelf();
        private FourSuitRummyPlayerItem GetOpponent => _gameContainer!.PlayerList!.GetOnlyOpponent();
    }
}