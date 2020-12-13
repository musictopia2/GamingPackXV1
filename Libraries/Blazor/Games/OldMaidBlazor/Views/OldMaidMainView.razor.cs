using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using OldMaidCP.Data;
using OldMaidCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace OldMaidBlazor.Views
{
    public partial class OldMaidMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private OldMaidVMData? _vmData;
        private OldMaidGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<OldMaidVMData>();
            _gameContainer = cons.Resolve<OldMaidGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(OldMaidMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(OldMaidMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(OldMaidMainViewModel.EndTurnAsync);
    }
}