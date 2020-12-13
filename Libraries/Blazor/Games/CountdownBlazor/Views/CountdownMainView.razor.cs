using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using CountdownCP.Data;
using CountdownCP.ViewModels;
using Microsoft.AspNetCore.Components;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace CountdownBlazor.Views
{
    public partial class CountdownMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private CountdownGameContainer? _gameContainer;
        protected override void OnInitialized()
        {
            _gameContainer = cons!.Resolve<CountdownGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(CountdownMainViewModel.NormalTurn))
                .AddLabel("Round", nameof(CountdownMainViewModel.Round))
                .AddLabel("Status", nameof(CountdownMainViewModel.Status));
            base.OnInitialized();
        }
        private string EndMethod => nameof(CountdownMainViewModel.EndTurnAsync);
        private string HintsMethod => nameof(CountdownMainViewModel.Hint); //i think.
    }
}