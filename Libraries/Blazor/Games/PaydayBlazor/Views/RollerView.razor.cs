using Microsoft.AspNetCore.Components;
using PaydayCP.Data;
using PaydayCP.ViewModels;
namespace PaydayBlazor.Views
{
    public partial class RollerView
    {
        [CascadingParameter]
        public PaydayVMData? VMData { get; set; }
        [CascadingParameter]
        public RollerViewModel? DataContext { get; set; }
        private string RollMethod => nameof(RollerViewModel.RollDiceAsync);
    }
}