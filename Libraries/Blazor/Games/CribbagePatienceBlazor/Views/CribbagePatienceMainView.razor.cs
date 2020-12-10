using CribbagePatienceCP.ViewModels;
namespace CribbagePatienceBlazor.Views
{
    public partial class CribbagePatienceMainView
    {
        private string CribMethod => nameof(CribbagePatienceMainViewModel.CribAsync);
        private string ContinueMethod => nameof(CribbagePatienceMainViewModel.Continue);
    }
}