using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using DominoBonesMultiplayerGamesCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using DominoBonesMultiplayerGamesCP.Data;
using BasicGameFrameworkLibrary.Dominos;
namespace DominoBonesMultiplayerGamesBlazor.Views
{
    public partial class DominoBonesMultiplayerGamesMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(DominoBonesMultiplayerGamesMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(DominoBonesMultiplayerGamesMainViewModel.Status));
            _scores.Clear();
            //not sure what scores we will have but its a starting point.
            base.OnInitialized();
        }
        public SimpleDominoInfo GetDomino
        {
            get
            {
                SimpleDominoInfo output = new SimpleDominoInfo();
                output.IsUnknown = true;
                output.Deck = 1; //needed so the back can show up properly.
                return output;
            }
        }
    }
}