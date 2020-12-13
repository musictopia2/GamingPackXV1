using Microsoft.AspNetCore.Components;
using MillebournesCP.Data;
using MillebournesCP.Logic;
using MillebournesCP.ViewModels;
namespace MillebournesBlazor
{
    public partial class SafetiesBlazor
    {
        [CascadingParameter]
        public MillebournesGameContainer? GameContainer { get; set; }
        [CascadingParameter]
        public MillebournesMainViewModel? DataContext { get; set; }
        [Parameter]
        public TeamCP? Team { get; set; }
        private string ClickName => nameof(MillebournesMainViewModel.SafetyClickAsync); //i think
    }
}