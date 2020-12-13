using RookCP.ViewModels;
namespace RookBlazor.Views
{
    public partial class RookBiddingView
    {
        private string BidMethod => nameof(RookBiddingViewModel.BidAsync);
        private string PassMethod => nameof(RookBiddingViewModel.PassAsync);
    }
}