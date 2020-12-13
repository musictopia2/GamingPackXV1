using BackgammonCP.Graphics;
using BackgammonCP.ViewModels;
using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BackgammonBlazor.Views
{
    public partial class BackgammonMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private GameBoardGraphicsCP? _graphicsData;
        protected override void OnInitialized()
        {
            _graphicsData = cons!.Resolve<GameBoardGraphicsCP>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(BackgammonMainViewModel.NormalTurn))
               .AddLabel("Game Status", nameof(BackgammonMainViewModel.Status))
               .AddLabel("Moves Made", nameof(BackgammonMainViewModel.MovesMade))
               .AddLabel("Last Status", nameof(BackgammonMainViewModel.LastStatus))
               .AddLabel("Instructions", nameof(BackgammonMainViewModel.Instructions));
            base.OnInitialized();
        }
        private string EndMethod => nameof(BackgammonMainViewModel.EndTurnAsync);
        private string UndoMethod => nameof(BackgammonMainViewModel.UndoMoveAsync);
    }
}