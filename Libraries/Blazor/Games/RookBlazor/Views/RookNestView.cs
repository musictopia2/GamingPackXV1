using BasicGamingUIBlazorLibrary.Views;
using RookCP.ViewModels;
namespace RookBlazor.Views
{
    public class RookNestView : BasicSubmitView<RookNestViewModel>
    {
        protected override string CommandText => nameof(RookNestViewModel.NestAsync);
    }
}