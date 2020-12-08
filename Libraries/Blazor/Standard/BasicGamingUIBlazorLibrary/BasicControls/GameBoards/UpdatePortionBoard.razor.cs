using BasicGameFrameworkLibrary.BasicEventModels;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.BasicControls.GameBoards
{
    //decided to keep this one afterall.
    public partial class UpdatePortionBoard
    {

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override void OnInitialized()
        {
            RepaintEventModel.UpdatePartOfBoard = ShowChange;
            base.OnInitialized();
        }

        private void ShowChange()
        {
            InvokeAsync(StateHasChanged);
        }
    }
}