using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using MancalaCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace MancalaBlazor.Views
{
    public partial class MancalaMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private GameBoardVM? OtherModel { get; set; }
        protected override void OnInitialized()
        {
            _labels.Clear();
            DataContext!.CommandContainer.AddAction(ShowChange);
            _labels.AddLabel("Turn", nameof(MancalaMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(MancalaMainViewModel.Status))
                 .AddLabel("Instructions", nameof(MancalaMainViewModel.Instructions));
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            OtherModel = cons!.Resolve<GameBoardVM>();
            base.OnParametersSet();
        }
    }
}