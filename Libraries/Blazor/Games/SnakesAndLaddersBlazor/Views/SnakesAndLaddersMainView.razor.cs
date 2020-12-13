using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using SnakesAndLaddersCP.ViewModels;
namespace SnakesAndLaddersBlazor.Views
{
    public partial class SnakesAndLaddersMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private string RollMethod => nameof(SnakesAndLaddersMainViewModel.RollDiceAsync);
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(SnakesAndLaddersMainViewModel.NormalTurn))
                .AddLabel("Status", nameof(SnakesAndLaddersMainViewModel.Status));
            base.OnInitialized();
        }

    }
}