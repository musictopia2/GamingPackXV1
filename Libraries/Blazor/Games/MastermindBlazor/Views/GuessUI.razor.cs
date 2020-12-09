using MastermindCP.Data;
using MastermindCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace MastermindBlazor.Views
{
    public partial class GuessUI
    {
        [Parameter]
        public Guess? Guess { get; set; }
        private static string MethodName => nameof(GameBoardViewModel.ChangeMind);



    }
}