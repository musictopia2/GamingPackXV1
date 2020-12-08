using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using BasicMultiplayerTrickCardGamesCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using BasicMultiplayerTrickCardGamesCP.Data;
namespace BasicMultiplayerTrickCardGamesBlazor.Views
{
    public partial class BasicMultiplayerTrickCardGamesMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private BasicMultiplayerTrickCardGamesVMData? _vmData;
        private BasicMultiplayerTrickCardGamesGameContainer? _gameContainer;

        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<BasicMultiplayerTrickCardGamesVMData>();
            _gameContainer = cons.Resolve<BasicMultiplayerTrickCardGamesGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(BasicMultiplayerTrickCardGamesMainViewModel.NormalTurn))
                .AddLabel("Trump", nameof(BasicMultiplayerTrickCardGamesMainViewModel.TrumpSuit))
                .AddLabel("Status", nameof(BasicMultiplayerTrickCardGamesMainViewModel.Status));

            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(BasicMultiplayerTrickCardGamesPlayerItem.ObjectCount))
                .AddColumn("Tricks Won", true, nameof(BasicMultiplayerTrickCardGamesPlayerItem.TricksWon))
                ; //cards left is common.  can be anything you need.
            base.OnInitialized();
        }

    }
}