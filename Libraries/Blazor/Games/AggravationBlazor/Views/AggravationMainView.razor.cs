using AggravationCP.Data;
using AggravationCP.Graphics;
using AggravationCP.ViewModels;
using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace AggravationBlazor.Views
{
    public partial class AggravationMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private AggravationGameContainer? _gameContainer;
        private GameBoardGraphicsCP? _graphicsData;
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(AggravationMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(AggravationMainViewModel.Instructions))
                 .AddLabel("Status", nameof(AggravationMainViewModel.Status));
            _gameContainer = cons!.Resolve<AggravationGameContainer>();
            _graphicsData = cons.Resolve<GameBoardGraphicsCP>();
            base.OnInitialized();
        }
        private string RollMethod => nameof(AggravationMainViewModel.RollDiceAsync);
    }
}