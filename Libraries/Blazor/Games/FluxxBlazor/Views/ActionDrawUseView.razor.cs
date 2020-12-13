using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor.Views
{
    public partial class ActionDrawUseView : SimpleActionView
    {
        [CascadingParameter]
        public ActionDrawUseViewModel? DataContext { get; set; }
        private string DrawUseMethod => nameof(ActionDrawUseViewModel.DrawUseAsync);
    }
}