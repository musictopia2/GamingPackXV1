using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using TroubleCP.Data;
using TroubleCP.Graphics;
using TroubleCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace TroubleBlazor.Views
{
    public partial class TroubleMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private TroubleGameContainer? _gameContainer;
        private GameBoardGraphicsCP? _graphicsData;
        protected override void OnInitialized()
        {
            _gameContainer = cons!.Resolve<TroubleGameContainer>();
            _graphicsData = cons.Resolve<GameBoardGraphicsCP>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(TroubleMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(TroubleMainViewModel.Instructions))
                 .AddLabel("Status", nameof(TroubleMainViewModel.Status));
            base.OnInitialized();
        }
        private string RollMethod => nameof(TroubleMainViewModel.RollDiceAsync);
    }
}