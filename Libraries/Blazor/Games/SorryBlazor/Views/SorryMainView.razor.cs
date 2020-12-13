using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SorryCP.Graphics;
using SorryCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace SorryBlazor.Views
{
    public partial class SorryMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private GameBoardGraphicsCP? _graphics;
        protected override void OnInitialized()
        {
            _graphics = cons!.Resolve<GameBoardGraphicsCP>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(SorryMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(SorryMainViewModel.Instructions))
                 .AddLabel("Status", nameof(SorryMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(SorryMainViewModel.EndTurnAsync);
    }
}