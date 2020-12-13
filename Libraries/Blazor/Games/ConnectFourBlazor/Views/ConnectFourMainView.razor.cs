using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using ConnectFourCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace ConnectFourBlazor.Views
{
    public partial class ConnectFourMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(ConnectFourMainViewModel.NormalTurn))
                 .AddLabel("Status", nameof(ConnectFourMainViewModel.Status));
            base.OnInitialized();
        }
        private string MethodName => nameof(ConnectFourMainViewModel.ColumnAsync);
    }
}