using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using PlainBoardGamesMultiplayerCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary;
namespace PlainBoardGamesMultiplayerBlazor.Views
{
    public partial class PlainBoardGamesMultiplayerMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(PlainBoardGamesMultiplayerMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(PlainBoardGamesMultiplayerMainViewModel.Instructions))
                 .AddLabel("Status", nameof(PlainBoardGamesMultiplayerMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(PlainBoardGamesMultiplayerMainViewModel.EndTurnAsync);
    }
}