using BasicGameFrameworkLibrary.ViewModels;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.Views
{
    public partial class RestoreView
    {
        [CascadingParameter]
        public RestoreViewModel? DataContext { get; set; }
        private string GetMethod => nameof(RestoreViewModel.RestoreAsync);
    }
}