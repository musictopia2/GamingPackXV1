using Microsoft.AspNetCore.Components;
using PaydayCP.Data;
namespace PaydayBlazor.Views
{
    public partial class DealPileView
    {
        [CascadingParameter]
        public PaydayVMData? VMData { get; set; } //hopefully this idea works.  this means the main view model needs to do cascading for this as well.
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
    }
}