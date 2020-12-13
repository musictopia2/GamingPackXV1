using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SequenceDiceCP.Data;
using SequenceDiceCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace SequenceDiceBlazor.Views
{
    public partial class SequenceDiceMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private SequenceDiceSaveInfo? _saveRoot;
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(SequenceDiceMainViewModel.NormalTurn))
                 .AddLabel("Instructions", nameof(SequenceDiceMainViewModel.Instructions))
                 .AddLabel("Status", nameof(SequenceDiceMainViewModel.Status));
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            _saveRoot = cons!.Resolve<SequenceDiceSaveInfo>();
            base.OnParametersSet();
        }
        private string RollMethod => nameof(SequenceDiceMainViewModel.RollDiceAsync);
        private string ColumnText => "50vw 50vw"; //could adjust as needed.
        private string MoveMethod => nameof(SequenceDiceMainViewModel.MakeMoveAsync);
    }
}