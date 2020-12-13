using Microsoft.AspNetCore.Components;
using PickelCardGameCP.ViewModels;
namespace PickelCardGameBlazor.Views
{
    public partial class PickelBidView
    {
        [CascadingParameter]
        public PickelBidViewModel? DataContext { get; set; }
    }
}