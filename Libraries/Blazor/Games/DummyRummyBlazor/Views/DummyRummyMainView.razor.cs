using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using DummyRummyCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using DummyRummyCP.Data;
namespace DummyRummyBlazor.Views
{
    public partial class DummyRummyMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private DummyRummyVMData? _vmData;
        private DummyRummyGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<DummyRummyVMData>();
            _gameContainer = cons.Resolve<DummyRummyGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(DummyRummyMainViewModel.NormalTurn))
               .AddLabel("Status", nameof(DummyRummyMainViewModel.Status))
               .AddLabel("Up To", nameof(DummyRummyMainViewModel.UpTo));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(DummyRummyPlayerItem.ObjectCount))
                .AddColumn("Current Score", true, nameof(DummyRummyPlayerItem.CurrentScore))
                .AddColumn("Total Score", true, nameof(DummyRummyPlayerItem.TotalScore));
            base.OnInitialized();
        }
        private string LayDownMethod => nameof(DummyRummyMainViewModel.LayDownSetsAsync);
        private string BackMethod => nameof(DummyRummyMainViewModel.Back);
    }
}