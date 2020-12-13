using System;
using ThreeLetterFunCP.ViewModels;
namespace ThreeLetterFunBlazor.Views
{
    public partial class AdvancedOptionsView : IDisposable
    {
        private string SubmitMethod => nameof(AdvancedOptionsViewModel.SubmitAsync);
    }
}