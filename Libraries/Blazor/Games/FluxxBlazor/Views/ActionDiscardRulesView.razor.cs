using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using CommonBasicStandardLibraries.CollectionClasses;
using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor.Views
{
    public partial class ActionDiscardRulesView : SimpleActionView
    {
        [CascadingParameter]
        public ActionDiscardRulesViewModel? DataContext { get; set; }
        private string ViewMethod => nameof(ActionDiscardRulesViewModel.ViewRuleCard);
        private string DiscardMethod => nameof(ActionDiscardRulesViewModel.DiscardRulesAsync);
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        protected override void OnInitialized()
        {
            _labels.Clear();
            _labels.AddLabel("Rules To Discard", nameof(ActionDiscardRulesViewModel.RulesToDiscard));
            base.OnInitialized();
        }
    }
}