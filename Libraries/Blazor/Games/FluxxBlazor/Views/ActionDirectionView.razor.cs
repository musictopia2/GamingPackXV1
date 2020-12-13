using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor.Views
{
    public partial class ActionDirectionView : SimpleActionView
    {
        [CascadingParameter]
        public ActionDirectionViewModel? DataContext { get; set; }
        private string DirectionMethod => nameof(ActionDirectionViewModel.DirectionAsync);
    }
}