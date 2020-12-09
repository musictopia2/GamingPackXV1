using BasicGamingUIBlazorLibrary.Views;
using BuncoDiceGameCP.ViewModels;
namespace BuncoDiceGameBlazor.Views
{
    public class BuncoNewRoundView : BasicCustomButtonView<BuncoNewRoundViewModel>
    {
        protected override string MethodName => nameof(BuncoNewRoundViewModel.NewRoundAsync);
        protected override string DisplayName => "New Round";
    }
}