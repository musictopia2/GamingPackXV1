using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using ClueBoardGameCP.Logic;
using ClueBoardGameCP.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using aa = BasicBlazorLibrary.Components.CssGrids.RowColumnHelpers;
namespace ClueBoardGameBlazor.Views
{
    public partial class ClueBoardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private readonly CustomBasicList<LabelGridModel> _clues = new CustomBasicList<LabelGridModel>();
        private GameBoardGraphicsCP? _graphicsData;
        private string GetRows => $"{aa.MinContent}auto";
        protected override void OnInitialized()
        {
            _graphicsData = cons!.Resolve<GameBoardGraphicsCP>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ClueBoardGameMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(ClueBoardGameMainViewModel.Status));
            _clues.Clear();
            _clues.AddLabel("Room", nameof(ClueBoardGameMainViewModel.CurrentRoomName))
                .AddLabel("Character", nameof(ClueBoardGameMainViewModel.CurrentCharacterName))
                .AddLabel("Weapon", nameof(ClueBoardGameMainViewModel.CurrentWeaponName));
            DataContext!.PopulateDetectiveNoteBook();
            base.OnInitialized();
        }
        private string EndMethod => nameof(ClueBoardGameMainViewModel.EndTurnAsync);
        private string RollMethod => nameof(ClueBoardGameMainViewModel.RollDiceAsync);
        private string PredictMethod => nameof(ClueBoardGameMainViewModel.MakePredictionAsync);
        private string AccusationMethod => nameof(ClueBoardGameMainViewModel.MakeAccusationAsync);
        private string GetColor => _graphicsData!.GameContainer!.SingleInfo!.Color.ToColor();
    }
}