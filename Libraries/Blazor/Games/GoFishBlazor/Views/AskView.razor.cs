using GoFishCP.Data;
using GoFishCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace GoFishBlazor.Views
{
    public partial class AskView
    {
        [CascadingParameter]
        public GoFishVMData? Model { get; set; }
        [CascadingParameter]
        public AskViewModel? DataContext { get; set; }
        private string AskMethod => nameof(AskViewModel.AskAsync);
    }
}