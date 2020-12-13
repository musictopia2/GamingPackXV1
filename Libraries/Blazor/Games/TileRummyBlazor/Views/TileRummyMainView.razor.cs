using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGamingUIBlazorLibrary.Shells;
using TileRummyCP.ViewModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using BasicBlazorLibrary.Helpers;
using TileRummyCP.Data;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
namespace TileRummyBlazor.Views
{
    public partial class TileRummyMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(TileRummyMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(TileRummyMainViewModel.Status));
            _scores.Clear();
            _scores.AddColumn("Tiles Left", true, nameof(TileRummyPlayerItem.ObjectCount))
                .AddColumn("Score", true, nameof(TileRummyPlayerItem.Score));

            base.OnInitialized();
        }
        public TileInfo GetTile
        {
            get
            {
                TileInfo output = new TileInfo();
                output.IsUnknown = true;
                output.Deck = 1; //needed so the back can show up properly.
                return output;
            }
        }
        private string FirstSetMethod => nameof(TileRummyMainViewModel.CreateFirstSetsAsync);
        private string NewSetMethod => nameof(TileRummyMainViewModel.CreateNewSetAsync);
        private string ResetMethod => nameof(TileRummyMainViewModel.UndoMoveAsync);
        private string EndMethod => nameof(TileRummyMainViewModel.EndTurnAsync);
    }
}