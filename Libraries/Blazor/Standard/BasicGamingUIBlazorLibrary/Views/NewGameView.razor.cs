using BasicGameFrameworkLibrary.ViewModels;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.Views
{
    public partial class NewGameView
    {
        [CascadingParameter]
        public NewGameViewModel? DataContext { get; set; }

        private string GetMethod => nameof(NewGameViewModel.StartNewGameAsync);
    }
}