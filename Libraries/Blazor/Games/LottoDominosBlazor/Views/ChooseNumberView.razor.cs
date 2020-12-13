using LottoDominosCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace LottoDominosBlazor.Views
{
    public partial class ChooseNumberView
    {
        [CascadingParameter]
        public ChooseNumberViewModel? DataContext { get; set; }
        private string ChooseMethod => nameof(ChooseNumberViewModel.ChooseNumberAsync);
    }
}