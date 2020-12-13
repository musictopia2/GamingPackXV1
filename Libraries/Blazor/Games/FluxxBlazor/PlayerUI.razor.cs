using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor
{
    public partial class PlayerUI
    {
        [Parameter]
        public bool UsePlayerButton { get; set; }
        [CascadingParameter]
        public CompleteContainerClass? CompleteContainer { get; set; }
        [Parameter]
        public object? DataContext { get; set; }
        [Parameter]
        public string ItemHeight { get; set; } = "5vh";
        [Parameter]
        public int ItemWidth { get; set; } = 200;
        private string ChooseMethod => nameof(ActionTakeUseViewModel.ChoosePlayerAsync);
    }
}