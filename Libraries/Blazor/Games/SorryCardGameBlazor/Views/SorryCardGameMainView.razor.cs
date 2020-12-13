using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using SorryCardGameCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using SorryCardGameCP.Data;
namespace SorryCardGameBlazor.Views
{
    public partial class SorryCardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private SorryCardGameVMData? _vmData;
        private SorryCardGameGameContainer? _gameContainer;
        private CustomBasicList<SorryCardGamePlayerItem> _playerList = new CustomBasicList<SorryCardGamePlayerItem>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<SorryCardGameVMData>();
            _gameContainer = cons.Resolve<SorryCardGameGameContainer>();
            _labels.AddLabel("Turn", nameof(SorryCardGameMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(SorryCardGameMainViewModel.Status))
                .AddLabel("Instructions", nameof(SorryCardGameMainViewModel.Instructions));
            _playerList = _gameContainer.PlayerList!.GetAllPlayersStartingWithSelf();
            base.OnInitialized();
        }

    }
}