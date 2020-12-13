using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor.Views
{
    public partial class ActionEverybodyGetsOneView : SimpleActionView
    {
        [CascadingParameter]
        public ActionEverybodyGetsOneViewModel? DataContext { get; set; }
        private string GiveMethod => nameof(ActionEverybodyGetsOneViewModel.GiveCardsAsync);
    }
}