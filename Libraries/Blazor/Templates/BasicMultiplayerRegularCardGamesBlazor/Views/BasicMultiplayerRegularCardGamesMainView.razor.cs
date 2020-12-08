using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using BasicMultiplayerRegularCardGamesCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using BasicMultiplayerRegularCardGamesCP.Data;
namespace BasicMultiplayerRegularCardGamesBlazor.Views
{
    public partial class BasicMultiplayerRegularCardGamesMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        private BasicMultiplayerRegularCardGamesVMData? _vmData;
        private BasicMultiplayerRegularCardGamesGameContainer? _gameContainer;

        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<BasicMultiplayerRegularCardGamesVMData>();
            _gameContainer = cons.Resolve<BasicMultiplayerRegularCardGamesGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(BasicMultiplayerRegularCardGamesMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(BasicMultiplayerRegularCardGamesMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Cards Left", true, nameof(BasicMultiplayerRegularCardGamesPlayerItem.ObjectCount))

                ; //cards left is common.  can be anything you need.
            base.OnInitialized();
        }

    }
}