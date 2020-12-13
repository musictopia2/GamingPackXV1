using PickelCardGameCP.ViewModels;
namespace PickelCardGameBlazor
{
    public partial class BidControl
    {
        private string BidMethod => nameof(PickelBidViewModel.ProcessBidAsync);
        private string PassMethod => nameof(PickelBidViewModel.PassAsync);
    }
}