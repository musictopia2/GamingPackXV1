using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor.Views
{
    public partial class ActionTakeUseView : SimpleActionView
    {
        [CascadingParameter]
        public ActionTakeUseViewModel? DataContext { get; set; }
        private string ChooseMethod => nameof(ActionTakeUseViewModel.ChooseCardAsync);
    }
}