using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor.Views
{
    public partial class ActionDoAgainView : SimpleActionView
    {
        [CascadingParameter]
        public ActionDoAgainViewModel? DataContext { get; set; }
        private string SelectMethod => nameof(ActionDoAgainViewModel.SelectCardAsync);
        private string ViewMethod => nameof(ActionDoAgainViewModel.ViewCard);
    }
}