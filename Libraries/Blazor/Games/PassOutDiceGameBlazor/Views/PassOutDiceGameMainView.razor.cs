using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using PassOutDiceGameCP.Logic;
using PassOutDiceGameCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace PassOutDiceGameBlazor.Views
{
    public partial class PassOutDiceGameMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private GameBoardGraphicsCP? _graphicsData;
        protected override void OnInitialized()
        {
            _graphicsData = cons!.Resolve<GameBoardGraphicsCP>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(PassOutDiceGameMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(PassOutDiceGameMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(PassOutDiceGameMainViewModel.EndTurnAsync);
    }
}