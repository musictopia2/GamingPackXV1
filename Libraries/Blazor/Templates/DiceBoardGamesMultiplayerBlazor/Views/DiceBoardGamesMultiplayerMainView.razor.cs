using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using DiceBoardGamesMultiplayerCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary;
namespace DiceBoardGamesMultiplayerBlazor.Views
{
    public partial class DiceBoardGamesMultiplayerMainView
    {
        //any code needed will go here.
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(DiceBoardGamesMultiplayerMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(DiceBoardGamesMultiplayerMainViewModel.Instructions))
                 .AddLabel("Status", nameof(DiceBoardGamesMultiplayerMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(DiceBoardGamesMultiplayerMainViewModel.EndTurnAsync);

        private string RollMethod => nameof(DiceBoardGamesMultiplayerMainViewModel.RollDiceAsync);
    }
}