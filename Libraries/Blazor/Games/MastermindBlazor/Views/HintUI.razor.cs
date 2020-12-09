using MastermindCP.Data;
using Microsoft.AspNetCore.Components;
namespace MastermindBlazor.Views
{
    public partial class HintUI
    {
        [Parameter]
        public Guess? YourGuess { get; set; }
        private int LeftOvers => 4 - YourGuess!.HowManyAquas - YourGuess.HowManyBlacks;
    }
}