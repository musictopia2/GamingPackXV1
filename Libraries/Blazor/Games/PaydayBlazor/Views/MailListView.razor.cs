using Microsoft.AspNetCore.Components;
using PaydayCP.Data;
namespace PaydayBlazor.Views
{
    public partial class MailListView
    {
        [CascadingParameter]
        public PaydayVMData? VMData { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
    }
}