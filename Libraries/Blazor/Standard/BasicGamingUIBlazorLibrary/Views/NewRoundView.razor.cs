using BasicGameFrameworkLibrary.ViewModels;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.Views
{
    public partial class NewRoundView
    {
        [CascadingParameter]
        public NewRoundViewModel? DataContext { get; set; }
        private string GetMethod => nameof(NewRoundViewModel.StartNewRoundAsync);
    }
}