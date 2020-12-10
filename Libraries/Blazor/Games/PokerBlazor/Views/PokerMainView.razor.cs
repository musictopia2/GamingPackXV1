using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using PokerCP.ViewModels;
namespace PokerBlazor.Views
{
    public partial class PokerMainView
    {
        private CustomBasicList<LabelGridModel> Labels { get; set; } = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {

            Labels.Clear();
            Labels.AddLabel("Money", nameof(PokerMainViewModel.Money))
                .AddLabel("Round", nameof(PokerMainViewModel.Round))
                .AddLabel("Winnings", nameof(PokerMainViewModel.Winnings))
                .AddLabel("Hand", nameof(PokerMainViewModel.HandLabel));
            //if you have to add command change, do so as well.
            base.OnInitialized();
        }
        private string MethodName => nameof(PokerMainViewModel.NewRoundAsync);
    }
}