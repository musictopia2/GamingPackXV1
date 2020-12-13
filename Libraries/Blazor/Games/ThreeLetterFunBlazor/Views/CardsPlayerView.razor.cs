using System;
using ThreeLetterFunCP.ViewModels;
namespace ThreeLetterFunBlazor.Views
{
    public partial class CardsPlayerView : IDisposable
    {
        private string SubmitMethod => nameof(CardsPlayerViewModel.SubmitAsync);
    }
}