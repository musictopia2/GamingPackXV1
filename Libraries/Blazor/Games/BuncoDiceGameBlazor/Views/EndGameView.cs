using BasicGamingUIBlazorLibrary.Views;
using BuncoDiceGameCP.ViewModels;
namespace BuncoDiceGameBlazor.Views
{
    public class EndGameView : BasicCustomButtonView<EndGameViewModel>
    {
        //here is another idea.  another idea can be to also have reflection to find what is needed.
        protected override string MethodName => nameof(EndGameViewModel.EndGameAsync);
        protected override string DisplayName => "End Game";
    }
}