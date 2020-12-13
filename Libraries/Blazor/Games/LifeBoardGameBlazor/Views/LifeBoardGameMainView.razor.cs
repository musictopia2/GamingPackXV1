using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using LifeBoardGameCP.Data;
using LifeBoardGameCP.Graphics;
using LifeBoardGameCP.Logic;
using LifeBoardGameCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace LifeBoardGameBlazor.Views
{
    public partial class LifeBoardGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private IBoardProcesses? _boardProcesses;
        private LifeBoardGameGameContainer? _gameContainer;
        private GameBoardGraphicsCP? _graphicsData;
        private LifeBoardGameVMData? _vmData;
        protected override void OnInitialized()
        {
            _boardProcesses = cons!.Resolve<IBoardProcesses>();
            _gameContainer = cons.Resolve<LifeBoardGameGameContainer>();
            _graphicsData = cons.Resolve<GameBoardGraphicsCP>();
            _vmData = cons.Resolve<LifeBoardGameVMData>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(LifeBoardGameMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(LifeBoardGameMainViewModel.Instructions))
                 .AddLabel("Status", nameof(LifeBoardGameMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(LifeBoardGameMainViewModel.EndTurnAsync);
    }
}