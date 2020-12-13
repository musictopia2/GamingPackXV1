using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor.Views
{
    public partial class ActionFirstCardRandomView : SimpleActionView
    {
        [CascadingParameter]
        public ActionFirstCardRandomViewModel? DataContext { get; set; }
        private string ChooseMethod => nameof(ActionFirstCardRandomViewModel.ChooseCardAsync);
    }
}