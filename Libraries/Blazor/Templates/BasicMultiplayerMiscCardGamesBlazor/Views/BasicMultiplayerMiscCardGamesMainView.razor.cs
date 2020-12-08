using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using BasicMultiplayerMiscCardGamesCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using BasicMultiplayerMiscCardGamesCP.Data;
namespace BasicMultiplayerMiscCardGamesBlazor.Views
{
    public partial class BasicMultiplayerMiscCardGamesMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private BasicMultiplayerMiscCardGamesVMData? _vmData;
        private BasicMultiplayerMiscCardGamesGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<BasicMultiplayerMiscCardGamesVMData>();
            _gameContainer = cons.Resolve<BasicMultiplayerMiscCardGamesGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(BasicMultiplayerMiscCardGamesMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(BasicMultiplayerMiscCardGamesMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(BasicMultiplayerMiscCardGamesPlayerItem.ObjectCount));
            base.OnInitialized();
        }

    }
}