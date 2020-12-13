using System;
using ThreeLetterFunCP.ViewModels;
namespace ThreeLetterFunBlazor.Views
{
    public partial class FirstOptionView : IDisposable
    {
        private string SubmitMethod => nameof(FirstOptionViewModel.SubmitAsync);
        private string DescriptionMethod => nameof(FirstOptionViewModel.DescriptionAsync);
    }
}