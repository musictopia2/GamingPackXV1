using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CandylandCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace CandylandBlazor.Views
{
    public partial class CandylandMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private GameBoardVM? BoardModel { get; set; }
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(CandylandMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(CandylandMainViewModel.Status));
            BoardModel = cons!.Resolve<GameBoardVM>();
            base.OnInitialized();
        }
    }
}